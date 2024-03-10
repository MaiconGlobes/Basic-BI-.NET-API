using BaseCodeAPI.Src.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace BaseCodeAPI.Src.Repositories
{
   public class MigrationRepository
   {
      private Repository FRepository { get; set; }

      public MigrationRepository()
      {
         FRepository = new();
      }

      public void ApplyMigrate()
      {
         this.FRepository.MigrationApply();
      }
   }
}
