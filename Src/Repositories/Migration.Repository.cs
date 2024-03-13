using Microsoft.EntityFrameworkCore;

namespace BaseCodeAPI.Src.Repositories
{
   internal class MigrationRepository
   {
      private Repository FRepository { get; set; }

      internal MigrationRepository()
      {
         FRepository = new();
      }

      internal void ApplyMigrate()
      {
         this.FRepository.MigrationApply();
      }

      internal void RevertAllMigrations()
      {
         var dbContext = new Context();

         var appliedMigrations = dbContext.Database.GetAppliedMigrations();

         foreach (var migration in appliedMigrations)
         {
            dbContext.Database.ExecuteSqlRaw($"DELETE FROM __EFMigrationsHistory WHERE MigrationId = '{migration}'");
         }

         dbContext.Database.EnsureDeleted();
      }
   }
}
