using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Services;
using BaseCodeAPI.Src.Utils;

namespace BaseCodeAPI.Src.Middlewares
{
   public class TokenFailureMiddleware
   {
      private readonly RequestDelegate _next;
      private IToken FIToken { get; set; }

      public TokenFailureMiddleware(RequestDelegate next)
      {
         _next = next;
      }

      public async Task Invoke(HttpContext AContext)
      {
         await _next(AContext);

         var utils = UtilsClass.New();

         if (AContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
         {
            var tokenService = new TokenService();

            string authorizationHeader = AContext.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorizationHeader))
            {
               await AContext.Response.WriteAsJsonAsync(utils.ProcessExceptionMessage(new Exception("Ooops! Token inexistente no header")).Json);
               return;
            }

            var tokenUserModelDto = new TokenUserModelDto
            {
               Token = authorizationHeader.Split(' ')[1]
            };

            var (Status, Json) = await tokenService.CreateNewToken(tokenUserModelDto);

            switch (Status)
            {
               case (byte)GlobalEnum.eStatusProc.Sucesso:
                  AContext.Response.StatusCode = StatusCodes.Status201Created;
                  await AContext.Response.WriteAsJsonAsync(Json);
                  break;
               case (byte)GlobalEnum.eStatusProc.NaoLocalizado:
                  AContext.Response.StatusCode = StatusCodes.Status200OK;
                  await AContext.Response.WriteAsJsonAsync(Json);
                  break;
               case (byte)GlobalEnum.eStatusProc.NaoAutorizado:
                  AContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                  await AContext.Response.WriteAsJsonAsync(Json);
                  break;
               case (byte)GlobalEnum.eStatusProc.ErroProcessamento:
                  AContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                  await AContext.Response.WriteAsJsonAsync(Json);
                  break;
               case (byte)GlobalEnum.eStatusProc.ErroServidor:
                  throw new NotImplementedException();
               default:
                  throw new NotImplementedException();
            }

            return;
         }
      }
   }
}
