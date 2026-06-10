using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Seminco.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExploracionesYMediciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "OperacionBaseSequence");

            migrationBuilder.CreateTable(
                name: "accesorios",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo = table.Column<string>(type: "text", nullable: false),
                    tipo_accesorio = table.Column<string>(type: "text", nullable: true),
                    costo = table.Column<decimal>(type: "numeric", nullable: false),
                    unidad_medida = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accesorios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "checklist_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    proceso = table.Column<string>(type: "text", nullable: true),
                    categoria = table.Column<string>(type: "text", nullable: true),
                    nombre = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checklist_items", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "checklists_telemando",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checklists_telemando", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "equipos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    proceso = table.Column<string>(type: "text", nullable: false),
                    codigo = table.Column<string>(type: "text", nullable: true),
                    marca = table.Column<string>(type: "text", nullable: true),
                    modelo = table.Column<string>(type: "text", nullable: true),
                    serie = table.Column<string>(type: "text", nullable: true),
                    anioFabricacion = table.Column<string>(type: "text", nullable: true),
                    fechaIngreso = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    capacidadYd3 = table.Column<double>(type: "double precision", nullable: true),
                    capacidadM3 = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "estados",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    estado_principal = table.Column<string>(type: "text", nullable: false),
                    codigo = table.Column<string>(type: "text", nullable: true),
                    tipo_estado = table.Column<string>(type: "text", nullable: true),
                    categoria = table.Column<string>(type: "text", nullable: true),
                    proceso = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estados", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "explisivos_uni",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dato = table.Column<double>(type: "double precision", nullable: false),
                    tipo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_explisivos_uni", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "explosivos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo = table.Column<string>(type: "text", nullable: false),
                    tipo_explosivo = table.Column<string>(type: "text", nullable: true),
                    cantidad_por_caja = table.Column<int>(type: "integer", nullable: true),
                    peso_unitario = table.Column<double>(type: "double precision", nullable: true),
                    costo_por_kg = table.Column<decimal>(type: "numeric", nullable: true),
                    unidad_medida = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_explosivos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "fechas_plan_mensual",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mes = table.Column<string>(type: "text", nullable: false),
                    fecha_ingreso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fechas_plan_mensual", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "longitud_barras",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    proceso = table.Column<string>(type: "text", nullable: true),
                    longitud_pies = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_longitud_barras", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mallas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tipo_malla = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mallas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mediciones_horizontal",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fecha = table.Column<string>(type: "text", nullable: false),
                    turno = table.Column<string>(type: "text", nullable: true),
                    empresa = table.Column<string>(type: "text", nullable: true),
                    zona = table.Column<string>(type: "text", nullable: true),
                    labor = table.Column<string>(type: "text", nullable: true),
                    veta = table.Column<string>(type: "text", nullable: true),
                    tipo_perforacion = table.Column<string>(type: "text", nullable: true),
                    kg_explosivos = table.Column<double>(type: "double precision", nullable: true),
                    avance_programado = table.Column<double>(type: "double precision", nullable: true),
                    ancho = table.Column<double>(type: "double precision", nullable: true),
                    alto = table.Column<double>(type: "double precision", nullable: true),
                    envio = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    id_explosivo = table.Column<int>(type: "integer", nullable: true),
                    idnube = table.Column<int>(type: "integer", nullable: true),
                    no_aplica = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    remanente = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mediciones_horizontal", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nube_datos_trabajo_exploraciones",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fecha = table.Column<string>(type: "text", nullable: false),
                    turno = table.Column<string>(type: "text", nullable: false),
                    taladro = table.Column<string>(type: "text", nullable: false),
                    pies_por_taladro = table.Column<string>(type: "text", nullable: false),
                    zona = table.Column<string>(type: "text", nullable: false),
                    tipo_labor = table.Column<string>(type: "text", nullable: false),
                    labor = table.Column<string>(type: "text", nullable: false),
                    ala = table.Column<string>(type: "text", nullable: true),
                    veta = table.Column<string>(type: "text", nullable: false),
                    nivel = table.Column<string>(type: "text", nullable: false),
                    tipo_perforacion = table.Column<string>(type: "text", nullable: false),
                    estado = table.Column<string>(type: "text", nullable: false, defaultValue: "Creado"),
                    cerrado = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    envio = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    semanaDefault = table.Column<string>(type: "text", nullable: true),
                    semanaSelect = table.Column<string>(type: "text", nullable: true),
                    empresa = table.Column<string>(type: "text", nullable: true),
                    seccion = table.Column<string>(type: "text", nullable: true),
                    medicion = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nube_datos_trabajo_exploraciones", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "numero_retardos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    longitud = table.Column<decimal>(type: "numeric", nullable: false),
                    tipo = table.Column<string>(type: "text", nullable: false),
                    codigo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_numero_retardos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Operacion_anfochanger",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"OperacionBaseSequence\"')"),
                    fecha = table.Column<string>(type: "text", nullable: true),
                    turno = table.Column<string>(type: "text", nullable: true),
                    operador = table.Column<string>(type: "text", nullable: true),
                    jefe_guardia = table.Column<string>(type: "text", nullable: true),
                    equipo = table.Column<string>(type: "text", nullable: true),
                    n_equipo = table.Column<string>(type: "text", nullable: true),
                    registros = table.Column<string>(type: "text", nullable: true),
                    horometros = table.Column<string>(type: "text", nullable: true),
                    condiciones_equipo = table.Column<string>(type: "text", nullable: true),
                    check_list = table.Column<string>(type: "text", nullable: true),
                    control_llantas = table.Column<string>(type: "text", nullable: true),
                    estado = table.Column<string>(type: "text", nullable: false, defaultValue: "activo"),
                    envio = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    revisado = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    aprobacion = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    observaciones_jefe = table.Column<string>(type: "text", nullable: true),
                    observaciones_jefe2 = table.Column<string>(type: "text", nullable: true),
                    observaciones_jefe3 = table.Column<string>(type: "text", nullable: true),
                    seccion = table.Column<string>(type: "text", nullable: true),
                    modelo_equipo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operacion_anfochanger", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Operacion_carguio",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"OperacionBaseSequence\"')"),
                    fecha = table.Column<string>(type: "text", nullable: true),
                    turno = table.Column<string>(type: "text", nullable: true),
                    operador = table.Column<string>(type: "text", nullable: true),
                    jefe_guardia = table.Column<string>(type: "text", nullable: true),
                    equipo = table.Column<string>(type: "text", nullable: true),
                    n_equipo = table.Column<string>(type: "text", nullable: true),
                    registros = table.Column<string>(type: "text", nullable: true),
                    horometros = table.Column<string>(type: "text", nullable: true),
                    condiciones_equipo = table.Column<string>(type: "text", nullable: true),
                    check_list = table.Column<string>(type: "text", nullable: true),
                    control_llantas = table.Column<string>(type: "text", nullable: true),
                    estado = table.Column<string>(type: "text", nullable: false, defaultValue: "activo"),
                    envio = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    revisado = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    aprobacion = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    observaciones_jefe = table.Column<string>(type: "text", nullable: true),
                    observaciones_jefe2 = table.Column<string>(type: "text", nullable: true),
                    observaciones_jefe3 = table.Column<string>(type: "text", nullable: true),
                    seccion = table.Column<string>(type: "text", nullable: true),
                    capacidad = table.Column<string>(type: "text", nullable: true),
                    tipo_equipo = table.Column<string>(type: "text", nullable: true),
                    programa_trabajo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operacion_carguio", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Operacion_dumper",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"OperacionBaseSequence\"')"),
                    fecha = table.Column<string>(type: "text", nullable: true),
                    turno = table.Column<string>(type: "text", nullable: true),
                    operador = table.Column<string>(type: "text", nullable: true),
                    jefe_guardia = table.Column<string>(type: "text", nullable: true),
                    equipo = table.Column<string>(type: "text", nullable: true),
                    n_equipo = table.Column<string>(type: "text", nullable: true),
                    registros = table.Column<string>(type: "text", nullable: true),
                    horometros = table.Column<string>(type: "text", nullable: true),
                    condiciones_equipo = table.Column<string>(type: "text", nullable: true),
                    check_list = table.Column<string>(type: "text", nullable: true),
                    control_llantas = table.Column<string>(type: "text", nullable: true),
                    estado = table.Column<string>(type: "text", nullable: false, defaultValue: "activo"),
                    envio = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    revisado = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    aprobacion = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    observaciones_jefe = table.Column<string>(type: "text", nullable: true),
                    observaciones_jefe2 = table.Column<string>(type: "text", nullable: true),
                    observaciones_jefe3 = table.Column<string>(type: "text", nullable: true),
                    seccion = table.Column<string>(type: "text", nullable: true),
                    capacidad = table.Column<string>(type: "text", nullable: true),
                    tipo_equipo = table.Column<string>(type: "text", nullable: true),
                    programa_trabajo = table.Column<string>(type: "text", nullable: true),
                    check_list_telemando = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operacion_dumper", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Operacion_empernador",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"OperacionBaseSequence\"')"),
                    fecha = table.Column<string>(type: "text", nullable: true),
                    turno = table.Column<string>(type: "text", nullable: true),
                    operador = table.Column<string>(type: "text", nullable: true),
                    jefe_guardia = table.Column<string>(type: "text", nullable: true),
                    equipo = table.Column<string>(type: "text", nullable: true),
                    n_equipo = table.Column<string>(type: "text", nullable: true),
                    registros = table.Column<string>(type: "text", nullable: true),
                    horometros = table.Column<string>(type: "text", nullable: true),
                    condiciones_equipo = table.Column<string>(type: "text", nullable: true),
                    check_list = table.Column<string>(type: "text", nullable: true),
                    control_llantas = table.Column<string>(type: "text", nullable: true),
                    estado = table.Column<string>(type: "text", nullable: false, defaultValue: "activo"),
                    envio = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    revisado = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    aprobacion = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    observaciones_jefe = table.Column<string>(type: "text", nullable: true),
                    observaciones_jefe2 = table.Column<string>(type: "text", nullable: true),
                    observaciones_jefe3 = table.Column<string>(type: "text", nullable: true),
                    seccion = table.Column<string>(type: "text", nullable: true),
                    tipo_equipo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operacion_empernador", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Operacion_rompebanco",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"OperacionBaseSequence\"')"),
                    fecha = table.Column<string>(type: "text", nullable: true),
                    turno = table.Column<string>(type: "text", nullable: true),
                    operador = table.Column<string>(type: "text", nullable: true),
                    jefe_guardia = table.Column<string>(type: "text", nullable: true),
                    equipo = table.Column<string>(type: "text", nullable: true),
                    n_equipo = table.Column<string>(type: "text", nullable: true),
                    registros = table.Column<string>(type: "text", nullable: true),
                    horometros = table.Column<string>(type: "text", nullable: true),
                    condiciones_equipo = table.Column<string>(type: "text", nullable: true),
                    check_list = table.Column<string>(type: "text", nullable: true),
                    control_llantas = table.Column<string>(type: "text", nullable: true),
                    estado = table.Column<string>(type: "text", nullable: false, defaultValue: "activo"),
                    envio = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    revisado = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    aprobacion = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    observaciones_jefe = table.Column<string>(type: "text", nullable: true),
                    observaciones_jefe2 = table.Column<string>(type: "text", nullable: true),
                    observaciones_jefe3 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operacion_rompebanco", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Operacion_scalamin",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"OperacionBaseSequence\"')"),
                    fecha = table.Column<string>(type: "text", nullable: true),
                    turno = table.Column<string>(type: "text", nullable: true),
                    operador = table.Column<string>(type: "text", nullable: true),
                    jefe_guardia = table.Column<string>(type: "text", nullable: true),
                    equipo = table.Column<string>(type: "text", nullable: true),
                    n_equipo = table.Column<string>(type: "text", nullable: true),
                    registros = table.Column<string>(type: "text", nullable: true),
                    horometros = table.Column<string>(type: "text", nullable: true),
                    condiciones_equipo = table.Column<string>(type: "text", nullable: true),
                    check_list = table.Column<string>(type: "text", nullable: true),
                    control_llantas = table.Column<string>(type: "text", nullable: true),
                    estado = table.Column<string>(type: "text", nullable: false, defaultValue: "activo"),
                    envio = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    revisado = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    aprobacion = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    observaciones_jefe = table.Column<string>(type: "text", nullable: true),
                    observaciones_jefe2 = table.Column<string>(type: "text", nullable: true),
                    observaciones_jefe3 = table.Column<string>(type: "text", nullable: true),
                    seccion = table.Column<string>(type: "text", nullable: true),
                    modelo_equipo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operacion_scalamin", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Operacion_scissor",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"OperacionBaseSequence\"')"),
                    fecha = table.Column<string>(type: "text", nullable: true),
                    turno = table.Column<string>(type: "text", nullable: true),
                    operador = table.Column<string>(type: "text", nullable: true),
                    jefe_guardia = table.Column<string>(type: "text", nullable: true),
                    equipo = table.Column<string>(type: "text", nullable: true),
                    n_equipo = table.Column<string>(type: "text", nullable: true),
                    registros = table.Column<string>(type: "text", nullable: true),
                    horometros = table.Column<string>(type: "text", nullable: true),
                    condiciones_equipo = table.Column<string>(type: "text", nullable: true),
                    check_list = table.Column<string>(type: "text", nullable: true),
                    control_llantas = table.Column<string>(type: "text", nullable: true),
                    estado = table.Column<string>(type: "text", nullable: false, defaultValue: "activo"),
                    envio = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    revisado = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    aprobacion = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    observaciones_jefe = table.Column<string>(type: "text", nullable: true),
                    observaciones_jefe2 = table.Column<string>(type: "text", nullable: true),
                    observaciones_jefe3 = table.Column<string>(type: "text", nullable: true),
                    seccion = table.Column<string>(type: "text", nullable: true),
                    modelo_equipo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operacion_scissor", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Operacion_tal_horizontal",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"OperacionBaseSequence\"')"),
                    fecha = table.Column<string>(type: "text", nullable: true),
                    turno = table.Column<string>(type: "text", nullable: true),
                    operador = table.Column<string>(type: "text", nullable: true),
                    jefe_guardia = table.Column<string>(type: "text", nullable: true),
                    equipo = table.Column<string>(type: "text", nullable: true),
                    n_equipo = table.Column<string>(type: "text", nullable: true),
                    registros = table.Column<string>(type: "text", nullable: true),
                    horometros = table.Column<string>(type: "text", nullable: true),
                    condiciones_equipo = table.Column<string>(type: "text", nullable: true),
                    check_list = table.Column<string>(type: "text", nullable: true),
                    control_llantas = table.Column<string>(type: "text", nullable: true),
                    estado = table.Column<string>(type: "text", nullable: false, defaultValue: "activo"),
                    envio = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    revisado = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    aprobacion = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    observaciones_jefe = table.Column<string>(type: "text", nullable: true),
                    observaciones_jefe2 = table.Column<string>(type: "text", nullable: true),
                    observaciones_jefe3 = table.Column<string>(type: "text", nullable: true),
                    seccion = table.Column<string>(type: "text", nullable: true),
                    modelo_equipo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operacion_tal_horizontal", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Operacion_tal_largo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"OperacionBaseSequence\"')"),
                    fecha = table.Column<string>(type: "text", nullable: true),
                    turno = table.Column<string>(type: "text", nullable: true),
                    operador = table.Column<string>(type: "text", nullable: true),
                    jefe_guardia = table.Column<string>(type: "text", nullable: true),
                    equipo = table.Column<string>(type: "text", nullable: true),
                    n_equipo = table.Column<string>(type: "text", nullable: true),
                    registros = table.Column<string>(type: "text", nullable: true),
                    horometros = table.Column<string>(type: "text", nullable: true),
                    condiciones_equipo = table.Column<string>(type: "text", nullable: true),
                    check_list = table.Column<string>(type: "text", nullable: true),
                    control_llantas = table.Column<string>(type: "text", nullable: true),
                    estado = table.Column<string>(type: "text", nullable: false, defaultValue: "activo"),
                    envio = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    revisado = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    aprobacion = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    observaciones_jefe = table.Column<string>(type: "text", nullable: true),
                    observaciones_jefe2 = table.Column<string>(type: "text", nullable: true),
                    observaciones_jefe3 = table.Column<string>(type: "text", nullable: true),
                    seccion = table.Column<string>(type: "text", nullable: true),
                    modelo_equipo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operacion_tal_largo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "origen_destino",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    proceso = table.Column<string>(type: "text", nullable: true),
                    tipo = table.Column<string>(type: "text", nullable: true),
                    nombre = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_origen_destino", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pernos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tipo_perno = table.Column<string>(type: "text", nullable: true),
                    longitud = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pernos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "plan_mensual",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MinadoTipo = table.Column<string>(type: "text", nullable: true),
                    Empresa = table.Column<string>(type: "text", nullable: true),
                    Zona = table.Column<string>(type: "text", nullable: true),
                    Area = table.Column<string>(type: "text", nullable: true),
                    TipoMineral = table.Column<string>(type: "text", nullable: true),
                    Fase = table.Column<string>(type: "text", nullable: true),
                    EstructuraVeta = table.Column<string>(type: "text", nullable: true),
                    Nivel = table.Column<string>(type: "text", nullable: true),
                    TipoLabor = table.Column<string>(type: "text", nullable: true),
                    Labor = table.Column<string>(type: "text", nullable: true),
                    Ala = table.Column<string>(type: "text", nullable: true),
                    AvanceM = table.Column<double>(type: "double precision", nullable: true),
                    AnchoM = table.Column<double>(type: "double precision", nullable: true),
                    AltoM = table.Column<double>(type: "double precision", nullable: true),
                    Tms = table.Column<double>(type: "double precision", nullable: true),
                    anio = table.Column<int>(type: "integer", nullable: false),
                    mes = table.Column<string>(type: "text", nullable: true),
                    col_1A = table.Column<string>(type: "text", nullable: true),
                    col_1B = table.Column<string>(type: "text", nullable: true),
                    col_2A = table.Column<string>(type: "text", nullable: true),
                    col_2B = table.Column<string>(type: "text", nullable: true),
                    col_3A = table.Column<string>(type: "text", nullable: true),
                    col_3B = table.Column<string>(type: "text", nullable: true),
                    col_4A = table.Column<string>(type: "text", nullable: true),
                    col_4B = table.Column<string>(type: "text", nullable: true),
                    col_5A = table.Column<string>(type: "text", nullable: true),
                    col_5B = table.Column<string>(type: "text", nullable: true),
                    col_6A = table.Column<string>(type: "text", nullable: true),
                    col_6B = table.Column<string>(type: "text", nullable: true),
                    col_7A = table.Column<string>(type: "text", nullable: true),
                    col_7B = table.Column<string>(type: "text", nullable: true),
                    col_8A = table.Column<string>(type: "text", nullable: true),
                    col_8B = table.Column<string>(type: "text", nullable: true),
                    col_9A = table.Column<string>(type: "text", nullable: true),
                    col_9B = table.Column<string>(type: "text", nullable: true),
                    col_10A = table.Column<string>(type: "text", nullable: true),
                    col_10B = table.Column<string>(type: "text", nullable: true),
                    col_11A = table.Column<string>(type: "text", nullable: true),
                    col_11B = table.Column<string>(type: "text", nullable: true),
                    col_12A = table.Column<string>(type: "text", nullable: true),
                    col_12B = table.Column<string>(type: "text", nullable: true),
                    col_13A = table.Column<string>(type: "text", nullable: true),
                    col_13B = table.Column<string>(type: "text", nullable: true),
                    col_14A = table.Column<string>(type: "text", nullable: true),
                    col_14B = table.Column<string>(type: "text", nullable: true),
                    col_15A = table.Column<string>(type: "text", nullable: true),
                    col_15B = table.Column<string>(type: "text", nullable: true),
                    col_16A = table.Column<string>(type: "text", nullable: true),
                    col_16B = table.Column<string>(type: "text", nullable: true),
                    col_17A = table.Column<string>(type: "text", nullable: true),
                    col_17B = table.Column<string>(type: "text", nullable: true),
                    col_18A = table.Column<string>(type: "text", nullable: true),
                    col_18B = table.Column<string>(type: "text", nullable: true),
                    col_19A = table.Column<string>(type: "text", nullable: true),
                    col_19B = table.Column<string>(type: "text", nullable: true),
                    col_20A = table.Column<string>(type: "text", nullable: true),
                    col_20B = table.Column<string>(type: "text", nullable: true),
                    col_21A = table.Column<string>(type: "text", nullable: true),
                    col_21B = table.Column<string>(type: "text", nullable: true),
                    col_22A = table.Column<string>(type: "text", nullable: true),
                    col_22B = table.Column<string>(type: "text", nullable: true),
                    col_23A = table.Column<string>(type: "text", nullable: true),
                    col_23B = table.Column<string>(type: "text", nullable: true),
                    col_24A = table.Column<string>(type: "text", nullable: true),
                    col_24B = table.Column<string>(type: "text", nullable: true),
                    col_25A = table.Column<string>(type: "text", nullable: true),
                    col_25B = table.Column<string>(type: "text", nullable: true),
                    col_26A = table.Column<string>(type: "text", nullable: true),
                    col_26B = table.Column<string>(type: "text", nullable: true),
                    col_27A = table.Column<string>(type: "text", nullable: true),
                    col_27B = table.Column<string>(type: "text", nullable: true),
                    col_28A = table.Column<string>(type: "text", nullable: true),
                    col_28B = table.Column<string>(type: "text", nullable: true),
                    programado = table.Column<string>(type: "text", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plan_mensual", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "planmetraje",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Semana = table.Column<string>(type: "text", nullable: true),
                    Mina = table.Column<string>(type: "text", nullable: true),
                    Zona = table.Column<string>(type: "text", nullable: true),
                    Area = table.Column<string>(type: "text", nullable: true),
                    Fase = table.Column<string>(type: "text", nullable: true),
                    MinadoTipo = table.Column<string>(type: "text", nullable: true),
                    TipoLabor = table.Column<string>(type: "text", nullable: true),
                    TipoMineral = table.Column<string>(type: "text", nullable: true),
                    EstructuraVeta = table.Column<string>(type: "text", nullable: true),
                    Nivel = table.Column<string>(type: "text", nullable: true),
                    Block = table.Column<string>(type: "text", nullable: true),
                    Labor = table.Column<string>(type: "text", nullable: true),
                    Ala = table.Column<string>(type: "text", nullable: true),
                    AnchoVeta = table.Column<double>(type: "double precision", nullable: true),
                    AnchoMinadoSem = table.Column<double>(type: "double precision", nullable: true),
                    AnchoMinadoMes = table.Column<double>(type: "double precision", nullable: true),
                    Burden = table.Column<double>(type: "double precision", nullable: true),
                    Espaciamiento = table.Column<double>(type: "double precision", nullable: true),
                    LongitudPerforacion = table.Column<double>(type: "double precision", nullable: true),
                    anio = table.Column<int>(type: "integer", nullable: false),
                    mes = table.Column<string>(type: "text", nullable: true),
                    columna_1A = table.Column<string>(type: "text", nullable: true),
                    columna_1B = table.Column<string>(type: "text", nullable: true),
                    columna_2A = table.Column<string>(type: "text", nullable: true),
                    columna_2B = table.Column<string>(type: "text", nullable: true),
                    columna_3A = table.Column<string>(type: "text", nullable: true),
                    columna_3B = table.Column<string>(type: "text", nullable: true),
                    columna_4A = table.Column<string>(type: "text", nullable: true),
                    columna_4B = table.Column<string>(type: "text", nullable: true),
                    columna_5A = table.Column<string>(type: "text", nullable: true),
                    columna_5B = table.Column<string>(type: "text", nullable: true),
                    columna_6A = table.Column<string>(type: "text", nullable: true),
                    columna_6B = table.Column<string>(type: "text", nullable: true),
                    columna_7A = table.Column<string>(type: "text", nullable: true),
                    columna_7B = table.Column<string>(type: "text", nullable: true),
                    columna_8A = table.Column<string>(type: "text", nullable: true),
                    columna_8B = table.Column<string>(type: "text", nullable: true),
                    columna_9A = table.Column<string>(type: "text", nullable: true),
                    columna_9B = table.Column<string>(type: "text", nullable: true),
                    columna_10A = table.Column<string>(type: "text", nullable: true),
                    columna_10B = table.Column<string>(type: "text", nullable: true),
                    columna_11A = table.Column<string>(type: "text", nullable: true),
                    columna_11B = table.Column<string>(type: "text", nullable: true),
                    columna_12A = table.Column<string>(type: "text", nullable: true),
                    columna_12B = table.Column<string>(type: "text", nullable: true),
                    columna_13A = table.Column<string>(type: "text", nullable: true),
                    columna_13B = table.Column<string>(type: "text", nullable: true),
                    columna_14A = table.Column<string>(type: "text", nullable: true),
                    columna_14B = table.Column<string>(type: "text", nullable: true),
                    columna_15A = table.Column<string>(type: "text", nullable: true),
                    columna_15B = table.Column<string>(type: "text", nullable: true),
                    columna_16A = table.Column<string>(type: "text", nullable: true),
                    columna_16B = table.Column<string>(type: "text", nullable: true),
                    columna_17A = table.Column<string>(type: "text", nullable: true),
                    columna_17B = table.Column<string>(type: "text", nullable: true),
                    columna_18A = table.Column<string>(type: "text", nullable: true),
                    columna_18B = table.Column<string>(type: "text", nullable: true),
                    columna_19A = table.Column<string>(type: "text", nullable: true),
                    columna_19B = table.Column<string>(type: "text", nullable: true),
                    columna_20A = table.Column<string>(type: "text", nullable: true),
                    columna_20B = table.Column<string>(type: "text", nullable: true),
                    columna_21A = table.Column<string>(type: "text", nullable: true),
                    columna_21B = table.Column<string>(type: "text", nullable: true),
                    columna_22A = table.Column<string>(type: "text", nullable: true),
                    columna_22B = table.Column<string>(type: "text", nullable: true),
                    columna_23A = table.Column<string>(type: "text", nullable: true),
                    columna_23B = table.Column<string>(type: "text", nullable: true),
                    columna_24A = table.Column<string>(type: "text", nullable: true),
                    columna_24B = table.Column<string>(type: "text", nullable: true),
                    columna_25A = table.Column<string>(type: "text", nullable: true),
                    columna_25B = table.Column<string>(type: "text", nullable: true),
                    columna_26A = table.Column<string>(type: "text", nullable: true),
                    columna_26B = table.Column<string>(type: "text", nullable: true),
                    columna_27A = table.Column<string>(type: "text", nullable: true),
                    columna_27B = table.Column<string>(type: "text", nullable: true),
                    columna_28A = table.Column<string>(type: "text", nullable: true),
                    columna_28B = table.Column<string>(type: "text", nullable: true),
                    programado = table.Column<string>(type: "text", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_planmetraje", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "planproduccions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Semana = table.Column<string>(type: "text", nullable: true),
                    Mina = table.Column<string>(type: "text", nullable: true),
                    Zona = table.Column<string>(type: "text", nullable: true),
                    Area = table.Column<string>(type: "text", nullable: true),
                    Fase = table.Column<string>(type: "text", nullable: true),
                    MinadoTipo = table.Column<string>(type: "text", nullable: true),
                    TipoLabor = table.Column<string>(type: "text", nullable: true),
                    TipoMineral = table.Column<string>(type: "text", nullable: true),
                    EstructuraVeta = table.Column<string>(type: "text", nullable: true),
                    Nivel = table.Column<string>(type: "text", nullable: true),
                    Block = table.Column<string>(type: "text", nullable: true),
                    Labor = table.Column<string>(type: "text", nullable: true),
                    Ala = table.Column<string>(type: "text", nullable: true),
                    AnchoVeta = table.Column<double>(type: "double precision", nullable: true),
                    AnchoMinadoSem = table.Column<double>(type: "double precision", nullable: true),
                    AnchoMinadoMes = table.Column<double>(type: "double precision", nullable: true),
                    AgGr = table.Column<double>(type: "double precision", nullable: true),
                    PorcentajeCu = table.Column<double>(type: "double precision", nullable: true),
                    PorcentajePb = table.Column<double>(type: "double precision", nullable: true),
                    PorcentajeZn = table.Column<double>(type: "double precision", nullable: true),
                    VptAct = table.Column<double>(type: "double precision", nullable: true),
                    VptFinal = table.Column<double>(type: "double precision", nullable: true),
                    CutOff1 = table.Column<double>(type: "double precision", nullable: true),
                    CutOff2 = table.Column<double>(type: "double precision", nullable: true),
                    anio = table.Column<int>(type: "integer", nullable: false),
                    mes = table.Column<string>(type: "text", nullable: true),
                    columna_1A = table.Column<string>(type: "text", nullable: true),
                    columna_1B = table.Column<string>(type: "text", nullable: true),
                    columna_2A = table.Column<string>(type: "text", nullable: true),
                    columna_2B = table.Column<string>(type: "text", nullable: true),
                    columna_3A = table.Column<string>(type: "text", nullable: true),
                    columna_3B = table.Column<string>(type: "text", nullable: true),
                    columna_4A = table.Column<string>(type: "text", nullable: true),
                    columna_4B = table.Column<string>(type: "text", nullable: true),
                    columna_5A = table.Column<string>(type: "text", nullable: true),
                    columna_5B = table.Column<string>(type: "text", nullable: true),
                    columna_6A = table.Column<string>(type: "text", nullable: true),
                    columna_6B = table.Column<string>(type: "text", nullable: true),
                    columna_7A = table.Column<string>(type: "text", nullable: true),
                    columna_7B = table.Column<string>(type: "text", nullable: true),
                    columna_8A = table.Column<string>(type: "text", nullable: true),
                    columna_8B = table.Column<string>(type: "text", nullable: true),
                    columna_9A = table.Column<string>(type: "text", nullable: true),
                    columna_9B = table.Column<string>(type: "text", nullable: true),
                    columna_10A = table.Column<string>(type: "text", nullable: true),
                    columna_10B = table.Column<string>(type: "text", nullable: true),
                    columna_11A = table.Column<string>(type: "text", nullable: true),
                    columna_11B = table.Column<string>(type: "text", nullable: true),
                    columna_12A = table.Column<string>(type: "text", nullable: true),
                    columna_12B = table.Column<string>(type: "text", nullable: true),
                    columna_13A = table.Column<string>(type: "text", nullable: true),
                    columna_13B = table.Column<string>(type: "text", nullable: true),
                    columna_14A = table.Column<string>(type: "text", nullable: true),
                    columna_14B = table.Column<string>(type: "text", nullable: true),
                    columna_15A = table.Column<string>(type: "text", nullable: true),
                    columna_15B = table.Column<string>(type: "text", nullable: true),
                    columna_16A = table.Column<string>(type: "text", nullable: true),
                    columna_16B = table.Column<string>(type: "text", nullable: true),
                    columna_17A = table.Column<string>(type: "text", nullable: true),
                    columna_17B = table.Column<string>(type: "text", nullable: true),
                    columna_18A = table.Column<string>(type: "text", nullable: true),
                    columna_18B = table.Column<string>(type: "text", nullable: true),
                    columna_19A = table.Column<string>(type: "text", nullable: true),
                    columna_19B = table.Column<string>(type: "text", nullable: true),
                    columna_20A = table.Column<string>(type: "text", nullable: true),
                    columna_20B = table.Column<string>(type: "text", nullable: true),
                    columna_21A = table.Column<string>(type: "text", nullable: true),
                    columna_21B = table.Column<string>(type: "text", nullable: true),
                    columna_22A = table.Column<string>(type: "text", nullable: true),
                    columna_22B = table.Column<string>(type: "text", nullable: true),
                    columna_23A = table.Column<string>(type: "text", nullable: true),
                    columna_23B = table.Column<string>(type: "text", nullable: true),
                    columna_24A = table.Column<string>(type: "text", nullable: true),
                    columna_24B = table.Column<string>(type: "text", nullable: true),
                    columna_25A = table.Column<string>(type: "text", nullable: true),
                    columna_25B = table.Column<string>(type: "text", nullable: true),
                    columna_26A = table.Column<string>(type: "text", nullable: true),
                    columna_26B = table.Column<string>(type: "text", nullable: true),
                    columna_27A = table.Column<string>(type: "text", nullable: true),
                    columna_27B = table.Column<string>(type: "text", nullable: true),
                    columna_28A = table.Column<string>(type: "text", nullable: true),
                    columna_28B = table.Column<string>(type: "text", nullable: true),
                    programado = table.Column<string>(type: "text", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_planproduccions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "secciones",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    proceso = table.Column<string>(type: "text", nullable: true),
                    nombre = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_secciones", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipo_equipos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipo_equipos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipoperforacions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    proceso = table.Column<string>(type: "text", nullable: true),
                    permitido_medicion = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipoperforacions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nube_despacho",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    datos_trabajo_id = table.Column<int>(type: "integer", nullable: false),
                    mili_segundo = table.Column<double>(type: "double precision", nullable: false),
                    medio_segundo = table.Column<double>(type: "double precision", nullable: false),
                    observaciones = table.Column<string>(type: "text", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nube_despacho", x => x.id);
                    table.ForeignKey(
                        name: "FK_nube_despacho_nube_datos_trabajo_exploraciones_datos_trabaj~",
                        column: x => x.datos_trabajo_id,
                        principalTable: "nube_datos_trabajo_exploraciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "nube_devoluciones",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    datos_trabajo_id = table.Column<int>(type: "integer", nullable: false),
                    mili_segundo = table.Column<double>(type: "double precision", nullable: false),
                    medio_segundo = table.Column<double>(type: "double precision", nullable: false),
                    observaciones = table.Column<string>(type: "text", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nube_devoluciones", x => x.id);
                    table.ForeignKey(
                        name: "FK_nube_devoluciones_nube_datos_trabajo_exploraciones_datos_tr~",
                        column: x => x.datos_trabajo_id,
                        principalTable: "nube_datos_trabajo_exploraciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "nube_despacho_detalle",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    despacho_id = table.Column<int>(type: "integer", nullable: false),
                    nombre_material = table.Column<string>(type: "text", nullable: false),
                    cantidad = table.Column<string>(type: "text", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nube_despacho_detalle", x => x.id);
                    table.ForeignKey(
                        name: "FK_nube_despacho_detalle_nube_despacho_despacho_id",
                        column: x => x.despacho_id,
                        principalTable: "nube_despacho",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "nube_detalle_despacho_explosivos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_despacho = table.Column<int>(type: "integer", nullable: false),
                    longitud = table.Column<double>(type: "double precision", nullable: false),
                    tipo = table.Column<string>(type: "text", nullable: false),
                    retardos = table.Column<string>(type: "jsonb", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nube_detalle_despacho_explosivos", x => x.id);
                    table.ForeignKey(
                        name: "FK_nube_detalle_despacho_explosivos_nube_despacho_id_despacho",
                        column: x => x.id_despacho,
                        principalTable: "nube_despacho",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "nube_detalle_devoluciones_explosivos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_devolucion = table.Column<int>(type: "integer", nullable: false),
                    longitud = table.Column<double>(type: "double precision", nullable: false),
                    tipo = table.Column<string>(type: "text", nullable: false),
                    retardos = table.Column<string>(type: "jsonb", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nube_detalle_devoluciones_explosivos", x => x.id);
                    table.ForeignKey(
                        name: "FK_nube_detalle_devoluciones_explosivos_nube_devoluciones_id_d~",
                        column: x => x.id_devolucion,
                        principalTable: "nube_devoluciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "nube_devolucion_detalle",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    devolucion_id = table.Column<int>(type: "integer", nullable: false),
                    nombre_material = table.Column<string>(type: "text", nullable: false),
                    cantidad = table.Column<string>(type: "text", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nube_devolucion_detalle", x => x.id);
                    table.ForeignKey(
                        name: "FK_nube_devolucion_detalle_nube_devoluciones_devolucion_id",
                        column: x => x.devolucion_id,
                        principalTable: "nube_devoluciones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_accesorios_codigo",
                table: "accesorios",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_explosivos_codigo",
                table: "explosivos",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_mediciones_horizontal_idnube",
                table: "mediciones_horizontal",
                column: "idnube",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_nube_despacho_datos_trabajo_id",
                table: "nube_despacho",
                column: "datos_trabajo_id");

            migrationBuilder.CreateIndex(
                name: "IX_nube_despacho_detalle_despacho_id",
                table: "nube_despacho_detalle",
                column: "despacho_id");

            migrationBuilder.CreateIndex(
                name: "IX_nube_detalle_despacho_explosivos_id_despacho",
                table: "nube_detalle_despacho_explosivos",
                column: "id_despacho");

            migrationBuilder.CreateIndex(
                name: "IX_nube_detalle_devoluciones_explosivos_id_devolucion",
                table: "nube_detalle_devoluciones_explosivos",
                column: "id_devolucion");

            migrationBuilder.CreateIndex(
                name: "IX_nube_devolucion_detalle_devolucion_id",
                table: "nube_devolucion_detalle",
                column: "devolucion_id");

            migrationBuilder.CreateIndex(
                name: "IX_nube_devoluciones_datos_trabajo_id",
                table: "nube_devoluciones",
                column: "datos_trabajo_id");

            migrationBuilder.CreateIndex(
                name: "ix_numero_retardos_codigo",
                table: "numero_retardos",
                column: "codigo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accesorios");

            migrationBuilder.DropTable(
                name: "checklist_items");

            migrationBuilder.DropTable(
                name: "checklists_telemando");

            migrationBuilder.DropTable(
                name: "equipos");

            migrationBuilder.DropTable(
                name: "estados");

            migrationBuilder.DropTable(
                name: "explisivos_uni");

            migrationBuilder.DropTable(
                name: "explosivos");

            migrationBuilder.DropTable(
                name: "fechas_plan_mensual");

            migrationBuilder.DropTable(
                name: "longitud_barras");

            migrationBuilder.DropTable(
                name: "mallas");

            migrationBuilder.DropTable(
                name: "mediciones_horizontal");

            migrationBuilder.DropTable(
                name: "nube_despacho_detalle");

            migrationBuilder.DropTable(
                name: "nube_detalle_despacho_explosivos");

            migrationBuilder.DropTable(
                name: "nube_detalle_devoluciones_explosivos");

            migrationBuilder.DropTable(
                name: "nube_devolucion_detalle");

            migrationBuilder.DropTable(
                name: "numero_retardos");

            migrationBuilder.DropTable(
                name: "Operacion_anfochanger");

            migrationBuilder.DropTable(
                name: "Operacion_carguio");

            migrationBuilder.DropTable(
                name: "Operacion_dumper");

            migrationBuilder.DropTable(
                name: "Operacion_empernador");

            migrationBuilder.DropTable(
                name: "Operacion_rompebanco");

            migrationBuilder.DropTable(
                name: "Operacion_scalamin");

            migrationBuilder.DropTable(
                name: "Operacion_scissor");

            migrationBuilder.DropTable(
                name: "Operacion_tal_horizontal");

            migrationBuilder.DropTable(
                name: "Operacion_tal_largo");

            migrationBuilder.DropTable(
                name: "origen_destino");

            migrationBuilder.DropTable(
                name: "pernos");

            migrationBuilder.DropTable(
                name: "plan_mensual");

            migrationBuilder.DropTable(
                name: "planmetraje");

            migrationBuilder.DropTable(
                name: "planproduccions");

            migrationBuilder.DropTable(
                name: "secciones");

            migrationBuilder.DropTable(
                name: "tipo_equipos");

            migrationBuilder.DropTable(
                name: "tipoperforacions");

            migrationBuilder.DropTable(
                name: "nube_despacho");

            migrationBuilder.DropTable(
                name: "nube_devoluciones");

            migrationBuilder.DropTable(
                name: "nube_datos_trabajo_exploraciones");

            migrationBuilder.DropSequence(
                name: "OperacionBaseSequence");
        }
    }
}
