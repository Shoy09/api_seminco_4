using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Seminco.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UseChecklistItemsForOperacionTalHorizontal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_operacion_tal_horizontal_checklist_checklist_item_catalog_c~",
                table: "operacion_tal_horizontal_checklist");

            migrationBuilder.DropTable(
                name: "checklist_item_catalog");

            migrationBuilder.AddForeignKey(
                name: "FK_operacion_tal_horizontal_checklist_checklist_items_checklis~",
                table: "operacion_tal_horizontal_checklist",
                column: "checklist_item_id",
                principalTable: "checklist_items",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_operacion_tal_horizontal_checklist_checklist_items_checklis~",
                table: "operacion_tal_horizontal_checklist");

            migrationBuilder.CreateTable(
                name: "checklist_item_catalog",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    activo = table.Column<bool>(type: "boolean", nullable: false),
                    categoria = table.Column<string>(type: "text", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    orden = table.Column<int>(type: "integer", nullable: false),
                    proceso = table.Column<string>(type: "text", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checklist_item_catalog", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_operacion_tal_horizontal_checklist_checklist_item_catalog_c~",
                table: "operacion_tal_horizontal_checklist",
                column: "checklist_item_id",
                principalTable: "checklist_item_catalog",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
