using BaseCodeAPI.Src.Models;
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
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
            var mySQLConnection = ConfigurationModel.New().FIConfigRoot.GetConnectionString("MySQLConnection");

            optionsBuilder.EnableSensitiveDataLogging()
                          .UseMySql(mySQLConnection, serverVersion).EnableSensitiveDataLogging();
         }
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<PersonModel>().Property(person => person.Id).ValueGeneratedOnAdd();
         modelBuilder.Entity<UserModel>().Property(user => user.Id).ValueGeneratedOnAdd();
         modelBuilder.Entity<ClientModel>().Property(client => client.Id).ValueGeneratedOnAdd();
         modelBuilder.Entity<CarrierModel>().Property(carrier => carrier.Id).ValueGeneratedOnAdd();
         modelBuilder.Entity<AddressModel>().Property(address => address.Id).ValueGeneratedOnAdd();

         modelBuilder.Entity<PersonModel>().HasIndex(person => person.Cpf_cnpj).IsUnique();
         modelBuilder.Entity<UserModel>().HasIndex(user => user.Email).IsUnique();

         modelBuilder.Entity<PersonModel>().Property(person => person.Nome).IsRequired();
         modelBuilder.Entity<PersonModel>().Property(person => person.Cpf_cnpj).IsRequired();

         modelBuilder.Entity<UserModel>().Property(user => user.Apelido).IsRequired();
         modelBuilder.Entity<UserModel>().Property(user => user.Email).IsRequired();
         modelBuilder.Entity<UserModel>().Property(user => user.Senha).IsRequired();
         modelBuilder.Entity<UserModel>().Property(user => user.PessoaId).IsRequired();

         modelBuilder.Entity<ClientModel>().Property(client => client.PessoaId).IsRequired();
         modelBuilder.Entity<CarrierModel>().Property(carrier => carrier.PessoaId).IsRequired();
         modelBuilder.Entity<AddressModel>().Property(address => address.PessoaId).IsRequired();

         modelBuilder.Entity<PersonModel>()
                     .HasOne(user => user.User)
                     .WithOne(user => user.Person)
                     .HasForeignKey<UserModel>(user => user.PessoaId)
                     .IsRequired()
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

         modelBuilder.Entity<PersonModel>()
                     .HasOne(transporter => transporter.Transporter)
                     .WithOne(transporter => transporter.Person)
                     .HasForeignKey<TransporterModel>(transporter => transporter.PessoaId)
                     .IsRequired()
                     .OnDelete(DeleteBehavior.Cascade);

         modelBuilder.Entity<PersonModel>().HasData(PopularDataUtils.Instancia().PopularPerson());
         modelBuilder.Entity<UserModel>().HasData(PopularDataUtils.Instancia().PopularUsers());
         modelBuilder.Entity<AddressModel>().HasData(PopularDataUtils.Instancia().PopularAddress());

         base.OnModelCreating(modelBuilder);
      }
   }
}
