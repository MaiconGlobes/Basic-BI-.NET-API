namespace BaseCodeAPI.Src.Middlewares
{
   public static class TokenFailureMiddlewareExtensions
   {
      public static IApplicationBuilder TokenFailureMiddlewareBuilder(this IApplicationBuilder builder)
      {
         return builder.UseMiddleware<TokenFailureMiddleware>();
      }
   }
}
