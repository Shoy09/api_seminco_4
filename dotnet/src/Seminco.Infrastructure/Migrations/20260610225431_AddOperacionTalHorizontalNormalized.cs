using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Seminco.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOperacionTalHorizontalNormalized : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "checklist_item_catalog",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    proceso = table.Column<string>(type: "text", nullable: false),
                    categoria = table.Column<string>(type: "text", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    orden = table.Column<int>(type: "integer", nullable: false),
                    activo = table.Column<bool>(type: "boolean", nullable: false),
                    version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checklist_item_catalog", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "operacion_tal_horizontal_v2",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    turno = table.Column<string>(type: "text", nullable: true),
                    operador = table.Column<string>(type: "text", nullable: true),
                    jefe_guardia = table.Column<string>(type: "text", nullable: true),
                    equipo_id = table.Column<int>(type: "integer", nullable: true),
                    equipo_nombre = table.Column<string>(type: "text", nullable: true),
                    n_equipo = table.Column<string>(type: "text", nullable: true),
                    seccion = table.Column<string>(type: "text", nullable: true),
                    modelo_equipo = table.Column<string>(type: "text", nullable: true),
                    estado = table.Column<string>(type: "text", nullable: false),
                    envio = table.Column<int>(type: "integer", nullable: false),
                    revisado = table.Column<int>(type: "integer", nullable: false),
                    aprobacion = table.Column<int>(type: "integer", nullable: false),
                    observaciones_jefe = table.Column<string>(type: "text", nullable: true),
                    observaciones_jefe2 = table.Column<string>(type: "text", nullable: true),
                    observaciones_jefe3 = table.Column<string>(type: "text", nullable: true),
                    payload_original = table.Column<string>(type: "jsonb", nullable: true),
                    payload_version = table.Column<string>(type: "text", nullable: true),
                    external_sync_id = table.Column<string>(type: "text", nullable: true),
                    device_id = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operacion_tal_horizontal_v2", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "operacion_tal_horizontal_checklist",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    operacion_id = table.Column<int>(type: "integer", nullable: false),
                    checklist_item_id = table.Column<int>(type: "integer", nullable: true),
                    categoria_snapshot = table.Column<string>(type: "text", nullable: false),
                    descripcion_snapshot = table.Column<string>(type: "text", nullable: false),
                    decision = table.Column<int>(type: "integer", nullable: false),
                    observacion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operacion_tal_horizontal_checklist", x => x.id);
                    table.ForeignKey(
                        name: "FK_operacion_tal_horizontal_checklist_checklist_item_catalog_c~",
                        column: x => x.checklist_item_id,
                        principalTable: "checklist_item_catalog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_operacion_tal_horizontal_checklist_operacion_tal_horizontal~",
                        column: x => x.operacion_id,
                        principalTable: "operacion_tal_horizontal_v2",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "operacion_tal_horizontal_condicion_equipo",
                columns: table => new
                {
                    operacion_id = table.Column<int>(type: "integer", nullable: false),
                    op = table.Column<bool>(type: "boolean", nullable: false),
                    no_op = table.Column<bool>(type: "boolean", nullable: false),
                    lugar = table.Column<string>(type: "text", nullable: true),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    aceite_motor = table.Column<bool>(type: "boolean", nullable: false),
                    aceite_hidraulico = table.Column<bool>(type: "boolean", nullable: false),
                    aceite_transmision = table.Column<bool>(type: "boolean", nullable: false),
                    combustible = table.Column<string>(type: "text", nullable: true),
                    hora_llenado = table.Column<TimeOnly>(type: "time without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operacion_tal_horizontal_condicion_equipo", x => x.operacion_id);
                    table.ForeignKey(
                        name: "FK_operacion_tal_horizontal_condicion_equipo_operacion_tal_hor~",
                        column: x => x.operacion_id,
                        principalTable: "operacion_tal_horizontal_v2",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "operacion_tal_horizontal_control_llanta",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    operacion_id = table.Column<int>(type: "integer", nullable: false),
                    posicion = table.Column<short>(type: "smallint", nullable: false),
                    estado = table.Column<bool>(type: "boolean", nullable: false),
                    presion = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    observacion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operacion_tal_horizontal_control_llanta", x => x.id);
                    table.ForeignKey(
                        name: "FK_operacion_tal_horizontal_control_llanta_operacion_tal_horiz~",
                        column: x => x.operacion_id,
                        principalTable: "operacion_tal_horizontal_v2",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "operacion_tal_horizontal_horometro",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    operacion_id = table.Column<int>(type: "integer", nullable: false),
                    tipo = table.Column<string>(type: "text", nullable: false),
                    inicio = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    final = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    op = table.Column<bool>(type: "boolean", nullable: false),
                    inop = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operacion_tal_horizontal_horometro", x => x.id);
                    table.ForeignKey(
                        name: "FK_operacion_tal_horizontal_horometro_operacion_tal_horizontal~",
                        column: x => x.operacion_id,
                        principalTable: "operacion_tal_horizontal_v2",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "operacion_tal_horizontal_registro",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    operacion_id = table.Column<int>(type: "integer", nullable: false),
                    external_id = table.Column<long>(type: "bigint", nullable: true),
                    numero = table.Column<int>(type: "integer", nullable: false),
                    estado_principal = table.Column<string>(type: "text", nullable: false),
                    codigo_estado = table.Column<string>(type: "text", nullable: false),
                    estado_catalogo_id = table.Column<int>(type: "integer", nullable: true),
                    hora_inicio = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    hora_final = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    payload_operacion = table.Column<string>(type: "jsonb", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operacion_tal_horizontal_registro", x => x.id);
                    table.ForeignKey(
                        name: "FK_operacion_tal_horizontal_registro_operacion_tal_horizontal_~",
                        column: x => x.operacion_id,
                        principalTable: "operacion_tal_horizontal_v2",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "operacion_tal_horizontal_registro_detalle",
                columns: table => new
                {
                    registro_id = table.Column<int>(type: "integer", nullable: false),
                    nivel = table.Column<string>(type: "text", nullable: true),
                    tipo_labor = table.Column<string>(type: "text", nullable: true),
                    labor = table.Column<string>(type: "text", nullable: true),
                    ala = table.Column<string>(type: "text", nullable: true),
                    tal_prod = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    tal_rimados = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    tal_alivio = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    tal_repaso = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    long_barras = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    num_barras = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    tipo_perforacion = table.Column<string>(type: "text", nullable: true),
                    tipo_perforacion_id = table.Column<int>(type: "integer", nullable: true),
                    observaciones = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operacion_tal_horizontal_registro_detalle", x => x.registro_id);
                    table.ForeignKey(
                        name: "FK_operacion_tal_horizontal_registro_detalle_operacion_tal_hor~",
                        column: x => x.registro_id,
                        principalTable: "operacion_tal_horizontal_registro",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_operacion_tal_horizontal_checklist_checklist_item_id",
                table: "operacion_tal_horizontal_checklist",
                column: "checklist_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_operacion_tal_horizontal_checklist_operacion_id",
                table: "operacion_tal_horizontal_checklist",
                column: "operacion_id");

            migrationBuilder.CreateIndex(
                name: "IX_operacion_tal_horizontal_control_llanta_operacion_id",
                table: "operacion_tal_horizontal_control_llanta",
                column: "operacion_id");

            migrationBuilder.CreateIndex(
                name: "IX_operacion_tal_horizontal_horometro_operacion_id_tipo",
                table: "operacion_tal_horizontal_horometro",
                columns: new[] { "operacion_id", "tipo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_operacion_tal_horizontal_registro_codigo_estado",
                table: "operacion_tal_horizontal_registro",
                column: "codigo_estado");

            migrationBuilder.CreateIndex(
                name: "IX_operacion_tal_horizontal_registro_estado_principal",
                table: "operacion_tal_horizontal_registro",
                column: "estado_principal");

            migrationBuilder.CreateIndex(
                name: "IX_operacion_tal_horizontal_registro_operacion_id",
                table: "operacion_tal_horizontal_registro",
                column: "operacion_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "operacion_tal_horizontal_checklist");

            migrationBuilder.DropTable(
                name: "operacion_tal_horizontal_condicion_equipo");

            migrationBuilder.DropTable(
                name: "operacion_tal_horizontal_control_llanta");

            migrationBuilder.DropTable(
                name: "operacion_tal_horizontal_horometro");

            migrationBuilder.DropTable(
                name: "operacion_tal_horizontal_registro_detalle");

            migrationBuilder.DropTable(
                name: "checklist_item_catalog");

            migrationBuilder.DropTable(
                name: "operacion_tal_horizontal_registro");

            migrationBuilder.DropTable(
                name: "operacion_tal_horizontal_v2");
        }
    }
}
