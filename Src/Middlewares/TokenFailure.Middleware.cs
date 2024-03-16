using BaseCodeAPI.Src.Utils;

namespace BaseCodeAPI.Src.Middlewares
{
   public class TokenFailureMiddleware
   {
      private readonly RequestDelegate _next;

      public TokenFailureMiddleware(RequestDelegate next)
      {
         _next = next;
      }

      public async Task Invoke(HttpContext AContext)  //Quando token vencer, vai retornar "Unauthorized" e o front terá que fazer nova requisição p/ rota de geração de novo token com base no refresh token
      {
         await _next(AContext);

         var utils = UtilsClass.New();

         if (AContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
         {
            string authorizationHeader = AContext.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorizationHeader))
            {
               await AContext.Response.WriteAsJsonAsync(utils.ProcessExceptionMessage(new Exception("Ooops! Token inexistente no header")).Json);
               return;
            }

            await AContext.Response.WriteAsJsonAsync(utils.ProcessExceptionMessage(new Exception("Ooops! Token enviado é inválido.")).Json);
            return;
         }

      }
   }
}
