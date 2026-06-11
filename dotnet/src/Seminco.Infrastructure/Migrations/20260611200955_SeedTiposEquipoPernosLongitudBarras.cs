using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seminco.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedTiposEquipoPernosLongitudBarras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                INSERT INTO tipo_equipos (id, nombre)
                VALUES
                  (3, 'Diesel'),
                  (4, 'Electrico')
                ON CONFLICT (id) DO UPDATE
                SET nombre = EXCLUDED.nombre;
                """);

            migrationBuilder.Sql(
                """
                INSERT INTO pernos (id, tipo_perno, longitud)
                VALUES
                  (4, 'SPLIT SET', 7),
                  (5, 'SPLIT SET', 5),
                  (6, 'SPLIT SET', 3),
                  (7, 'SWELLEX', 7),
                  (8, 'PERNO HELICOIDAL', 7),
                  (9, 'SERVICIOS', 2)
                ON CONFLICT (id) DO UPDATE
                SET
                  tipo_perno = EXCLUDED.tipo_perno,
                  longitud = EXCLUDED.longitud;
                """);

            migrationBuilder.Sql(
                """
                INSERT INTO longitud_barras (id, proceso, longitud_pies)
                VALUES
                  (1, 'PERFORACIÓN TALADROS LARGOS', 4),
                  (2, 'PERFORACIÓN HORIZONTAL', 13),
                  (3, 'PERFORACIÓN TALADROS LARGOS', 3),
                  (4, 'PERFORACIÓN TALADROS LARGOS', 5),
                  (5, 'PERFORACIÓN HORIZONTAL', 12),
                  (6, 'PERFORACIÓN HORIZONTAL', 11),
                  (7, 'PERFORACIÓN HORIZONTAL', 10),
                  (8, 'PERFORACIÓN HORIZONTAL', 9),
                  (9, 'PERFORACIÓN HORIZONTAL', 8),
                  (11, 'PERFORACIÓN HORIZONTAL', 7),
                  (12, 'PERFORACIÓN HORIZONTAL', 2),
                  (13, 'PERFORACIÓN HORIZONTAL', 3),
                  (14, 'PERFORACIÓN HORIZONTAL', 6)
                ON CONFLICT (id) DO UPDATE
                SET
                  proceso = EXCLUDED.proceso,
                  longitud_pies = EXCLUDED.longitud_pies;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM tipo_equipos WHERE id IN (3, 4);");
            migrationBuilder.Sql("DELETE FROM pernos WHERE id IN (4, 5, 6, 7, 8, 9);");
            migrationBuilder.Sql("DELETE FROM longitud_barras WHERE id IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 11, 12, 13, 14);");
        }
    }
}
