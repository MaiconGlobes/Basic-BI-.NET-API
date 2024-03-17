using AutoMapper;
using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Repositories;
using BaseCodeAPI.Src.Utils;

namespace BaseCodeAPI.Src.Services
{
   public class TokenService : IToken
   {
      private IMapper FMapper { get; set; }
      private IRepository<UserModel> FIRepository { get; set; }

      public TokenService(IMapper AIMapper, IRepository<UserModel> AiRepository)
      {
         this.FMapper = AIMapper;
         this.FIRepository = AiRepository;
      }

      public async Task<(byte Status, object Json)> CreateNewToken<T>(T AModel)
      {
         try
         {
            var tokenUserModelDto = AModel as TokenUserModelDto;
            var user = this.FMapper.Map<UserModel>(tokenUserModelDto);

            user.Senha = SecurityService.New().EncryptPassword(tokenUserModelDto.Senha);

            var usersObject = await FIRepository.GetOneRegisterAsync(user);

            if (usersObject != null)
            { 
               tokenUserModelDto.Token = SecurityService.New().GenerateToken2(tokenUserModelDto);
               tokenUserModelDto.Email = null;
               tokenUserModelDto.Senha = null;

               return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().RetornoOk(tokenUserModelDto));
            }
            
            return ((byte)GlobalEnum.eStatusProc.NaoLocalizado, ResponseUtils.Instancia().RetornoNotFound(new object()));
         }
         catch (Exception ex)
         {
            return UtilsClass.New().ProcessExceptionMessage(ex);
         }
      }
   }
}
