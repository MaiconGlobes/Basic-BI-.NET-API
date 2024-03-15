using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BaseCodeAPI.Src.Middlewares
{
   public class JwtTokenFilterMiddleware : IAsyncActionFilter
   {
      public async Task OnActionExecutionAsync(ActionExecutingContext AContext, ActionExecutionDelegate ANext)
      {
         var hasAuthorizeAttribute = AContext.ActionDescriptor.EndpointMetadata
             .Any(em => em.GetType() == typeof(Microsoft.AspNetCore.Authorization.AuthorizeAttribute));

         if (hasAuthorizeAttribute)
         {
            var token = AContext.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
               IConfigurationRoot configuration = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("settingsconfig.json")
                  .Build();

               var secretKey = configuration.GetConnectionString("SecretKeyToken");
               var key = Encoding.ASCII.GetBytes(secretKey);

               var tokenHandler = new JwtSecurityTokenHandler();

               try
               {
                  tokenHandler.ValidateToken(token, new TokenValidationParameters
                  {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(key),
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     RequireExpirationTime = true,
                     ValidateLifetime = true
                  }, out SecurityToken validatedToken);

                  AContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(((JwtSecurityToken)validatedToken).Claims));
               }
               catch (Exception)
               {
                  AContext.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                  await AContext.HttpContext.Response.WriteAsync("Token inválido ou expirado.");
                  return;
               }
            }
            else
            {
               AContext.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
               await AContext.HttpContext.Response.WriteAsync("Token não fornecido.");
               return;
            }
         }

         await ANext();
      }
   }
}
