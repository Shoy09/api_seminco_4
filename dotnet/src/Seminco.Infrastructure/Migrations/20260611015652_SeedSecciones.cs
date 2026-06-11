using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seminco.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedSecciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                insert into secciones (id, proceso, nombre)
                values  (7, 'PERFORACIÓN TALADROS LARGOS', 'VII'),
                        (8, 'PERFORACIÓN HORIZONTAL', 'VII'),
                        (9, 'EMPERNADOR', 'VII'),
                        (10, 'SCISSOR', 'VII'),
                        (11, 'SCALAMIN', 'VII'),
                        (12, 'ROMPEBANCOS', 'VII'),
                        (13, 'ANFOCHANGER', 'VII'),
                        (14, 'SCOOPTRAM', 'VII'),
                        (15, 'DUMPER', 'VII'),
                        (16, 'PERFORACIÓN HORIZONTAL', 'I'),
                        (17, 'PERFORACIÓN HORIZONTAL', 'II'),
                        (18, 'PERFORACIÓN HORIZONTAL', 'III'),
                        (19, 'PERFORACIÓN HORIZONTAL', 'IV'),
                        (20, 'PERFORACIÓN HORIZONTAL', 'V'),
                        (21, 'PERFORACIÓN HORIZONTAL', 'VI'),
                        (22, 'PERFORACIÓN TALADROS LARGOS', 'I'),
                        (23, 'PERFORACIÓN TALADROS LARGOS', 'II'),
                        (24, 'PERFORACIÓN TALADROS LARGOS', 'III'),
                        (25, 'PERFORACIÓN TALADROS LARGOS', 'IV'),
                        (26, 'PERFORACIÓN TALADROS LARGOS', 'V'),
                        (27, 'PERFORACIÓN TALADROS LARGOS', 'VI'),
                        (28, 'EMPERNADOR', 'I'),
                        (29, 'EMPERNADOR', 'II'),
                        (30, 'EMPERNADOR', 'III'),
                        (31, 'EMPERNADOR', 'IV'),
                        (32, 'EMPERNADOR', 'V'),
                        (33, 'EMPERNADOR', 'VI'),
                        (34, 'SCISSOR', 'I'),
                        (35, 'SCISSOR', 'II'),
                        (36, 'SCISSOR', 'III'),
                        (37, 'SCISSOR', 'IV'),
                        (38, 'SCISSOR', 'V'),
                        (39, 'SCISSOR', 'VI'),
                        (40, 'SCALAMIN', 'I'),
                        (41, 'SCALAMIN', 'II'),
                        (42, 'SCALAMIN', 'III'),
                        (43, 'SCALAMIN', 'IV'),
                        (44, 'SCALAMIN', 'V'),
                        (45, 'SCALAMIN', 'VI'),
                        (46, 'ROMPEBANCOS', 'I'),
                        (47, 'ROMPEBANCOS', 'II'),
                        (48, 'ROMPEBANCOS', 'III'),
                        (49, 'ROMPEBANCOS', 'IV'),
                        (50, 'ROMPEBANCOS', 'V'),
                        (51, 'ROMPEBANCOS', 'VI'),
                        (52, 'ANFOCHANGER', 'I'),
                        (53, 'ANFOCHANGER', 'II'),
                        (54, 'ANFOCHANGER', 'III'),
                        (55, 'ANFOCHANGER', 'IV'),
                        (56, 'ANFOCHANGER', 'V'),
                        (57, 'ANFOCHANGER', 'VI'),
                        (58, 'SCOOPTRAM', 'I'),
                        (59, 'SCOOPTRAM', 'II'),
                        (60, 'SCOOPTRAM', 'III'),
                        (61, 'SCOOPTRAM', 'IV'),
                        (62, 'SCOOPTRAM', 'V'),
                        (63, 'SCOOPTRAM', 'VI'),
                        (64, 'DUMPER', 'I'),
                        (65, 'DUMPER', 'II'),
                        (66, 'DUMPER', 'III'),
                        (67, 'DUMPER', 'IV'),
                        (68, 'DUMPER', 'V'),
                        (69, 'DUMPER', 'VI')
                on conflict (id) do update set
                    proceso = excluded.proceso,
                    nombre = excluded.nombre;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM secciones WHERE id BETWEEN 7 AND 69");
        }
    }
}
