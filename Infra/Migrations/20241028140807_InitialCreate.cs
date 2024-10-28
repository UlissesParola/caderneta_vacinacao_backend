using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dependentes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Sobrenome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    Sexo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependentes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vacinas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacinas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Sobrenome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    Sexo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ApplicationUserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DosesRecomendadas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Numero = table.Column<int>(type: "integer", nullable: false),
                    IdadeParaAplicacaoEmMeses = table.Column<int>(type: "integer", nullable: false),
                    VacinaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DosesRecomendadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DosesRecomendadas_Vacinas_VacinaId",
                        column: x => x.VacinaId,
                        principalTable: "Vacinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosDependentes",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(type: "text", nullable: false),
                    DependenteId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosDependentes", x => new { x.UsuarioId, x.DependenteId });
                    table.ForeignKey(
                        name: "FK_UsuariosDependentes_Dependentes_DependenteId",
                        column: x => x.DependenteId,
                        principalTable: "Dependentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosDependentes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistrosVacinas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DataAplicacao = table.Column<DateOnly>(type: "date", nullable: false),
                    DataProximaDose = table.Column<DateOnly>(type: "date", nullable: true),
                    Lote = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Laboratorio = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UnidadeSaude = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NomeAplicador = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    VacinaId = table.Column<Guid>(type: "uuid", nullable: false),
                    DoseRecomendadaId = table.Column<Guid>(type: "uuid", nullable: false),
                    DependenteId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosVacinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosVacinas_Dependentes_DependenteId",
                        column: x => x.DependenteId,
                        principalTable: "Dependentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistrosVacinas_DosesRecomendadas_DoseRecomendadaId",
                        column: x => x.DoseRecomendadaId,
                        principalTable: "DosesRecomendadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegistrosVacinas_Vacinas_VacinaId",
                        column: x => x.VacinaId,
                        principalTable: "Vacinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Vacinas",
                columns: new[] { "Id", "Descricao", "Nome" },
                values: new object[,]
                {
                    { new Guid("0b78503f-90bd-4f82-8347-e9849fe55259"), "Protege contra a febre amarela.", "Febre Amarela" },
                    { new Guid("26e44612-330e-46de-8eb5-9bf4e71b9f43"), "Protege contra hepatite A.", "Hepatite A" },
                    { new Guid("31d1ee53-1b9a-4b28-a9b1-f995e706762c"), "Protege contra meningite causada pelo meningococo C.", "Meningocócica C" },
                    { new Guid("406631b0-6df9-4d2c-bce4-d47bb64750a2"), "Protege contra formas graves de tuberculose.", "BCG" },
                    { new Guid("716beece-7cb7-4cee-adc1-bde6c8ce2f87"), "Protege contra doenças causadas por pneumococo, como pneumonia, otite e meningite.", "Pneumocócica 10-valente" },
                    { new Guid("78257eb4-c6cf-489e-af85-ad97bc9c16c1"), "Protege contra gastroenterite causada por rotavírus.", "Rotavírus" },
                    { new Guid("87a537c3-5059-49a1-92be-6e2e82ac9c77"), "Protege contra a varicela (catapora).", "Varicela" },
                    { new Guid("9088c37a-e025-4917-87cb-152d3f71601e"), "Protege contra hepatite B.", "Hepatite B" },
                    { new Guid("ac2e603b-4bf2-4ea5-9f76-df057bbfa1ba"), "Protege contra difteria, tétano, coqueluche, hepatite B e Haemophilus influenzae tipo B.", "Pentavalente" },
                    { new Guid("b9455192-3aed-4df0-8e53-e47948c339b7"), "Protege contra sarampo, caxumba e rubéola.", "Tríplice Viral (SCR)" },
                    { new Guid("de168e1e-cb06-46b8-8a47-ec2118c5ff96"), "Protege contra difteria, tétano e coqueluche.", "DTP" },
                    { new Guid("e1b837e7-8a8d-4001-af1e-3addad46723d"), "Protege contra sarampo, caxumba, rubéola e varicela.", "Tetraviral" }
                });

            migrationBuilder.InsertData(
                table: "DosesRecomendadas",
                columns: new[] { "Id", "IdadeParaAplicacaoEmMeses", "Numero", "VacinaId" },
                values: new object[,]
                {
                    { new Guid("067d862c-0d3a-48d5-9e3e-06b30e592116"), 4, 2, new Guid("ac2e603b-4bf2-4ea5-9f76-df057bbfa1ba") },
                    { new Guid("194bd841-345f-42f5-a4c8-8dd6d4d867b5"), 2, 1, new Guid("716beece-7cb7-4cee-adc1-bde6c8ce2f87") },
                    { new Guid("2351ea36-2b3f-498d-b9bc-1f01ac607121"), 1, 2, new Guid("9088c37a-e025-4917-87cb-152d3f71601e") },
                    { new Guid("23dc3152-6860-4854-b521-6ab99fa5460c"), 3, 1, new Guid("31d1ee53-1b9a-4b28-a9b1-f995e706762c") },
                    { new Guid("30949a17-aa7d-45ef-9976-63ab529e2ac4"), 0, 1, new Guid("9088c37a-e025-4917-87cb-152d3f71601e") },
                    { new Guid("4ad27030-54b5-4e33-94bd-6d63c0d51407"), 4, 2, new Guid("716beece-7cb7-4cee-adc1-bde6c8ce2f87") },
                    { new Guid("7d9d8824-9f9e-418f-aa8d-c91ac9926de1"), 15, 1, new Guid("87a537c3-5059-49a1-92be-6e2e82ac9c77") },
                    { new Guid("90ead3db-3d55-47e2-b601-9c13e26f233b"), 5, 2, new Guid("31d1ee53-1b9a-4b28-a9b1-f995e706762c") },
                    { new Guid("968da4c8-2a86-493e-9e2b-eae21137adea"), 6, 3, new Guid("ac2e603b-4bf2-4ea5-9f76-df057bbfa1ba") },
                    { new Guid("a62e81ce-01cf-43f4-bf0b-dd211279250e"), 2, 1, new Guid("ac2e603b-4bf2-4ea5-9f76-df057bbfa1ba") },
                    { new Guid("a66b1b7b-12db-4aeb-8468-844437461019"), 15, 2, new Guid("b9455192-3aed-4df0-8e53-e47948c339b7") },
                    { new Guid("b737047f-58af-4bbf-ad80-4b00d7b8f33a"), 12, 1, new Guid("26e44612-330e-46de-8eb5-9bf4e71b9f43") },
                    { new Guid("b88a72f8-7292-4ecc-88a9-f79a1eb5893a"), 48, 2, new Guid("de168e1e-cb06-46b8-8a47-ec2118c5ff96") },
                    { new Guid("c2899c11-d2cd-4f7b-8f90-f572bebfea88"), 12, 3, new Guid("716beece-7cb7-4cee-adc1-bde6c8ce2f87") },
                    { new Guid("c336219a-2354-407b-8db5-c72c3e236c13"), 0, 1, new Guid("406631b0-6df9-4d2c-bce4-d47bb64750a2") },
                    { new Guid("c7de7599-9bb5-420c-ab97-7042e186d4e6"), 144, 3, new Guid("de168e1e-cb06-46b8-8a47-ec2118c5ff96") },
                    { new Guid("cc37076f-887a-44b6-998d-fdae03053fae"), 12, 1, new Guid("b9455192-3aed-4df0-8e53-e47948c339b7") },
                    { new Guid("de7f606f-f1d7-4cea-9b9f-c82144dc8a61"), 9, 1, new Guid("0b78503f-90bd-4f82-8347-e9849fe55259") },
                    { new Guid("e043c764-3caf-4001-a03c-73bb9d1f2422"), 15, 1, new Guid("de168e1e-cb06-46b8-8a47-ec2118c5ff96") },
                    { new Guid("e35e3ea6-7580-4f72-9804-8432a20d4d71"), 2, 1, new Guid("78257eb4-c6cf-489e-af85-ad97bc9c16c1") },
                    { new Guid("ed1cd1e7-5a3a-4f47-b75c-03835c747833"), 4, 2, new Guid("78257eb4-c6cf-489e-af85-ad97bc9c16c1") },
                    { new Guid("f40c7b4b-c821-4dd7-b2a2-b02da57367cc"), 15, 1, new Guid("e1b837e7-8a8d-4001-af1e-3addad46723d") },
                    { new Guid("fa0dd5ce-289e-456b-bbb9-e0f6f6c18427"), 6, 3, new Guid("9088c37a-e025-4917-87cb-152d3f71601e") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DosesRecomendadas_VacinaId",
                table: "DosesRecomendadas",
                column: "VacinaId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosVacinas_DependenteId",
                table: "RegistrosVacinas",
                column: "DependenteId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosVacinas_DoseRecomendadaId",
                table: "RegistrosVacinas",
                column: "DoseRecomendadaId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosVacinas_VacinaId",
                table: "RegistrosVacinas",
                column: "VacinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_ApplicationUserId",
                table: "Usuarios",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosDependentes_DependenteId",
                table: "UsuariosDependentes",
                column: "DependenteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "RegistrosVacinas");

            migrationBuilder.DropTable(
                name: "UsuariosDependentes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "DosesRecomendadas");

            migrationBuilder.DropTable(
                name: "Dependentes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Vacinas");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
