using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEmpleado.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableDepartamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdDepartamento",
                table: "Empleado",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Departamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_IdDepartamento",
                table: "Empleado",
                column: "IdDepartamento");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleado_Departamento_IdDepartamento",
                table: "Empleado",
                column: "IdDepartamento",
                principalTable: "Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleado_Departamento_IdDepartamento",
                table: "Empleado");

            migrationBuilder.DropTable(
                name: "Departamento");

            migrationBuilder.DropIndex(
                name: "IX_Empleado_IdDepartamento",
                table: "Empleado");

            migrationBuilder.DropColumn(
                name: "IdDepartamento",
                table: "Empleado");
        }
    }
}
