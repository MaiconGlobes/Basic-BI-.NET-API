using AutoMapper;
using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Interfaces;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Utils;

namespace BaseCodeAPI.Src.Services
{
   public class UserService : IServices
   {
      private IMapper FMapper { get; set; }
      private IRepository<UserModel> FIRepository { get; set; }

      public UserService(IMapper AIMapper, IRepository<UserModel> AiRepository)
      {
         this.FMapper = AIMapper;
         this.FIRepository = AiRepository;
      }

      public async Task<(byte Status, object Json)> GetAllRegistersAsync()
      {
         try
         {
            var usersObject = await FIRepository.GetAllRegisterAsync();

            return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().RetornoOk(usersObject));

         }
         catch (Exception ex)
         {
            return UtilsClass.New().ProcessExceptionDatabase(ex);
         }
      }

      public async Task<(byte Status, object Json)> CreateRegisterAsync<T>(T AModel)
      {
         try
         {
            var userModelDto = AModel as UserModelDto;

            var user = this.FMapper.Map<UserModel>(userModelDto);
            var person = this.FMapper.Map<PersonModel>(userModelDto.Pessoa);

            if (user != null || person != null)
            {
               user.Senha         = UtilsClass.New().EncryptPassword(userModelDto.Senha);
               user.Refresh_token = UtilsClass.New().GenerateToken(userModelDto);
               user.Person        = person!;

               int rowsAffect = await FIRepository.CreateRegisterAsync(user);

               if (rowsAffect > 0)
               {
                  userModelDto.PessoaId      = person.Id;
                  userModelDto.Senha         = null;
                  userModelDto.Token = user.Refresh_token;

                  return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().RetornoOk(userModelDto));
               }
            }

            throw new Exception("Houve um erro ao salvar o usuário.");
         }
         catch (Exception ex)
         {
            return UtilsClass.New().ProcessExceptionDatabase(ex);
         }
      }
   }
}
