using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Services;
using BaseCodeAPI.Src.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BaseCodeAPI.Src.Middlewares
{
   public class TokenFailureMiddleware
   {
      private readonly RequestDelegate _next;

      public TokenFailureMiddleware(RequestDelegate next)
      {
         _next = next;
      }

      public async Task Invoke(HttpContext AContext)
      {

         await _next(AContext);

         var utils = UtilsClass.New();

         if (AContext.Response.StatusCode != StatusCodes.Status401Unauthorized)
         {
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

            var (Status, Json) = await SecurityService.New().CreateNewToken(tokenUserModelDto);

            ActionResult result = Status switch
            {
               (byte)GlobalEnum.eStatusProc.Sucesso => new OkObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.NaoLocalizado => new OkObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.NaoAutorizado => new UnauthorizedObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroProcessamento => new UnprocessableEntityObjectResult(Json),
               (byte)GlobalEnum.eStatusProc.ErroServidor => throw new NotImplementedException(),
               _ => throw new NotImplementedException()
            };

            await result.ExecuteResultAsync(new ActionContext
            {
               HttpContext = AContext
            });
         }
      }
   }
}
