using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Middlewares;
using BaseCodeAPI.Src.Models;
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
      public static void Main(string[] args)
      {
         var builder = WebApplication.CreateBuilder(args);

         ConfigServices(builder.Services);

         var app = builder.Build();

         app.UseCors(x => x.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());

         app.TokenFailureMiddlewareBuilder();   

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
         services.AddScoped<IToken, TokenService>();
         services.AddScoped<IServices, UserService>();
         services.AddScoped<IRepository<UserModel>, UserRepository>();

         var secretKey = ConfigurationModel.New().FIConfigRoot.GetConnectionString("SecretKeyToken");
         var key = Encoding.ASCII.GetBytes(secretKey);

         services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
         {
            options.TokenValidationParameters = new TokenValidationParameters
            {
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(key),
               ValidateIssuer = false,
               ValidateAudience = false,
               RequireExpirationTime = true,
               ValidateLifetime = true,
            };
         });

         services.AddAuthorization();
      }
   }
}
