using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Middleware;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Models.Profiles;
using BaseCodeAPI.Src.Repositories;
using BaseCodeAPI.Src.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BaseCodeAPI
{
   public class Program
   {
      public static IConfigurationRoot FIConfigurationRoot { get; set; }

      public static void Main(string[] args)
      {
         FIConfigurationRoot = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())                                                         
            .AddJsonFile("settingsconfig.json")
            .Build();

         var builder = WebApplication.CreateBuilder(args);

         ConfigureServices(builder.Services);

         var app = builder.Build();

         FinallyServiceAPI(app);

         app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
         app.UseAuthentication();
         app.UseAuthorization();
         app.MapControllers();
         app.UserMiddlewareBuilder();

         app.Run("http://*:5005");
      }

      public static void ConfigureServices(IServiceCollection services)
      {
         var secretKey = FIConfigurationRoot.GetConnectionString("SecretKeyToken");
         var key = Encoding.ASCII.GetBytes(secretKey);

         services.AddCors();
         services.AddControllers();
         services.AddHttpContextAccessor();
         services.AddAutoMapper(typeof(AutoMapperProfile));
         services.AddScoped<IServices, UserService>();
         services.AddScoped<IRepository<UserModel>, UserRepository>();

         services.AddAuthentication(a => 
         {
            a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
         })
         .AddJwtBearer(x =>
         {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters()
            {
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(key),
               ValidateIssuer = false,
               ValidateAudience = false,
            };
         });
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
