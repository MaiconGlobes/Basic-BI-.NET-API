using BaseCodeAPI.Src.Models;
using BaseCodeAPI.Src.Utils;
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
               var secretKey = ConfigurationModel.New().FIConfigRoot.GetConnectionString("SecretKeyToken");
               var key       = Encoding.ASCII.GetBytes(secretKey);

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
               catch (Exception ex)
               {
                  await AContext.HttpContext.Response.WriteAsJsonAsync(ResponseUtils.Instancia().RetornoErrorProcess(ex));
                  return;
               }
            }
            else
            {
               await AContext.HttpContext.Response.WriteAsJsonAsync(ResponseUtils.Instancia().RetornoUnauthorized(new Exception("Token inválido!")));
               return;
            }
         }

         await ANext();
      }
   }
}
