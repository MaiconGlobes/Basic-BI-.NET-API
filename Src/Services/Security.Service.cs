
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

      /// <summary>
      /// Cria um novo token JWT com base nas informações do modelo fornecido.
      /// </summary>
      /// <typeparam name="T">O tipo do modelo.</typeparam>
      /// <param name="AModel">O modelo utilizado para criar o token.</param>
      /// <returns>Uma tarefa representando a operação assíncrona, retornando uma tupla contendo o status e o objeto JSON correspondente.</returns>
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

                  return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().ReturnOk(tokenUserModelDto));
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

      /// <summary>
      /// Criptografa uma senha usando o algoritmo de hash SHA256 com uma chave secreta.
      /// </summary>
      /// <param name="password">A senha a ser criptografada.</param>
      /// <returns>A senha criptografada.</returns>
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

      /// <summary>
      /// Gera um token JWT com base nas informações do modelo de usuário e no tempo de expiração especificado.
      /// </summary>
      /// <param name="AModel">O modelo de usuário contendo as informações necessárias para gerar o token.</param>
      /// <param name="AExpirationMinutes">O tempo de expiração do token em minutos.</param>
      /// <returns>O token JWT gerado como uma string.</returns>
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

      /// <summary>
      /// Gera um token JWT com base nas informações do modelo de token do usuário.
      /// </summary>
      /// <param name="AModel">O modelo de token do usuário contendo as informações necessárias para gerar o token.</param>
      /// <returns>O token JWT gerado como uma string.</returns>
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

      /// <summary>
      /// Compara uma senha em texto plano com uma senha criptografada para verificar se são iguais.
      /// </summary>
      /// <param name="APassword">A senha em texto plano a ser comparada.</param>
      /// <param name="AHashedPassword">A senha criptografada a ser comparada.</param>
      /// <returns>True se as senhas forem iguais, false caso contrário.</returns>
      internal bool CompareHasch(string APassword, string AHashedPassword)
      {
         string hashedInput = this.EncryptPassword(APassword);
         return string.Equals(hashedInput, AHashedPassword, StringComparison.OrdinalIgnoreCase);
      }

      /// <summary>
      /// Verifica se um token JWT expirou.
      /// </summary>
      /// <param name="AToken">O token JWT a ser verificado.</param>
      /// <returns>True se o token estiver expirado, false caso contrário.</returns>
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
