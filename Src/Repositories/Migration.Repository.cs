using Microsoft.EntityFrameworkCore;

namespace BaseCodeAPI.Src.Repositories
{
   internal class MigrationRepository
   {
      private RepositoryBase FRepository { get; set; }

      internal MigrationRepository()
      {
         FRepository = new();
      }

      /// <summary>
      /// Aplica as migrações pendentes no banco de dados.
      /// </summary>
      internal void ApplyMigrate()
      {
         this.FRepository.MigrationApply();
      }

      /// <summary>
      /// Reverte todas as migrações aplicadas no banco de dados, excluindo o histórico de migrações.
      /// </summary>
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
