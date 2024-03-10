using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Utils;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Repositories;

namespace BaseCodeAPI.Src.Services
{
   public class UserService
   {
      private UserRepository FUserRepository { get; set; }

      public UserService()
      {
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
            return ((byte)GlobalEnum.eStatusProc.ErroProcessamento, ResponseUtils.Instancia().RetornoErrorProcess(ex.InnerException));
         }
      }

      public async Task<(byte Status, object Json)> CreateUser(UserModel AUser)
      {
         try
         {
            var usersObject = await FUserRepository.CreateUser(AUser);

            return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().RetornoOk(usersObject));

         }
         catch (Exception ex)
         {
            return ((byte)GlobalEnum.eStatusProc.ErroProcessamento, ResponseUtils.Instancia().RetornoErrorProcess(ex.InnerException));
         }
      }
   }
}
