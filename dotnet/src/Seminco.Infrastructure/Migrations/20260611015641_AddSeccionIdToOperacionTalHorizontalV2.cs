using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seminco.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeccionIdToOperacionTalHorizontalV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "seccion_id",
                table: "operacion_tal_horizontal_v2",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_operacion_tal_horizontal_v2_seccion_id",
                table: "operacion_tal_horizontal_v2",
                column: "seccion_id");

            migrationBuilder.AddForeignKey(
                name: "FK_operacion_tal_horizontal_v2_secciones_seccion_id",
                table: "operacion_tal_horizontal_v2",
                column: "seccion_id",
                principalTable: "secciones",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_operacion_tal_horizontal_v2_secciones_seccion_id",
                table: "operacion_tal_horizontal_v2");

            migrationBuilder.DropIndex(
                name: "IX_operacion_tal_horizontal_v2_seccion_id",
                table: "operacion_tal_horizontal_v2");

            migrationBuilder.DropColumn(
                name: "seccion_id",
                table: "operacion_tal_horizontal_v2");
        }
    }
}
