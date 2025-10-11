using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace P1_AP1_JuanGuillen.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntradaHuacal",
                columns: table => new
                {
                    IdEntrada = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NombreCliente = table.Column<string>(type: "TEXT", nullable: false),
                    cantidad = table.Column<int>(type: "INTEGER", nullable: false),
                    precio = table.Column<double>(type: "REAL", nullable: false),
                    Importe = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_controlHuacales", x => x.IdEntrada);
                });

            migrationBuilder.CreateTable(
                name: "TipoHuacales",
                columns: table => new
                {
                    TipoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Existencia = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoHuacales", x => x.TipoId);
                });

            migrationBuilder.CreateTable(
                name: "HuacalDetalles",
                columns: table => new
                {
                    DetallesId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdEntrada = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: false),
                    Precio = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalles", x => x.DetallesId);
                    table.ForeignKey(
                        name: "FK_Detalles_TipoHuacales_TipoId",
                        column: x => x.TipoId,
                        principalTable: "TipoHuacales",
                        principalColumn: "TipoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Detalles_controlHuacales_IdEntrada",
                        column: x => x.IdEntrada,
                        principalTable: "EntradaHuacal",
                        principalColumn: "IdEntrada",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TipoHuacales",
                columns: new[] { "TipoId", "Descripcion", "Existencia" },
                values: new object[,]
                {
                    { 1, "Huacales verdes", 0 },
                    { 2, "Huacales azules", 0 },
                    { 3, "Huacales de huevos", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_IdEntrada",
                table: "HuacalDetalles",
                column: "IdEntrada");

            migrationBuilder.CreateIndex(
                name: "IX_Detalles_TipoId",
                table: "HuacalDetalles",
                column: "TipoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HuacalDetalles");

            migrationBuilder.DropTable(
                name: "TipoHuacales");

            migrationBuilder.DropTable(
                name: "EntradaHuacal");
        }
    }
}
