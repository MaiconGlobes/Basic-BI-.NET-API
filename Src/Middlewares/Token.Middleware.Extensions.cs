using BaseCodeAPI.Src.Middlewares;

namespace BaseCodeAPI.Src.Middleware
{
   public static class TokenMiddlewareExtensions
   {
      public static IApplicationBuilder TokenMiddlewareBuilder(this IApplicationBuilder builder)
      {
         return builder.UseMiddleware<TokenMiddleware>();
      }
   }
}