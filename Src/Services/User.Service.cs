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

      public async Task<(byte Status, object Json)> GetAllRegistersAsync(IHttpContextAccessor httpContextAccessor)
      {
         try
         {
            var token = (string)httpContextAccessor?.HttpContext?.Items["Token"]!;

            var usersObject = await FIRepository.GetAllRegisterAsync();

            var objReturn = new
            {
               usuario = usersObject,
               token
            };

            return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().RetornoOk(objReturn));

         }
         catch (Exception ex)
         {
            return UtilsClass.New().ProcessExceptionMessage(ex);
         }
      }

      public async Task<(byte Status, object Json)> CreateRegisterAsync<T>(T AModel)
      {
         try
         {
            var userModelDto = AModel as UserModelDto;
            var user         = this.FMapper.Map<UserModel>(userModelDto);
            var person       = this.FMapper.Map<PersonModel>(userModelDto.Pessoa);

            if (user != null || person != null)
            {
               user.Senha         = SecurityService.New().EncryptPassword(userModelDto.Senha);
               user.Refresh_token = SecurityService.New().GenerateToken(userModelDto, 10080);
               user.Person        = person!;

               int rowsAffect = await FIRepository.CreateRegisterAsync(user);

               if (rowsAffect > 0)
               {
                  userModelDto.PessoaId = person.Id;
                  userModelDto.Token    = SecurityService.New().GenerateToken(userModelDto, 1);
                  userModelDto.Senha    = null;

                  return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().RetornoOk(userModelDto));
               }
            }

            throw new Exception("Houve um erro ao salvar o usuário.");
         }
         catch (Exception ex)
         {
            return UtilsClass.New().ProcessExceptionMessage(ex);
         }
      }
   }
}
