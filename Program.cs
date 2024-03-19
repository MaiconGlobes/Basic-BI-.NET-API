using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Middleware;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Models.Profiles;
using BaseCodeAPI.Src.Repositories;
using BaseCodeAPI.Src.Services;

namespace BaseCodeAPI
{
   public class Program
   {
      public static void Main(string[] args)
      {
         var builder = WebApplication.CreateBuilder(args);

         ConfigServices(builder.Services);

         var app = builder.Build();

         app.UseCors(cors => cors.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
         app.TokenMiddlewareBuilder();
         app.UseAuthentication();
         app.UseAuthorization();
         app.MapControllers();
         app.Run("http://*:5005");
      }

      public static void ConfigServices(IServiceCollection services)
      {
         services.AddCors();
         services.AddControllers();
         services.AddHttpContextAccessor();

         services.AddAutoMapper(typeof(AutoMapperProfile));
         services.AddScoped<IServices, UserService>();
         services.AddScoped<IRepository<UserModel>, UserRepository>();

         services.AddAuthentication();
         services.AddAuthorization();
      }
   }
}
