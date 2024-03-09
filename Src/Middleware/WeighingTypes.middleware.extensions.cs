namespace ASControllerAPI.Src.Middleware
{
   public static class WeighingTypesMiddlewareExtensions
   {
      public static IApplicationBuilder WeighingTypesMiddlewareBuilder(this IApplicationBuilder builder)
      {
         return builder.UseMiddleware<WeighingTypesMiddleware>();
      }
   }
}
