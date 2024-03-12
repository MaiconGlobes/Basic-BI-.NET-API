﻿using BaseCodeAPI.Src.Enums;
using BaseCodeAPI.Src.Utils;
using BaseCodeAPI.Src.Repositories;

namespace BaseCodeAPI.Src.Services
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

      public (byte Status, object Json) RevertAllMigration()
      {
         try
         {
            FMigrationRepository.RevertAllMigrations();

            return ((byte)GlobalEnum.eStatusProc.Sucesso, ResponseUtils.Instancia().RetornoOk(new object() { }));

         }
         catch (Exception ex)
         {
            return ((byte)GlobalEnum.eStatusProc.ErroProcessamento, ResponseUtils.Instancia().RetornoErrorProcess(ex));
         }
      }
   }
}
