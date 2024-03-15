using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Models.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BaseCodeAPI.Src.Utils
{
   internal class UtilsClass
   {
      internal static UtilsClass FInstancia { get; set; }
      private IConfigurationRoot FIConfigurationRoot { get; set; }

      internal static UtilsClass New()
      {
         FInstancia ??= new UtilsClass();
         return FInstancia;
      }

         public UtilsClass()
         {
            this.FIConfigurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settingsconfig.json")
                .Build();
   }

         internal RequestDelegate ValidatePathUserAll(HttpContext context, RequestDelegate next, string uri)
      {
         var path = context.Request.Path;

         if (!path.HasValue || !path.Value.Contains(uri))
         {
            return next;
         }

         var segments = path.Value.Split('/');

         if (segments.Length > 3)
         {
            throw new Exception("A URL não contém o formato esperado.");
         }

         return null;
      }

      private Func<Exception, object> GetDuplicateEntryErrorFunc()
      {
         return ex => {
            int indexForKey = ex.InnerException.Message.IndexOf("for key");
            string errorMessage = ex.InnerException.Message.Substring(0, indexForKey).Trim();
            errorMessage = errorMessage.Replace("Duplicate entry", "Registro já cadastrado para");

            var exceptionReturn = new Exception(errorMessage);

            return ResponseUtils.Instancia().RetornoDuplicated(exceptionReturn);
         };
      }

      internal (byte Status, object Json) ProcessExceptionDatabase(Exception ex)
      {
         Dictionary<string, (byte, Func<Exception, object>)> errorMappings = new Dictionary<string, (byte, Func<Exception, object>)>
         {
            { "Duplicate entry", ((byte)GlobalEnum.eStatusProc.RegistroDuplicado, GetDuplicateEntryErrorFunc())},
         };

         if (ex.InnerException != null)
         {
            string message = ex.InnerException.Message;

            foreach (var kvp in errorMappings)
            {
               if (message.Contains(kvp.Key))
               {
                  var (status, func) = kvp.Value;
                  return (status, func(ex));
               }
            }
         }

         return ((byte)GlobalEnum.eStatusProc.ErroProcessamento, ResponseUtils.Instancia().RetornoErrorProcess(ex));
      }

      internal string EncryptPassword(string password)
      {
         var secretKeyPassword = this.FIConfigurationRoot.GetConnectionString("SecretKeyPassword");

         using (SHA256 sha256Hash = SHA256.Create())
         {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new();

            for (int i = 0; i < bytes.Length; i++)
            {
               builder.Append(bytes[i].ToString(secretKeyPassword));
            }

            return builder.ToString();
         }
      }

      internal bool ComparePassword(string APassword, string AHashedPassword)
      {
         string hashedInput = this.EncryptPassword(APassword);
         return string.Equals(hashedInput, AHashedPassword, StringComparison.OrdinalIgnoreCase);
      }

      internal string GenerateToken(UserModelDto AUser)
      {
         var secretKey       = this.FIConfigurationRoot.GetConnectionString("SecretKeyToken");
         var tokenHandler    = new JwtSecurityTokenHandler();
         var key             = Encoding.ASCII.GetBytes(secretKey);
         var tokenDescriptor = new SecurityTokenDescriptor()
         {
            Subject = new ClaimsIdentity(new Claim[]
               {
               new (ClaimTypes.Name, AUser.Apelido.ToString()),
               new (ClaimTypes.Role, AUser.Email.ToString()),
               }),
            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
         };

         var token = tokenHandler.CreateToken(tokenDescriptor);
         return tokenHandler.WriteToken(token);
      }

   }
}
