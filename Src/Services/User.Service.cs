using ASControllerAPI.Src.Enums;
using ASControllerAPI.Src.Utils;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Models;

namespace ASControllerAPI.Src.Services
{
   public class UserService
   {
      public (byte Status, object Json) GetUserAllAsync()
      {
         try
         {
            var usersObject = new List<UserModel>()
            {
               new()
               {
                  Id = 1,
                  Name = "Alberto Silva"
               }
            };

            return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().RetornoOk(usersObject));
         }
         catch (Exception ex)
         {
            return ((byte)GlobalEnum.eStatusProc.ErroProcessamento, ResponseUtils.Instancia().RetornoErrorProcess(ex));
         }
      }
   }
}
