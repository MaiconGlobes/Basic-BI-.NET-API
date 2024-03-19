using BaseCodeAPI.Src.Middlewares;

namespace BaseCodeAPI.Src.Middleware
{
   public static class CancellationTokenMiddlewareExtensions
   {
      public static IApplicationBuilder CancellationTokenMiddlewareBuilder(this IApplicationBuilder builder)
      {
         return builder.UseMiddleware<CancellationTokenMiddleware>();
      }
   }
}