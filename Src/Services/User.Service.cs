using AutoMapper;
using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Repositories;
using BaseCodeAPI.Src.Utils;

namespace BaseCodeAPI.Src.Services
{
   public class UserService
   {
      private UserRepository FUserRepository { get; set; }
      private IMapper FMapper { get; set; }

      public UserService(IMapper AMapper)
      {
         this.FMapper = AMapper;
         FUserRepository = new();
      }

      public async Task<(byte Status, object Json)> GetUserAllAsync()
      {
         try
         {
            var usersObject = await FUserRepository.GetUserAllAsync();

            return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().RetornoOk(usersObject));

         }
         catch (Exception ex)
         {
            return ((byte)GlobalEnum.eStatusProc.ErroProcessamento, ResponseUtils.Instancia().RetornoErrorProcess(ex.InnerException != null ? ex.InnerException : ex));
         }
      }

      public async Task<(byte Status, object Json)> CreateUser(UserModelDto AUser)
      {
         try
         {
            var user = this.FMapper.Map<UserModel>(AUser);
            var person = this.FMapper.Map<PersonModel>(AUser?.Pessoa);

            if (user != null || person != null)
            {
               user.Pessoa = person!;

               int rowsAffect = await FUserRepository.CreateUser(user);

               if (rowsAffect > 0)
               {
                  AUser.PessoaId = person.Id;
                  AUser.Senha = null;
                  return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().RetornoOk(AUser));
               }
            }

            throw new Exception("Houve um erro ao salvar o usuário.");
         }
         catch (Exception ex)
         {
            return ((byte)GlobalEnum.eStatusProc.ErroProcessamento, ResponseUtils.Instancia().RetornoErrorProcess(ex.InnerException != null ? ex.InnerException : ex));
         }
      }
   }
}
