using BaseCodeAPI.Src.Models.Entity;
using BaseCodeAPI.Src.Utils;
using Microsoft.EntityFrameworkCore;

namespace BaseCodeAPI.Src
{
   internal class Context : DbContext
   {
      public DbSet<PersonModel> PERSON { get; set; }
      public DbSet<UserModel> USER { get; set; }
      public DbSet<ClientModel> CLIENT { get; set; }
      public DbSet<CarrierModel> CARRIER { get; set; }
      public DbSet<AddressModel> ADDRESS { get; set; }

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
         modelBuilder.Entity<UserModel>().ToTable("usuario");
         modelBuilder.Entity<PersonModel>().ToTable("pessoa");
         modelBuilder.Entity<ClientModel>().ToTable("cliente");
         modelBuilder.Entity<CarrierModel>().ToTable("fornecedor");
         modelBuilder.Entity<AddressModel>().ToTable("endereco");

         modelBuilder.Entity<PersonModel>().Property(person => person.Id).ValueGeneratedOnAdd();
         modelBuilder.Entity<ClientModel>().Property(client => client.Id).ValueGeneratedOnAdd();

         modelBuilder.Entity<ClientModel>().HasOne(client => client.Person).WithOne().HasForeignKey<ClientModel>(client => client.PessoaId).IsRequired().OnDelete(DeleteBehavior.Cascade);
         modelBuilder.Entity<CarrierModel>().HasOne(carrier => carrier.Person).WithOne().HasForeignKey<CarrierModel>(carrier => carrier.PessoaId).IsRequired().OnDelete(DeleteBehavior.Cascade);
         modelBuilder.Entity<AddressModel>().HasOne(address => address.Person).WithMany(person => person.Addresses).HasForeignKey(address => address.Pessoa_id);

         modelBuilder.Entity<UserModel>().HasData(PopularDataUtils.Instancia().PopularUser());
         modelBuilder.Entity<ClientModel>().HasData(PopularDataUtils.Instancia().PopularClient());
         modelBuilder.Entity<CarrierModel>().HasData(PopularDataUtils.Instancia().PopularCarrier());
         modelBuilder.Entity<AddressModel>().HasData(PopularDataUtils.Instancia().PopularAddress());

         base.OnModelCreating(modelBuilder);
      }
   }
}
