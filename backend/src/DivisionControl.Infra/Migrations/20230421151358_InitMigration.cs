using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DivisionControl.Infra.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dividas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroDoTitulo = table.Column<string>(type: "varchar(100)", nullable: false),
                    NomeDoDevedor = table.Column<string>(type: "varchar(100)", nullable: false),
                    CpfDoDevedor = table.Column<string>(type: "varchar(14)", nullable: false),
                    Juros = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Multa = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dividas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parcelas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DividaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroDaParcela = table.Column<string>(type: "varchar(100)", nullable: false),
                    DataDeVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorDaParcela = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcelas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parcelas_Dividas_DividaId",
                        column: x => x.DividaId,
                        principalTable: "Dividas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parcelas_DividaId",
                table: "Parcelas",
                column: "DividaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parcelas");

            migrationBuilder.DropTable(
                name: "Dividas");
        }
    }
}
