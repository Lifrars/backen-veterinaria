using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApiVeterinaria.Migrations
{
    public partial class migration7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Cedula = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombres = table.Column<string>(type: "text", nullable: false),
                    Apellidos = table.Column<string>(type: "text", nullable: false),
                    Direccion = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Cedula);
                });

            migrationBuilder.CreateTable(
                name: "Mascotas",
                columns: table => new
                {
                    Identificacion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Raza = table.Column<string>(type: "text", nullable: false),
                    Edad = table.Column<int>(type: "integer", nullable: false),
                    Peso = table.Column<double>(type: "double precision", nullable: false),
                    ClienteCedula = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mascotas", x => x.Identificacion);
                    table.ForeignKey(
                        name: "FK_Mascotas_Clientes_ClienteCedula",
                        column: x => x.ClienteCedula,
                        principalTable: "Clientes",
                        principalColumn: "Cedula",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medicamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Dosis = table.Column<string>(type: "text", nullable: false),
                    MascotaIdentificacion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicamentos_Mascotas_MascotaIdentificacion",
                        column: x => x.MascotaIdentificacion,
                        principalTable: "Mascotas",
                        principalColumn: "Identificacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Cedula", "Apellidos", "Direccion", "Nombres", "Telefono" },
                values: new object[,]
                {
                    { 1, "Pérez", "Calle 1", "Juan", "123456789" },
                    { 2, "García", "Calle 2", "Ana", "987654321" }
                });

            migrationBuilder.InsertData(
                table: "Mascotas",
                columns: new[] { "Identificacion", "ClienteCedula", "Edad", "Nombre", "Peso", "Raza" },
                values: new object[,]
                {
                    { 1, 1, 5, "Rex", 30.5, "Labrador" },
                    { 2, 2, 3, "Bella", 25.0, "Bulldog" }
                });

            migrationBuilder.InsertData(
                table: "Medicamentos",
                columns: new[] { "Id", "Descripcion", "Dosis", "MascotaIdentificacion", "Nombre" },
                values: new object[,]
                {
                    { 1, "Antibiotico para infecciones", "1 pastilla cada 8 horas", 1, "Antibiotico A" },
                    { 2, "Para inflamaciones", "1 pastilla cada 12 horas", 1, "Antiinflamatorio B" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_ClienteCedula",
                table: "Mascotas",
                column: "ClienteCedula");

            migrationBuilder.CreateIndex(
                name: "IX_Medicamentos_MascotaIdentificacion",
                table: "Medicamentos",
                column: "MascotaIdentificacion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medicamentos");

            migrationBuilder.DropTable(
                name: "Mascotas");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
