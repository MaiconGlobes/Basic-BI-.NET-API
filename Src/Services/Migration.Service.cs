using ASControllerAPI.Src.Enums;
using ASControllerAPI.Src.Utils;
using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Repositories;

namespace ASControllerAPI.Src.Services
{
   public class MigrationService
   {
      private MigrationRepository FMigrationRepository { get; set; }

      public MigrationService()
      {
         FMigrationRepository = new();
      }

      public (byte Status, object Json) ApplyMigrate()
      {
         try
         {
            FMigrationRepository.ApplyMigrate();

            return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().RetornoOk(new object() {}));

         }
         catch (Exception ex)
         {
            return ((byte)GlobalEnum.eStatusProc.ErroProcessamento, ResponseUtils.Instancia().RetornoErrorProcess(ex));
         }
      }
   }
}
