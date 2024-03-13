﻿// <auto-generated />
using System;
using BaseCodeAPI.Src;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BaseCodeAPI.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.AddressModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Bairro")
                        .HasMaxLength(35)
                        .HasColumnType("varchar(35)")
                        .HasColumnName("bairro");

                    b.Property<string>("Cep")
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)")
                        .HasColumnName("cep");

                    b.Property<string>("Endereco")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("endereco");

                    b.Property<string>("Municipio")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("municipio");

                    b.Property<string>("Numero")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("numero");

                    b.Property<int>("PessoaId")
                        .HasColumnType("int")
                        .HasColumnName("pessoa_id");

                    b.Property<string>("Uf")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)")
                        .HasColumnName("uf");

                    b.HasKey("Id");

                    b.HasIndex("PessoaId");

                    b.ToTable("endereco");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Bairro = "Bairro 2",
                            Cep = "13545215",
                            Endereco = "Endereco 2",
                            Municipio = "Municipio 2",
                            Numero = "1001",
                            PessoaId = 1,
                            Uf = "SP"
                        },
                        new
                        {
                            Id = 2,
                            Bairro = "Bairro 2",
                            Cep = "13545215",
                            Endereco = "Endereco 2",
                            Municipio = "Municipio 2",
                            Numero = "1001",
                            PessoaId = 1,
                            Uf = "SP"
                        },
                        new
                        {
                            Id = 3,
                            Bairro = "Bairro 3",
                            Cep = "13545215",
                            Endereco = "Endereco 3",
                            Municipio = "Municipio 3",
                            Numero = "1001",
                            PessoaId = 1,
                            Uf = "SP"
                        });
                });

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.CarrierModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("PessoaId")
                        .HasColumnType("int")
                        .HasColumnName("pessoa_id");

                    b.HasKey("Id");

                    b.HasIndex("PessoaId")
                        .IsUnique();

                    b.ToTable("fornecedor");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PessoaId = 1
                        });
                });

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.ClientModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<decimal>("Limite_credito")
                        .HasColumnType("numeric(18,2)")
                        .HasColumnName("limite_credito");

                    b.Property<int>("PessoaId")
                        .HasColumnType("int")
                        .HasColumnName("pessoa_id");

                    b.HasKey("Id");

                    b.HasIndex("PessoaId")
                        .IsUnique();

                    b.ToTable("cliente");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Limite_credito = 1000m,
                            PessoaId = 1
                        });
                });

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.PersonModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Apelido")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("varchar(35)")
                        .HasColumnName("apelido");

                    b.Property<string>("Cpf_cnpj")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("varchar(14)")
                        .HasColumnName("cpf_cnpj");

                    b.Property<string>("Telefone")
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)")
                        .HasColumnName("telefone");

                    b.HasKey("Id");

                    b.HasIndex("Cpf_cnpj")
                        .IsUnique();

                    b.ToTable("pessoa");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Apelido = "Apelido 1",
                            Cpf_cnpj = "07640402948",
                            Telefone = "19999999999"
                        });
                });

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.TransporterModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int?>("PersonId")
                        .HasColumnType("int");

                    b.Property<int>("PessoaId")
                        .HasColumnType("int")
                        .HasColumnName("pessoa_id");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("transportadora");
                });

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("email");

                    b.Property<int>("PessoaId")
                        .HasColumnType("int")
                        .HasColumnName("pessoa_id");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("senha");

                    b.Property<string>("Token")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("token");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PessoaId");

                    b.ToTable("usuario");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "Email1@example.com",
                            PessoaId = 1,
                            Senha = "Senha 1"
                        },
                        new
                        {
                            Id = 2,
                            Email = "Email2@example.com",
                            PessoaId = 1,
                            Senha = "Senha 2"
                        });
                });

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.AddressModel", b =>
                {
                    b.HasOne("BaseCodeAPI.Src.Models.Entity.PersonModel", "Person")
                        .WithMany("Addresses")
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.CarrierModel", b =>
                {
                    b.HasOne("BaseCodeAPI.Src.Models.Entity.PersonModel", "Person")
                        .WithOne("Carrier")
                        .HasForeignKey("BaseCodeAPI.Src.Models.Entity.CarrierModel", "PessoaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.ClientModel", b =>
                {
                    b.HasOne("BaseCodeAPI.Src.Models.Entity.PersonModel", "Person")
                        .WithOne("Client")
                        .HasForeignKey("BaseCodeAPI.Src.Models.Entity.ClientModel", "PessoaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.TransporterModel", b =>
                {
                    b.HasOne("BaseCodeAPI.Src.Models.Entity.PersonModel", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.UserModel", b =>
                {
                    b.HasOne("BaseCodeAPI.Src.Models.Entity.PersonModel", "Pessoa")
                        .WithMany("Users")
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("BaseCodeAPI.Src.Models.Entity.PersonModel", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Carrier");

                    b.Navigation("Client");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
