namespace BaseCodeAPI.Src.Middlewares
{
   public class TokenFailureMiddleware
   {
      private readonly RequestDelegate _next;

      public TokenFailureMiddleware(RequestDelegate next)
      {
         _next = next;
      }

      public async Task Invoke(HttpContext context)
      {
         await _next(context);

         if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
         {
            await context.Response.WriteAsync("Falha na autenticação: Token inválido ou expirado.");
         }
      }
   }
}
