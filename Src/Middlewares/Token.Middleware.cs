using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Services;
using BaseCodeAPI.Src.Utils;
using System.Reflection;

namespace BaseCodeAPI.Src.Middlewares
{
   public class TokenMiddleware
   {
      private readonly RequestDelegate _next;

      public TokenMiddleware(RequestDelegate next)
      {
         this._next = next;
      }

      public async Task InvokeAsync(HttpContext AContext)
      {
         try
         {
            var endpoint = AContext.GetEndpoint();

            if (endpoint != null)
            {
               var hasAuthenticated = endpoint.Metadata.GetMetadata<Authenticated>() != null;

               if (hasAuthenticated)
               {
                  var utils = UtilsClass.New();

                  var authorizationHeader = (string)AContext.Request.Headers["Authorization"];

                  if (string.IsNullOrEmpty(authorizationHeader))
                  {
                     await AContext.Response.WriteAsJsonAsync(utils.ProcessExceptionMessage(new Exception("Ooops! Token inexistente no header")).Json);
                     return;
                  }

                  var tokenUserModelDto = new TokenUserModelDto
                  {
                     Token = authorizationHeader.Split(' ')[1]
                  };

                  if (!SecurityService.New().IsTokenExpired(tokenUserModelDto.Token))
                  {
                     await this._next(AContext);
                     return;
                  }

                  var (Status, Json) = await SecurityService.New().CreateNewToken(tokenUserModelDto);

                  if (Status == (byte)GlobalEnum.eStatusProc.Sucesso)
                  {
                     Type objectType = Json.GetType();
                     PropertyInfo retornoProperty = objectType.GetProperty("retorno");

                     if (retornoProperty != null)
                     {
                        object retornoValue = retornoProperty.GetValue(Json);

                        if (retornoValue is not null && retornoValue.GetType().GetProperty("dados") != null)
                        {
                           object dadosValue = retornoValue.GetType().GetProperty("dados").GetValue(retornoValue);

                           if (dadosValue != null && dadosValue.GetType().GetProperty("Token") != null)
                           {
                              AContext.Items["Token"] = dadosValue.GetType().GetProperty("Token").GetValue(dadosValue)?.ToString();
                           }
                        }
                     }

                     await this._next(AContext);
                     return;
                  }

                  switch (Status)
                  {
                     case (byte)GlobalEnum.eStatusProc.NaoLocalizado:
                     await AContext.Response.WriteAsJsonAsync(utils.ProcessExceptionMessage(new Exception("Ooops! Usuário não encontrado.")).Json);
                     break;
                     case (byte)GlobalEnum.eStatusProc.NaoAutorizado:
                     await AContext.Response.WriteAsJsonAsync(utils.ProcessExceptionMessage(new Exception("Ooops! Acesso não autorizado.")).Json);
                     break;
                     case (byte)GlobalEnum.eStatusProc.ErroProcessamento:
                     await AContext.Response.WriteAsJsonAsync(utils.ProcessExceptionMessage(new Exception("Ooops! Erro de processamento.")).Json);
                     break;
                     case (byte)GlobalEnum.eStatusProc.ErroServidor:
                     await AContext.Response.WriteAsJsonAsync(utils.ProcessExceptionMessage(new Exception("Ooops! Erro de servidor.")).Json);
                     break;
                     default:
                     throw new NotImplementedException();
                  }

                  return;
               }
            }

            await this._next(AContext);
         }
         catch (OperationCanceledException ex)
         {
            AContext.Response.StatusCode = StatusCodes.Status408RequestTimeout;
            await AContext.Response.WriteAsJsonAsync(ResponseUtils.Instancia().RetornoErrorProcess(ex));
         }
      }
   }
}
