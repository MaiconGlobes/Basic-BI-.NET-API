using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Utils;
using Microsoft.EntityFrameworkCore;

namespace BaseCodeAPI.Src
{
   internal class Context : DbContext
   {
      public DbSet<PersonalModel> PESSOA { get; set; }
      public DbSet<UserModel> USUARIO { get; set; }
      public DbSet<ClientModel> CLIENTE { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         if (!optionsBuilder.IsConfigured)
         {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("env_config.json")
               .Build();

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
            var mySQLConnection = configuration.GetConnectionString("MySQLConnection");

            optionsBuilder.UseMySql(mySQLConnection, serverVersion);
         }
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<PersonalModel>().ToTable("pessoa");
         modelBuilder.Entity<UserModel>().ToTable("usuario");
         modelBuilder.Entity<ClientModel>().ToTable("cliente");

         modelBuilder.Entity<PersonalModel>().Property(personal => personal.Apelido).IsRequired();
         modelBuilder.Entity<PersonalModel>().Property(personal => personal.Email).IsRequired();
         modelBuilder.Entity<PersonalModel>().Property(personal => personal.Municipio).IsRequired();
         modelBuilder.Entity<PersonalModel>().Property(personal => personal.Uf).IsRequired();

         modelBuilder.Entity<UserModel>().Property(user => user.Apelido).IsRequired();
         modelBuilder.Entity<UserModel>().Property(user => user.Senha).IsRequired();

         modelBuilder.Entity<ClientModel>().Property(client => client.Cpf_cnpj).IsRequired();

         base.OnModelCreating(modelBuilder);
      }
   }
}
