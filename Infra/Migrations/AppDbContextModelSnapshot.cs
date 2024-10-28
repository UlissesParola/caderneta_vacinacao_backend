﻿// <auto-generated />
using System;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infra.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Core.Entities.Dependente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.Property<DateOnly>("DataNascimento")
                        .HasColumnType("date");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Dependentes", (string)null);
                });

            modelBuilder.Entity("Core.Entities.DoseRecomendada", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("IdadeParaAplicacaoEmMeses")
                        .HasColumnType("integer");

                    b.Property<int>("Numero")
                        .HasColumnType("integer");

                    b.Property<Guid>("VacinaId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("VacinaId");

                    b.ToTable("DosesRecomendadas", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("c336219a-2354-407b-8db5-c72c3e236c13"),
                            IdadeParaAplicacaoEmMeses = 0,
                            Numero = 1,
                            VacinaId = new Guid("406631b0-6df9-4d2c-bce4-d47bb64750a2")
                        },
                        new
                        {
                            Id = new Guid("30949a17-aa7d-45ef-9976-63ab529e2ac4"),
                            IdadeParaAplicacaoEmMeses = 0,
                            Numero = 1,
                            VacinaId = new Guid("9088c37a-e025-4917-87cb-152d3f71601e")
                        },
                        new
                        {
                            Id = new Guid("2351ea36-2b3f-498d-b9bc-1f01ac607121"),
                            IdadeParaAplicacaoEmMeses = 1,
                            Numero = 2,
                            VacinaId = new Guid("9088c37a-e025-4917-87cb-152d3f71601e")
                        },
                        new
                        {
                            Id = new Guid("fa0dd5ce-289e-456b-bbb9-e0f6f6c18427"),
                            IdadeParaAplicacaoEmMeses = 6,
                            Numero = 3,
                            VacinaId = new Guid("9088c37a-e025-4917-87cb-152d3f71601e")
                        },
                        new
                        {
                            Id = new Guid("a62e81ce-01cf-43f4-bf0b-dd211279250e"),
                            IdadeParaAplicacaoEmMeses = 2,
                            Numero = 1,
                            VacinaId = new Guid("ac2e603b-4bf2-4ea5-9f76-df057bbfa1ba")
                        },
                        new
                        {
                            Id = new Guid("067d862c-0d3a-48d5-9e3e-06b30e592116"),
                            IdadeParaAplicacaoEmMeses = 4,
                            Numero = 2,
                            VacinaId = new Guid("ac2e603b-4bf2-4ea5-9f76-df057bbfa1ba")
                        },
                        new
                        {
                            Id = new Guid("968da4c8-2a86-493e-9e2b-eae21137adea"),
                            IdadeParaAplicacaoEmMeses = 6,
                            Numero = 3,
                            VacinaId = new Guid("ac2e603b-4bf2-4ea5-9f76-df057bbfa1ba")
                        },
                        new
                        {
                            Id = new Guid("e35e3ea6-7580-4f72-9804-8432a20d4d71"),
                            IdadeParaAplicacaoEmMeses = 2,
                            Numero = 1,
                            VacinaId = new Guid("78257eb4-c6cf-489e-af85-ad97bc9c16c1")
                        },
                        new
                        {
                            Id = new Guid("ed1cd1e7-5a3a-4f47-b75c-03835c747833"),
                            IdadeParaAplicacaoEmMeses = 4,
                            Numero = 2,
                            VacinaId = new Guid("78257eb4-c6cf-489e-af85-ad97bc9c16c1")
                        },
                        new
                        {
                            Id = new Guid("194bd841-345f-42f5-a4c8-8dd6d4d867b5"),
                            IdadeParaAplicacaoEmMeses = 2,
                            Numero = 1,
                            VacinaId = new Guid("716beece-7cb7-4cee-adc1-bde6c8ce2f87")
                        },
                        new
                        {
                            Id = new Guid("4ad27030-54b5-4e33-94bd-6d63c0d51407"),
                            IdadeParaAplicacaoEmMeses = 4,
                            Numero = 2,
                            VacinaId = new Guid("716beece-7cb7-4cee-adc1-bde6c8ce2f87")
                        },
                        new
                        {
                            Id = new Guid("c2899c11-d2cd-4f7b-8f90-f572bebfea88"),
                            IdadeParaAplicacaoEmMeses = 12,
                            Numero = 3,
                            VacinaId = new Guid("716beece-7cb7-4cee-adc1-bde6c8ce2f87")
                        },
                        new
                        {
                            Id = new Guid("23dc3152-6860-4854-b521-6ab99fa5460c"),
                            IdadeParaAplicacaoEmMeses = 3,
                            Numero = 1,
                            VacinaId = new Guid("31d1ee53-1b9a-4b28-a9b1-f995e706762c")
                        },
                        new
                        {
                            Id = new Guid("90ead3db-3d55-47e2-b601-9c13e26f233b"),
                            IdadeParaAplicacaoEmMeses = 5,
                            Numero = 2,
                            VacinaId = new Guid("31d1ee53-1b9a-4b28-a9b1-f995e706762c")
                        },
                        new
                        {
                            Id = new Guid("de7f606f-f1d7-4cea-9b9f-c82144dc8a61"),
                            IdadeParaAplicacaoEmMeses = 9,
                            Numero = 1,
                            VacinaId = new Guid("0b78503f-90bd-4f82-8347-e9849fe55259")
                        },
                        new
                        {
                            Id = new Guid("cc37076f-887a-44b6-998d-fdae03053fae"),
                            IdadeParaAplicacaoEmMeses = 12,
                            Numero = 1,
                            VacinaId = new Guid("b9455192-3aed-4df0-8e53-e47948c339b7")
                        },
                        new
                        {
                            Id = new Guid("a66b1b7b-12db-4aeb-8468-844437461019"),
                            IdadeParaAplicacaoEmMeses = 15,
                            Numero = 2,
                            VacinaId = new Guid("b9455192-3aed-4df0-8e53-e47948c339b7")
                        },
                        new
                        {
                            Id = new Guid("b737047f-58af-4bbf-ad80-4b00d7b8f33a"),
                            IdadeParaAplicacaoEmMeses = 12,
                            Numero = 1,
                            VacinaId = new Guid("26e44612-330e-46de-8eb5-9bf4e71b9f43")
                        },
                        new
                        {
                            Id = new Guid("f40c7b4b-c821-4dd7-b2a2-b02da57367cc"),
                            IdadeParaAplicacaoEmMeses = 15,
                            Numero = 1,
                            VacinaId = new Guid("e1b837e7-8a8d-4001-af1e-3addad46723d")
                        },
                        new
                        {
                            Id = new Guid("e043c764-3caf-4001-a03c-73bb9d1f2422"),
                            IdadeParaAplicacaoEmMeses = 15,
                            Numero = 1,
                            VacinaId = new Guid("de168e1e-cb06-46b8-8a47-ec2118c5ff96")
                        },
                        new
                        {
                            Id = new Guid("b88a72f8-7292-4ecc-88a9-f79a1eb5893a"),
                            IdadeParaAplicacaoEmMeses = 48,
                            Numero = 2,
                            VacinaId = new Guid("de168e1e-cb06-46b8-8a47-ec2118c5ff96")
                        },
                        new
                        {
                            Id = new Guid("c7de7599-9bb5-420c-ab97-7042e186d4e6"),
                            IdadeParaAplicacaoEmMeses = 144,
                            Numero = 3,
                            VacinaId = new Guid("de168e1e-cb06-46b8-8a47-ec2118c5ff96")
                        },
                        new
                        {
                            Id = new Guid("7d9d8824-9f9e-418f-aa8d-c91ac9926de1"),
                            IdadeParaAplicacaoEmMeses = 15,
                            Numero = 1,
                            VacinaId = new Guid("87a537c3-5059-49a1-92be-6e2e82ac9c77")
                        });
                });

            modelBuilder.Entity("Core.Entities.RegistroVacina", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("DataAplicacao")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("DataProximaDose")
                        .HasColumnType("date");

                    b.Property<Guid>("DependenteId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DoseRecomendadaId")
                        .HasColumnType("uuid");

                    b.Property<string>("Laboratorio")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Lote")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("NomeAplicador")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("UnidadeSaude")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<Guid>("VacinaId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DependenteId");

                    b.HasIndex("DoseRecomendadaId");

                    b.HasIndex("VacinaId");

                    b.ToTable("RegistrosVacinas", (string)null);
                });

            modelBuilder.Entity("Core.Entities.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.Property<DateOnly>("DataNascimento")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Sexo")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.ToTable("Usuarios", (string)null);
                });

            modelBuilder.Entity("Core.Entities.UsuarioDependente", b =>
                {
                    b.Property<string>("UsuarioId")
                        .HasColumnType("text");

                    b.Property<Guid>("DependenteId")
                        .HasColumnType("uuid");

                    b.HasKey("UsuarioId", "DependenteId");

                    b.HasIndex("DependenteId");

                    b.ToTable("UsuariosDependentes", (string)null);
                });

            modelBuilder.Entity("Core.Entities.Vacina", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Descricao")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("Vacinas", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("406631b0-6df9-4d2c-bce4-d47bb64750a2"),
                            Descricao = "Protege contra formas graves de tuberculose.",
                            Nome = "BCG"
                        },
                        new
                        {
                            Id = new Guid("9088c37a-e025-4917-87cb-152d3f71601e"),
                            Descricao = "Protege contra hepatite B.",
                            Nome = "Hepatite B"
                        },
                        new
                        {
                            Id = new Guid("ac2e603b-4bf2-4ea5-9f76-df057bbfa1ba"),
                            Descricao = "Protege contra difteria, tétano, coqueluche, hepatite B e Haemophilus influenzae tipo B.",
                            Nome = "Pentavalente"
                        },
                        new
                        {
                            Id = new Guid("78257eb4-c6cf-489e-af85-ad97bc9c16c1"),
                            Descricao = "Protege contra gastroenterite causada por rotavírus.",
                            Nome = "Rotavírus"
                        },
                        new
                        {
                            Id = new Guid("716beece-7cb7-4cee-adc1-bde6c8ce2f87"),
                            Descricao = "Protege contra doenças causadas por pneumococo, como pneumonia, otite e meningite.",
                            Nome = "Pneumocócica 10-valente"
                        },
                        new
                        {
                            Id = new Guid("31d1ee53-1b9a-4b28-a9b1-f995e706762c"),
                            Descricao = "Protege contra meningite causada pelo meningococo C.",
                            Nome = "Meningocócica C"
                        },
                        new
                        {
                            Id = new Guid("0b78503f-90bd-4f82-8347-e9849fe55259"),
                            Descricao = "Protege contra a febre amarela.",
                            Nome = "Febre Amarela"
                        },
                        new
                        {
                            Id = new Guid("b9455192-3aed-4df0-8e53-e47948c339b7"),
                            Descricao = "Protege contra sarampo, caxumba e rubéola.",
                            Nome = "Tríplice Viral (SCR)"
                        },
                        new
                        {
                            Id = new Guid("26e44612-330e-46de-8eb5-9bf4e71b9f43"),
                            Descricao = "Protege contra hepatite A.",
                            Nome = "Hepatite A"
                        },
                        new
                        {
                            Id = new Guid("e1b837e7-8a8d-4001-af1e-3addad46723d"),
                            Descricao = "Protege contra sarampo, caxumba, rubéola e varicela.",
                            Nome = "Tetraviral"
                        },
                        new
                        {
                            Id = new Guid("de168e1e-cb06-46b8-8a47-ec2118c5ff96"),
                            Descricao = "Protege contra difteria, tétano e coqueluche.",
                            Nome = "DTP"
                        },
                        new
                        {
                            Id = new Guid("87a537c3-5059-49a1-92be-6e2e82ac9c77"),
                            Descricao = "Protege contra a varicela (catapora).",
                            Nome = "Varicela"
                        });
                });

            modelBuilder.Entity("Infra.Identity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Core.Entities.DoseRecomendada", b =>
                {
                    b.HasOne("Core.Entities.Vacina", "Vacina")
                        .WithMany("DosesRecomendadas")
                        .HasForeignKey("VacinaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vacina");
                });

            modelBuilder.Entity("Core.Entities.RegistroVacina", b =>
                {
                    b.HasOne("Core.Entities.Dependente", "Dependente")
                        .WithMany("RegistrosVacinas")
                        .HasForeignKey("DependenteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.DoseRecomendada", "DoseRecomendada")
                        .WithMany()
                        .HasForeignKey("DoseRecomendadaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Core.Entities.Vacina", "Vacina")
                        .WithMany()
                        .HasForeignKey("VacinaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dependente");

                    b.Navigation("DoseRecomendada");

                    b.Navigation("Vacina");
                });

            modelBuilder.Entity("Core.Entities.Usuario", b =>
                {
                    b.HasOne("Infra.Identity.ApplicationUser", null)
                        .WithOne("Usuario")
                        .HasForeignKey("Core.Entities.Usuario", "ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Entities.UsuarioDependente", b =>
                {
                    b.HasOne("Core.Entities.Dependente", "Dependente")
                        .WithMany("UsuarioDependente")
                        .HasForeignKey("DependenteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Usuario", "Usuario")
                        .WithMany("UsuarioDependente")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dependente");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Infra.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Infra.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infra.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Infra.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Entities.Dependente", b =>
                {
                    b.Navigation("RegistrosVacinas");

                    b.Navigation("UsuarioDependente");
                });

            modelBuilder.Entity("Core.Entities.Usuario", b =>
                {
                    b.Navigation("UsuarioDependente");
                });

            modelBuilder.Entity("Core.Entities.Vacina", b =>
                {
                    b.Navigation("DosesRecomendadas");
                });

            modelBuilder.Entity("Infra.Identity.ApplicationUser", b =>
                {
                    b.Navigation("Usuario")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
