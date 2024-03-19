using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Services;
using BaseCodeAPI.Src.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace BaseCodeAPI.Src.Middlewares
{
   public class CancellationTokenMiddleware
   {
      private readonly RequestDelegate _next;
      private CancellationTokenSource FCancellationTokenSource { get; set; }

      public CancellationTokenMiddleware(RequestDelegate next)
      {
         this._next = next;
      }

      public async Task InvokeAsync(HttpContext AContext)
      {
         try
         {
            var authorizationHeader = (string)AContext.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authorizationHeader))
            {
               var utils = UtilsClass.New();
            
               //var authorizationHeader = (string)AContext.Request.Headers["Authorization"];

               if (string.IsNullOrEmpty(authorizationHeader))
               {
                  await AContext.Response.WriteAsJsonAsync(utils.ProcessExceptionMessage(new Exception("Ooops! Token inexistente no header")).Json);
                  return;
               }

               var tokenUserModelDto = new TokenUserModelDto
               {
                  Token = authorizationHeader.Split(' ')[1]
               };

               AContext.Items["Token"] = tokenUserModelDto.Token;

               var (Status, Json) = await SecurityService.New().CreateNewToken(tokenUserModelDto);

               switch (Status)
               {
                  case (byte)GlobalEnum.eStatusProc.Sucesso:
                  await this._next(AContext);
                  break;
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
