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
      public DbSet<TransporterModel> TRANSPORTER { get; set; }
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

            optionsBuilder.EnableSensitiveDataLogging()
                          .UseMySql(mySQLConnection, serverVersion).EnableSensitiveDataLogging();
         }
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {

         modelBuilder.Entity<PersonModel>().Property(person => person.Id).ValueGeneratedOnAdd();
         modelBuilder.Entity<UserModel>().Property(user => user.Id).ValueGeneratedOnAdd();

         modelBuilder.Entity<PersonModel>()
                     .HasMany(person => person.Users)
                     .WithOne(user => user.Person)
                     .HasForeignKey(user => user.PessoaId)
                     .OnDelete(DeleteBehavior.Cascade);

         modelBuilder.Entity<PersonModel>()
                     .HasMany(person => person.Addresses)
                     .WithOne(address => address.Person)
                     .HasForeignKey(address => address.PessoaId)
                     .OnDelete(DeleteBehavior.Cascade);

         modelBuilder.Entity<PersonModel>()
                     .HasOne(person => person.Client)
                     .WithOne(client => client.Person)
                     .HasForeignKey<ClientModel>(client => client.PessoaId)
                     .IsRequired()
                     .OnDelete(DeleteBehavior.Cascade);

         modelBuilder.Entity<PersonModel>()
                     .HasOne(person => person.Carrier)
                     .WithOne(carrier => carrier.Person)
                     .HasForeignKey<CarrierModel>(carrier => carrier.PessoaId)
                     .IsRequired()
                     .OnDelete(DeleteBehavior.Cascade);

         modelBuilder.Entity<PersonModel>().HasData(PopularDataUtils.Instancia().PopularPerson());
         modelBuilder.Entity<UserModel>().HasData(PopularDataUtils.Instancia().PopularUsers());
         modelBuilder.Entity<ClientModel>().HasData(PopularDataUtils.Instancia().PopularClient());
         modelBuilder.Entity<CarrierModel>().HasData(PopularDataUtils.Instancia().PopularCarrier());
         modelBuilder.Entity<AddressModel>().HasData(PopularDataUtils.Instancia().PopularAddress());

         base.OnModelCreating(modelBuilder);
      }
   }
}
