using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Utils;
using Microsoft.EntityFrameworkCore;

namespace BaseCodeAPI.Src
{
   internal class Context : DbContext
   {
      public DbSet<PersonalModel> PESSOA { get; set; }
      public DbSet<UserModel> USUARIO { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         if (!optionsBuilder.IsConfigured)
         {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));

            optionsBuilder.UseMySql(connectionString, serverVersion);
         }
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<PersonalModel>().ToTable("pessoa"); 
         modelBuilder.Entity<UserModel>().ToTable("usuario");

         base.OnModelCreating(modelBuilder);

         modelBuilder.Entity<UserModel>().Property(parametro => parametro.Apelido).IsRequired();
         modelBuilder.Entity<UserModel>().Property(parametro => parametro.Senha).IsRequired();

         modelBuilder.Entity<UserModel>().HasData(PopularDadosUtils.Instancia().PopularUser());
      }
   }
}
