using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MockAbiANS.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Situacaos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Situacaos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposRegistros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposRegistros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Protocolos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    NumeroProcesso = table.Column<string>(type: "text", nullable: false),
                    CodOperadora = table.Column<string>(type: "text", nullable: false),
                    TipoRegistroId = table.Column<int>(type: "integer", nullable: false),
                    SituacaoId = table.Column<int>(type: "integer", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Protocolos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Protocolos_Situacaos_SituacaoId",
                        column: x => x.SituacaoId,
                        principalTable: "Situacaos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Protocolos_TiposRegistros_TipoRegistroId",
                        column: x => x.TipoRegistroId,
                        principalTable: "TiposRegistros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Numero = table.Column<string>(type: "text", nullable: false),
                    ProtocoloId = table.Column<int>(type: "integer", nullable: false),
                    Assunto = table.Column<string>(type: "text", nullable: false),
                    DataDocumento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documentos_Protocolos_ProtocoloId",
                        column: x => x.ProtocoloId,
                        principalTable: "Protocolos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InformacoesAdicionais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    NumeroAtendimento = table.Column<long>(type: "bigint", nullable: false),
                    CompetenciaAtendimento = table.Column<string>(type: "text", nullable: false),
                    DataFimAtendimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProtocoloId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformacoesAdicionais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InformacoesAdicionais_Protocolos_Id",
                        column: x => x.Id,
                        principalTable: "Protocolos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InformacoesAdicionais_Protocolos_ProtocoloId",
                        column: x => x.ProtocoloId,
                        principalTable: "Protocolos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Arquivos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Hash = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Tamanho = table.Column<long>(type: "bigint", nullable: false),
                    DocumentoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arquivos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arquivos_Documentos_DocumentoId",
                        column: x => x.DocumentoId,
                        principalTable: "Documentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TiposDocumento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DocumentoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDocumento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TiposDocumento_Documentos_DocumentoId",
                        column: x => x.DocumentoId,
                        principalTable: "Documentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Situacaos",
                columns: new[] { "Id", "Descricao" },
                values: new object[] { 1, "Andamento" });

            migrationBuilder.InsertData(
                table: "TiposRegistros",
                columns: new[] { "Id", "Descricao" },
                values: new object[] { 2, "Petição (Encaminhado pela Operadora para a ANS)" });

            migrationBuilder.CreateIndex(
                name: "IX_Arquivos_DocumentoId",
                table: "Arquivos",
                column: "DocumentoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_ProtocoloId",
                table: "Documentos",
                column: "ProtocoloId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacoesAdicionais_ProtocoloId",
                table: "InformacoesAdicionais",
                column: "ProtocoloId");

            migrationBuilder.CreateIndex(
                name: "IX_Protocolos_SituacaoId",
                table: "Protocolos",
                column: "SituacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Protocolos_TipoRegistroId",
                table: "Protocolos",
                column: "TipoRegistroId");

            migrationBuilder.CreateIndex(
                name: "IX_TiposDocumento_DocumentoId",
                table: "TiposDocumento",
                column: "DocumentoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arquivos");

            migrationBuilder.DropTable(
                name: "InformacoesAdicionais");

            migrationBuilder.DropTable(
                name: "TiposDocumento");

            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "Protocolos");

            migrationBuilder.DropTable(
                name: "Situacaos");

            migrationBuilder.DropTable(
                name: "TiposRegistros");
        }
    }
}
