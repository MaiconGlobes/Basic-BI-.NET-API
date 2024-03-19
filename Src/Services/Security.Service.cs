
using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Models;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Repositories;
using BaseCodeAPI.Src.Utils;
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

      internal async Task<(byte Status, object Json)> CreateNewToken<T>(T AModel)
      {
         try
         {
            var userRepository    = new UserRepository();
            var tokenUserModelDto = AModel as TokenUserModelDto;
            var tokenHandler      = new JwtSecurityTokenHandler();
            var jwtSecurityToken  = tokenHandler.ReadJwtToken(tokenUserModelDto.Token);

            string email = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "email")?.Value;
            string senha = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "secret")?.Value;

            var user = new UserModel { Email = email, Senha = senha };
            user.Senha = this.EncryptPassword(senha);

            var usersObject = await userRepository.GetOneRegisterAsync(user);

            if (usersObject != null)
            {
               if (!this.IsTokenExpired(usersObject.Refresh_token))
               {
                  tokenUserModelDto.Token = this.GenerateToken(tokenUserModelDto);

                  return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().RetornoOk(tokenUserModelDto));
               }

               return UtilsClass.New().ProcessExceptionMessage(new Exception("Usuário não autorizado"));
            }

            return UtilsClass.New().ProcessExceptionMessage(new Exception("Token enviado é inválido"));
         }
         catch (Exception ex)
         {
            return UtilsClass.New().ProcessExceptionMessage(ex);
         }
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

      internal virtual string GenerateToken(UserModelDto AModel, int AExpirationMinutes)
      {
         var secretKey = ConfigurationModel.New().FIConfigRoot.GetConnectionString("SecretKeyToken");
         var tokenHandler = new JwtSecurityTokenHandler();
         var key = Encoding.ASCII.GetBytes(secretKey);
         var tokenDescriptor = new SecurityTokenDescriptor()
         {
            Subject = new ClaimsIdentity(new Claim[]
            {
               new (ClaimTypes.Email, AModel.Email),
               new ("secret", AModel.Senha),
            }),
            Expires = DateTime.UtcNow.AddMinutes(AExpirationMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
         };

         var token = tokenHandler.CreateToken(tokenDescriptor);
         return tokenHandler.WriteToken(token);
      }

      internal virtual string GenerateToken(TokenUserModelDto AModel)
      {
         var secretKey        = ConfigurationModel.New().FIConfigRoot.GetConnectionString("SecretKeyToken");
         var tokenHandler     = new JwtSecurityTokenHandler();
         var jwtSecurityToken = tokenHandler.ReadJwtToken(AModel.Token);
     
         var key   = Encoding.ASCII.GetBytes(secretKey);
         var email = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "email")?.Value;
         var senha = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "secret")?.Value;

         var tokenDescriptor = new SecurityTokenDescriptor()
         {
            Subject = new ClaimsIdentity(new Claim[]
            {
               new (ClaimTypes.Email, email),
               new ("secret", senha),
            }),
            Expires = DateTime.UtcNow.AddSeconds(60),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
         };

         var token = tokenHandler.CreateToken(tokenDescriptor);
         return tokenHandler.WriteToken(token);
      }

      internal bool CompareHasch(string APassword, string AHashedPassword)
      {
         string hashedInput = this.EncryptPassword(APassword);
         return string.Equals(hashedInput, AHashedPassword, StringComparison.OrdinalIgnoreCase);
      }

      internal bool IsTokenExpired(string AToken)
      {
         try
         {
            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken = tokenHandler.ReadJwtToken(AToken);

            if (jwtSecurityToken.ValidTo > DateTime.UtcNow)
               return false;

            return true;
         }
         catch
         {
            return true;
         }
      }
   }
}
