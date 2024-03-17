
using BaseCodeAPI.Src.Models;
using BaseCodeAPI.Src.Models.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BaseCodeAPI.Src.Services
{
   public class SecurityService
   {
      internal static SecurityService FInstancia { get; set; }

      internal static SecurityService New()
      {
         FInstancia ??= new SecurityService();
         return FInstancia;
      }

      internal string EncryptPassword(string password)
      {
         var secretKeyPassword = ConfigurationModel.New().FIConfigRoot.GetConnectionString("SecretKeyPassword");

         using (SHA256 sha256Hash = SHA256.Create())
         {
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            var builder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
               builder.Append(bytes[i].ToString(secretKeyPassword));

            return builder.ToString();
         }
      }

      internal string GenerateToken(UserModelDto AUser)
      {
         var secretKey = ConfigurationModel.New().FIConfigRoot.GetConnectionString("SecretKeyToken");
         var tokenHandler = new JwtSecurityTokenHandler();
         var key = Encoding.ASCII.GetBytes(secretKey);
         var tokenDescriptor = new SecurityTokenDescriptor()
         {
            Subject = new ClaimsIdentity(new Claim[]
               {
               new (ClaimTypes.Role, (AUser.Refresh_token?.Length > 0 ? AUser.Refresh_token : AUser.Email)),
               }),
            Expires = DateTime.UtcNow.AddSeconds(5),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
         };

         var token = tokenHandler.CreateToken(tokenDescriptor);
         return tokenHandler.WriteToken(token);
      }

      internal string GenerateToken2(TokenUserModelDto AUser)
      {
         var secretKey = ConfigurationModel.New().FIConfigRoot.GetConnectionString("SecretKeyToken");
         var tokenHandler = new JwtSecurityTokenHandler();
         var key = Encoding.ASCII.GetBytes(secretKey);
         var tokenDescriptor = new SecurityTokenDescriptor()
         {
            Subject = new ClaimsIdentity(new Claim[]
               {
               new (ClaimTypes.Role, (AUser.Token?.Length > 0 ? AUser.Token : AUser.Email)),
               }),
            Expires = DateTime.UtcNow.AddSeconds(5),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
         };

         var token = tokenHandler.CreateToken(tokenDescriptor);
         return tokenHandler.WriteToken(token);
      }

      internal bool ComparePassword(string APassword, string AHashedPassword)
      {
         string hashedInput = this.EncryptPassword(APassword);
         return string.Equals(hashedInput, AHashedPassword, StringComparison.OrdinalIgnoreCase);
      }
   }
}
