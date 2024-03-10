namespace BaseCodeAPI.Src.Middleware
{
   public static class WeighingTypesMiddlewareExtensions
   {
      public static IApplicationBuilder UserMiddlewareBuilder(this IApplicationBuilder builder)
      {
         return builder.UseMiddleware<UserMiddleware>();
      }
   }
}
