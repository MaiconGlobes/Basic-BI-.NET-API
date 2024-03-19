using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Repositories;
using BaseCodeAPI.Src.Utils;

namespace BaseCodeAPI.Src.Services
{
   public class MigrationService
   {
      private MigrationRepository FMigrationRepository { get; set; }

      public MigrationService()
      {
         FMigrationRepository = new();
      }

      /// <summary>
      /// Aplica as migrações pendentes no banco de dados e retorna um status e um objeto JSON correspondente ao resultado da operação.
      /// </summary>
      /// <returns>Uma tupla contendo o status da operação e o objeto JSON correspondente ao resultado.</returns>
      public (byte Status, object Json) ApplyMigrate()
      {
         try
         {
            FMigrationRepository.ApplyMigrate();

            return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().ReturnOk(new object() {}));

         }
         catch (Exception ex)
         {
            return UtilsClass.New().ProcessExceptionMessage(ex);
         }
      }

      /// <summary>
      /// Reverte todas as migrações aplicadas no banco de dados, excluindo o histórico de migrações, e retorna um status e um objeto JSON correspondente ao resultado da operação.
      /// </summary>
      /// <returns>Uma tupla contendo o status da operação e o objeto JSON correspondente ao resultado.</returns>
      public (byte Status, object Json) RevertAllMigration()
      {
         try
         {
            FMigrationRepository.RevertAllMigrations();

            return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().ReturnOk(new object() { }));

         }
         catch (Exception ex)
         {
            return UtilsClass.New().ProcessExceptionMessage(ex);
         }
      }
   }
}
