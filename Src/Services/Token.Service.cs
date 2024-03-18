using AutoMapper;
using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Repositories;
using BaseCodeAPI.Src.Utils;
using System.IdentityModel.Tokens.Jwt;

namespace BaseCodeAPI.Src.Services
{
   public class TokenService
   {
      private IMapper FMapper { get; set; }
      private UserRepository FIRepository { get; set; }

      public TokenService(/*IMapper AIMapper, IRepository<UserModel> AiRepository*/)
      {
         //this.FMapper = AIMapper;
         this.FIRepository = new UserRepository();
      }

      public async Task<(byte Status, object Json)> CreateNewToken<T>(T AModel)
      {
         try
         {
            var tokenUserModelDto = AModel as TokenUserModelDto;

            JwtSecurityTokenHandler tokenHandler = new();
            JwtSecurityToken jwtSecurityToken    = tokenHandler.ReadJwtToken(tokenUserModelDto.Token);

            string email = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "email")?.Value;
            string senha = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "secret")?.Value;

            var user   = new UserModel { Email = email, Senha = senha };
            user.Senha = SecurityService.New().EncryptPassword(senha);

            var usersObject = await FIRepository.GetOneRegisterAsync(user);

            if (usersObject != null)
            {
               tokenUserModelDto.Token = SecurityService.New().GenerateToken2(tokenUserModelDto);

               return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().RetornoOk(tokenUserModelDto));
            }

            return UtilsClass.New().ProcessExceptionMessage(new Exception("Token enviado é inválido"));
         }
         catch (Exception ex)
         {
            return UtilsClass.New().ProcessExceptionMessage(ex);
         }
      }
   }
}
