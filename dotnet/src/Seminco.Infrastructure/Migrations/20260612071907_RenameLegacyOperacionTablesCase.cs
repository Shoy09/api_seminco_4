using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Seminco.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameLegacyOperacionTablesCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                DO $$
                BEGIN
                    IF to_regclass('"Operacion_tal_largo"') IS NULL AND to_regclass('operacion_tal_largo') IS NOT NULL THEN
                        EXECUTE 'ALTER TABLE operacion_tal_largo RENAME TO "Operacion_tal_largo"';
                    END IF;
                    IF to_regclass('"Operacion_tal_horizontal"') IS NULL AND to_regclass('operacion_tal_horizontal') IS NOT NULL THEN
                        EXECUTE 'ALTER TABLE operacion_tal_horizontal RENAME TO "Operacion_tal_horizontal"';
                    END IF;
                    IF to_regclass('"Operacion_empernador"') IS NULL AND to_regclass('operacion_empernador') IS NOT NULL THEN
                        EXECUTE 'ALTER TABLE operacion_empernador RENAME TO "Operacion_empernador"';
                    END IF;
                    IF to_regclass('"Operacion_carguio"') IS NULL AND to_regclass('operacion_carguio') IS NOT NULL THEN
                        EXECUTE 'ALTER TABLE operacion_carguio RENAME TO "Operacion_carguio"';
                    END IF;
                    IF to_regclass('"Operacion_rompebanco"') IS NULL AND to_regclass('operacion_rompebanco') IS NOT NULL THEN
                        EXECUTE 'ALTER TABLE operacion_rompebanco RENAME TO "Operacion_rompebanco"';
                    END IF;
                    IF to_regclass('"Operacion_scissor"') IS NULL AND to_regclass('operacion_scissor') IS NOT NULL THEN
                        EXECUTE 'ALTER TABLE operacion_scissor RENAME TO "Operacion_scissor"';
                    END IF;
                    IF to_regclass('"Operacion_anfochanger"') IS NULL AND to_regclass('operacion_anfochanger') IS NOT NULL THEN
                        EXECUTE 'ALTER TABLE operacion_anfochanger RENAME TO "Operacion_anfochanger"';
                    END IF;
                    IF to_regclass('"Operacion_scalamin"') IS NULL AND to_regclass('operacion_scalamin') IS NOT NULL THEN
                        EXECUTE 'ALTER TABLE operacion_scalamin RENAME TO "Operacion_scalamin"';
                    END IF;
                    IF to_regclass('"Operacion_dumper"') IS NULL AND to_regclass('operacion_dumper') IS NOT NULL THEN
                        EXECUTE 'ALTER TABLE operacion_dumper RENAME TO "Operacion_dumper"';
                    END IF;
                END $$;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
