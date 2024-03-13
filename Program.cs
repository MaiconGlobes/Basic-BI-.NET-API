using BaseCodeAPI.Src.Middleware;
using BaseCodeAPI.Src.Models.Profiles;

namespace BaseCodeAPI
{
   public class Program
   {
      public static void Main(string[] args)
      {
         var builder = WebApplication.CreateBuilder(args);

         builder.Services.AddControllers();

         ConfigureServices(builder.Services);

         var app = builder.Build();

         FinallyServiceAPI(app);

         app.UseAuthorization();
         app.MapControllers();

         app.UserMiddlewareBuilder();

         app.Run("http://*:5005");
      }

      public static void ConfigureServices(IServiceCollection services)
      {
         services.AddControllers();
         services.AddHttpContextAccessor();
         services.AddAutoMapper(typeof(AutoMapperProfile));
      }

      private static void FinallyServiceAPI(WebApplication AApp)
      {
         var lifetime = AApp.Services.GetRequiredService<IHostApplicationLifetime>();

         lifetime.ApplicationStopping.Register(() =>
         {
            //Faz algo ao finalizar o serviço da api
         });
      }
   }
}
