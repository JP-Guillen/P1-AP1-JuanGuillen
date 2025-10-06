using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P1_AP1_JuanGuillen.Migrations
{
    /// <inheritdoc />
    public partial class InicioActualizado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Registros",
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
                    table.PrimaryKey("PK_Registros", x => x.IdEntrada);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registros");
        }
    }
}
