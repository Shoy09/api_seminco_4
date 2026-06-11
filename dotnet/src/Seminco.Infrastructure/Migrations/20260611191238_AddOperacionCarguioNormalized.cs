using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Seminco.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOperacionCarguioNormalized : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "operacion_carguio_v2",
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
                    seccion_id = table.Column<int>(type: "integer", nullable: true),
                    capacidad = table.Column<string>(type: "text", nullable: true),
                    tipo_equipo_diesel = table.Column<bool>(type: "boolean", nullable: true),
                    tipo_equipo_electrico = table.Column<bool>(type: "boolean", nullable: true),
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
                    table.PrimaryKey("PK_operacion_carguio_v2", x => x.id);
                    table.ForeignKey(
                        name: "FK_operacion_carguio_v2_secciones_seccion_id",
                        column: x => x.seccion_id,
                        principalTable: "secciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "operacion_carguio_checklist",
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
                    table.PrimaryKey("PK_operacion_carguio_checklist", x => x.id);
                    table.ForeignKey(
                        name: "FK_operacion_carguio_checklist_checklist_items_checklist_item_~",
                        column: x => x.checklist_item_id,
                        principalTable: "checklist_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_operacion_carguio_checklist_operacion_carguio_v2_operacion_~",
                        column: x => x.operacion_id,
                        principalTable: "operacion_carguio_v2",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "operacion_carguio_condicion_equipo",
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
                    table.PrimaryKey("PK_operacion_carguio_condicion_equipo", x => x.operacion_id);
                    table.ForeignKey(
                        name: "FK_operacion_carguio_condicion_equipo_operacion_carguio_v2_ope~",
                        column: x => x.operacion_id,
                        principalTable: "operacion_carguio_v2",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "operacion_carguio_control_llanta",
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
                    table.PrimaryKey("PK_operacion_carguio_control_llanta", x => x.id);
                    table.ForeignKey(
                        name: "FK_operacion_carguio_control_llanta_operacion_carguio_v2_opera~",
                        column: x => x.operacion_id,
                        principalTable: "operacion_carguio_v2",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "operacion_carguio_horometro",
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
                    table.PrimaryKey("PK_operacion_carguio_horometro", x => x.id);
                    table.ForeignKey(
                        name: "FK_operacion_carguio_horometro_operacion_carguio_v2_operacion_~",
                        column: x => x.operacion_id,
                        principalTable: "operacion_carguio_v2",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "operacion_carguio_programa_trabajo",
                columns: table => new
                {
                    operacion_id = table.Column<int>(type: "integer", nullable: false),
                    n_cucharas_programado = table.Column<int>(type: "integer", nullable: true),
                    n_cucharas_realizado = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operacion_carguio_programa_trabajo", x => x.operacion_id);
                    table.ForeignKey(
                        name: "FK_operacion_carguio_programa_trabajo_operacion_carguio_v2_ope~",
                        column: x => x.operacion_id,
                        principalTable: "operacion_carguio_v2",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "operacion_carguio_registro",
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
                    table.PrimaryKey("PK_operacion_carguio_registro", x => x.id);
                    table.ForeignKey(
                        name: "FK_operacion_carguio_registro_operacion_carguio_v2_operacion_id",
                        column: x => x.operacion_id,
                        principalTable: "operacion_carguio_v2",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "operacion_carguio_registro_detalle",
                columns: table => new
                {
                    registro_id = table.Column<int>(type: "integer", nullable: false),
                    nivel_inicio = table.Column<string>(type: "text", nullable: true),
                    tipo_labor_inicio = table.Column<string>(type: "text", nullable: true),
                    labor_inicio = table.Column<string>(type: "text", nullable: true),
                    ala_inicio = table.Column<string>(type: "text", nullable: true),
                    ubicacion_destino = table.Column<string>(type: "text", nullable: true),
                    n_cucharas = table.Column<int>(type: "integer", nullable: true),
                    observaciones = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operacion_carguio_registro_detalle", x => x.registro_id);
                    table.ForeignKey(
                        name: "FK_operacion_carguio_registro_detalle_operacion_carguio_regist~",
                        column: x => x.registro_id,
                        principalTable: "operacion_carguio_registro",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_operacion_carguio_checklist_checklist_item_id",
                table: "operacion_carguio_checklist",
                column: "checklist_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_operacion_carguio_checklist_operacion_id",
                table: "operacion_carguio_checklist",
                column: "operacion_id");

            migrationBuilder.CreateIndex(
                name: "IX_operacion_carguio_control_llanta_operacion_id",
                table: "operacion_carguio_control_llanta",
                column: "operacion_id");

            migrationBuilder.CreateIndex(
                name: "IX_operacion_carguio_horometro_operacion_id_tipo",
                table: "operacion_carguio_horometro",
                columns: new[] { "operacion_id", "tipo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_operacion_carguio_registro_codigo_estado",
                table: "operacion_carguio_registro",
                column: "codigo_estado");

            migrationBuilder.CreateIndex(
                name: "IX_operacion_carguio_registro_estado_principal",
                table: "operacion_carguio_registro",
                column: "estado_principal");

            migrationBuilder.CreateIndex(
                name: "IX_operacion_carguio_registro_operacion_id",
                table: "operacion_carguio_registro",
                column: "operacion_id");

            migrationBuilder.CreateIndex(
                name: "IX_operacion_carguio_v2_seccion_id",
                table: "operacion_carguio_v2",
                column: "seccion_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "operacion_carguio_checklist");

            migrationBuilder.DropTable(
                name: "operacion_carguio_condicion_equipo");

            migrationBuilder.DropTable(
                name: "operacion_carguio_control_llanta");

            migrationBuilder.DropTable(
                name: "operacion_carguio_horometro");

            migrationBuilder.DropTable(
                name: "operacion_carguio_programa_trabajo");

            migrationBuilder.DropTable(
                name: "operacion_carguio_registro_detalle");

            migrationBuilder.DropTable(
                name: "operacion_carguio_registro");

            migrationBuilder.DropTable(
                name: "operacion_carguio_v2");
        }
    }
}
