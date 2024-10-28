using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "aspnetroles",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalizedname = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrencystamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnetroles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "aspnetusers",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    username = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalizedusername = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalizedemail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    emailconfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    passwordhash = table.Column<string>(type: "text", nullable: true),
                    securitystamp = table.Column<string>(type: "text", nullable: true),
                    concurrencystamp = table.Column<string>(type: "text", nullable: true),
                    phonenumber = table.Column<string>(type: "text", nullable: true),
                    phonenumberconfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    twofactorenabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockoutend = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockoutenabled = table.Column<bool>(type: "boolean", nullable: false),
                    accessfailedcount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnetusers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dependentes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    sobrenome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    datanascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    sexo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dependentes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vacinas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    descricao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vacinas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "aspnetroleclaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    roleid = table.Column<string>(type: "text", nullable: false),
                    claimtype = table.Column<string>(type: "text", nullable: true),
                    claimvalue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnetroleclaims", x => x.id);
                    table.ForeignKey(
                        name: "FK_aspnetroleclaims_aspnetroles_roleid",
                        column: x => x.roleid,
                        principalTable: "aspnetroles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnetuserclaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<string>(type: "text", nullable: false),
                    claimtype = table.Column<string>(type: "text", nullable: true),
                    claimvalue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnetuserclaims", x => x.id);
                    table.ForeignKey(
                        name: "FK_aspnetuserclaims_aspnetusers_userid",
                        column: x => x.userid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnetuserlogins",
                columns: table => new
                {
                    loginprovider = table.Column<string>(type: "text", nullable: false),
                    providerkey = table.Column<string>(type: "text", nullable: false),
                    providerdisplayname = table.Column<string>(type: "text", nullable: true),
                    userid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnetuserlogins", x => new { x.loginprovider, x.providerkey });
                    table.ForeignKey(
                        name: "FK_aspnetuserlogins_aspnetusers_userid",
                        column: x => x.userid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnetuserroles",
                columns: table => new
                {
                    userid = table.Column<string>(type: "text", nullable: false),
                    roleid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnetuserroles", x => new { x.userid, x.roleid });
                    table.ForeignKey(
                        name: "FK_aspnetuserroles_aspnetroles_roleid",
                        column: x => x.roleid,
                        principalTable: "aspnetroles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_aspnetuserroles_aspnetusers_userid",
                        column: x => x.userid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "aspnetusertokens",
                columns: table => new
                {
                    userid = table.Column<string>(type: "text", nullable: false),
                    loginprovider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aspnetusertokens", x => new { x.userid, x.loginprovider, x.name });
                    table.ForeignKey(
                        name: "FK_aspnetusertokens_aspnetusers_userid",
                        column: x => x.userid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    sobrenome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    datanascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    sexo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    applicationuserid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_usuarios_aspnetusers_applicationuserid",
                        column: x => x.applicationuserid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dosesrecomendadas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    numero = table.Column<int>(type: "integer", nullable: false),
                    idadeparaaplicacaoemmeses = table.Column<int>(type: "integer", nullable: false),
                    vacinaid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dosesrecomendadas", x => x.id);
                    table.ForeignKey(
                        name: "FK_dosesrecomendadas_vacinas_vacinaid",
                        column: x => x.vacinaid,
                        principalTable: "vacinas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuariosdependentes",
                columns: table => new
                {
                    usuarioid = table.Column<string>(type: "text", nullable: false),
                    dependenteid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuariosdependentes", x => new { x.usuarioid, x.dependenteid });
                    table.ForeignKey(
                        name: "FK_usuariosdependentes_dependentes_dependenteid",
                        column: x => x.dependenteid,
                        principalTable: "dependentes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usuariosdependentes_usuarios_usuarioid",
                        column: x => x.usuarioid,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "registrosvacinas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    dataaplicacao = table.Column<DateOnly>(type: "date", nullable: false),
                    dataproximadose = table.Column<DateOnly>(type: "date", nullable: true),
                    lote = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    laboratorio = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    unidadesaude = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    nomeaplicador = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    vacinaid = table.Column<Guid>(type: "uuid", nullable: false),
                    doserecomendadaid = table.Column<Guid>(type: "uuid", nullable: false),
                    dependenteid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registrosvacinas", x => x.id);
                    table.ForeignKey(
                        name: "FK_registrosvacinas_dependentes_dependenteid",
                        column: x => x.dependenteid,
                        principalTable: "dependentes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_registrosvacinas_dosesrecomendadas_doserecomendadaid",
                        column: x => x.doserecomendadaid,
                        principalTable: "dosesrecomendadas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_registrosvacinas_vacinas_vacinaid",
                        column: x => x.vacinaid,
                        principalTable: "vacinas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "vacinas",
                columns: new[] { "id", "descricao", "nome" },
                values: new object[,]
                {
                    { new Guid("1a00c51e-f159-4cfc-9f45-8c8a598e001c"), "Protege contra a febre amarela.", "Febre Amarela" },
                    { new Guid("260a06d5-51a3-4f27-877d-8eeb26ce2bb6"), "Protege contra meningite causada pelo meningococo C.", "Meningocócica C" },
                    { new Guid("394b910a-dcc4-4b57-b37c-4113c09583a0"), "Protege contra sarampo, caxumba, rubéola e varicela.", "Tetraviral" },
                    { new Guid("52b58243-791f-47a3-9736-e1438bdb5a6f"), "Protege contra hepatite A.", "Hepatite A" },
                    { new Guid("576a24a4-881d-451b-939a-6ce93c9d811f"), "Protege contra sarampo, caxumba e rubéola.", "Tríplice Viral (SCR)" },
                    { new Guid("6d3d4ddc-dc69-4e30-8a3a-ec110c225b6f"), "Protege contra difteria, tétano e coqueluche.", "DTP" },
                    { new Guid("a1b90445-a23b-4d00-9ff5-76d9d9c2bd99"), "Protege contra hepatite B.", "Hepatite B" },
                    { new Guid("a7fdba5c-5296-4334-ac90-a9ded4fd4403"), "Protege contra doenças causadas por pneumococo, como pneumonia, otite e meningite.", "Pneumocócica 10-valente" },
                    { new Guid("c379c194-2b25-4f95-a45b-d2d14918fcfe"), "Protege contra formas graves de tuberculose.", "BCG" },
                    { new Guid("c6644ebd-63cf-4a7b-913b-e21d8191066b"), "Protege contra difteria, tétano, coqueluche, hepatite B e Haemophilus influenzae tipo B.", "Pentavalente" },
                    { new Guid("d2795cf8-95e9-4321-ac80-94fc0b34eead"), "Protege contra gastroenterite causada por rotavírus.", "Rotavírus" },
                    { new Guid("f72c805d-9ef8-4a33-81f3-1f93a10e945c"), "Protege contra a varicela (catapora).", "Varicela" }
                });

            migrationBuilder.InsertData(
                table: "dosesrecomendadas",
                columns: new[] { "id", "idadeparaaplicacaoemmeses", "numero", "vacinaid" },
                values: new object[,]
                {
                    { new Guid("2b3895e0-ecba-4dcf-a8cb-e50ea18fbfe3"), 48, 2, new Guid("6d3d4ddc-dc69-4e30-8a3a-ec110c225b6f") },
                    { new Guid("31c04d01-a82e-4001-a4ab-571e0400c133"), 3, 1, new Guid("260a06d5-51a3-4f27-877d-8eeb26ce2bb6") },
                    { new Guid("3e92a0f4-2cf6-49ad-bf93-10e40e326173"), 2, 1, new Guid("c6644ebd-63cf-4a7b-913b-e21d8191066b") },
                    { new Guid("42b7ac3b-dd0d-466d-8af3-3c6e74bedf3d"), 2, 1, new Guid("d2795cf8-95e9-4321-ac80-94fc0b34eead") },
                    { new Guid("546854d6-bd49-4a9b-bd17-053827bf13bd"), 15, 2, new Guid("576a24a4-881d-451b-939a-6ce93c9d811f") },
                    { new Guid("5c2544bc-5f66-4717-a8ce-4d2531cf6330"), 15, 1, new Guid("f72c805d-9ef8-4a33-81f3-1f93a10e945c") },
                    { new Guid("63d225ff-d790-4930-9388-09c8e7f32723"), 9, 1, new Guid("1a00c51e-f159-4cfc-9f45-8c8a598e001c") },
                    { new Guid("65b2edf6-eb38-4fc8-8b41-82fec0eab7bf"), 0, 1, new Guid("a1b90445-a23b-4d00-9ff5-76d9d9c2bd99") },
                    { new Guid("7662852b-cddb-4421-8cb6-d6f00b79bb09"), 2, 1, new Guid("a7fdba5c-5296-4334-ac90-a9ded4fd4403") },
                    { new Guid("872f1c20-981c-4935-bf85-68ad775fe27e"), 6, 3, new Guid("c6644ebd-63cf-4a7b-913b-e21d8191066b") },
                    { new Guid("9b89942b-6e43-4ebd-9d4c-d4ea27b55ea0"), 15, 1, new Guid("6d3d4ddc-dc69-4e30-8a3a-ec110c225b6f") },
                    { new Guid("a51a4a84-294b-4bbe-8e59-5f6888a0333c"), 15, 1, new Guid("394b910a-dcc4-4b57-b37c-4113c09583a0") },
                    { new Guid("a6e64b35-9307-4f64-a2e4-a31ea3999877"), 4, 2, new Guid("d2795cf8-95e9-4321-ac80-94fc0b34eead") },
                    { new Guid("ab20f03c-41f7-44ff-b9b6-3a2b93e7e4e0"), 12, 3, new Guid("a7fdba5c-5296-4334-ac90-a9ded4fd4403") },
                    { new Guid("ac63b526-3f58-4232-ac85-27d317d34483"), 6, 3, new Guid("a1b90445-a23b-4d00-9ff5-76d9d9c2bd99") },
                    { new Guid("c8a8fc7b-17fa-4821-a576-41a4c932f4ea"), 4, 2, new Guid("a7fdba5c-5296-4334-ac90-a9ded4fd4403") },
                    { new Guid("e70e0ce3-0319-4771-a805-dfcfcc7f845d"), 1, 2, new Guid("a1b90445-a23b-4d00-9ff5-76d9d9c2bd99") },
                    { new Guid("e8c48f65-5d84-43b8-ae03-2d2c8fbac8e5"), 144, 3, new Guid("6d3d4ddc-dc69-4e30-8a3a-ec110c225b6f") },
                    { new Guid("ec1ae12d-e013-438c-a248-66eb0a13c815"), 5, 2, new Guid("260a06d5-51a3-4f27-877d-8eeb26ce2bb6") },
                    { new Guid("edd567d9-a8de-473f-b0aa-6542ccc1ea8e"), 12, 1, new Guid("52b58243-791f-47a3-9736-e1438bdb5a6f") },
                    { new Guid("f2910b1c-e5fa-4c43-b7b2-88620c2c78e5"), 4, 2, new Guid("c6644ebd-63cf-4a7b-913b-e21d8191066b") },
                    { new Guid("f6541a2a-a526-436f-a1fd-af2f3142fbd6"), 0, 1, new Guid("c379c194-2b25-4f95-a45b-d2d14918fcfe") },
                    { new Guid("f8b554a5-439a-4179-a72d-9785a7d2929f"), 12, 1, new Guid("576a24a4-881d-451b-939a-6ce93c9d811f") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_aspnetroleclaims_roleid",
                table: "aspnetroleclaims",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "aspnetroles",
                column: "normalizedname",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_aspnetuserclaims_userid",
                table: "aspnetuserclaims",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_aspnetuserlogins_userid",
                table: "aspnetuserlogins",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_aspnetuserroles_roleid",
                table: "aspnetuserroles",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "aspnetusers",
                column: "normalizedemail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "aspnetusers",
                column: "normalizedusername",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dosesrecomendadas_vacinaid",
                table: "dosesrecomendadas",
                column: "vacinaid");

            migrationBuilder.CreateIndex(
                name: "IX_registrosvacinas_dependenteid",
                table: "registrosvacinas",
                column: "dependenteid");

            migrationBuilder.CreateIndex(
                name: "IX_registrosvacinas_doserecomendadaid",
                table: "registrosvacinas",
                column: "doserecomendadaid");

            migrationBuilder.CreateIndex(
                name: "IX_registrosvacinas_vacinaid",
                table: "registrosvacinas",
                column: "vacinaid");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_applicationuserid",
                table: "usuarios",
                column: "applicationuserid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuariosdependentes_dependenteid",
                table: "usuariosdependentes",
                column: "dependenteid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aspnetroleclaims");

            migrationBuilder.DropTable(
                name: "aspnetuserclaims");

            migrationBuilder.DropTable(
                name: "aspnetuserlogins");

            migrationBuilder.DropTable(
                name: "aspnetuserroles");

            migrationBuilder.DropTable(
                name: "aspnetusertokens");

            migrationBuilder.DropTable(
                name: "registrosvacinas");

            migrationBuilder.DropTable(
                name: "usuariosdependentes");

            migrationBuilder.DropTable(
                name: "aspnetroles");

            migrationBuilder.DropTable(
                name: "dosesrecomendadas");

            migrationBuilder.DropTable(
                name: "dependentes");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "vacinas");

            migrationBuilder.DropTable(
                name: "aspnetusers");
        }
    }
}
