using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seminco.Domain.Catalogs;
using Seminco.Domain.Exploraciones;
using Seminco.Domain.Mediciones;
using Seminco.Domain.Operaciones;
using Seminco.Domain.Planes;
using Seminco.Domain.Users;
using Seminco.Domain.OperacionesV2;
namespace Seminco.Infrastructure.Persistence;

public sealed class SemincoDbContext(DbContextOptions<SemincoDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Equipo> Equipos => Set<Equipo>();
    public DbSet<Estado> Estados => Set<Estado>();
    public DbSet<TipoPerforacion> TiposPerforacion => Set<TipoPerforacion>();
    public DbSet<TipoEquipo> TiposEquipo => Set<TipoEquipo>();
    public DbSet<CheckListItem> CheckListItems => Set<CheckListItem>();
    public DbSet<ChecklistTelemando> ChecklistsTelemando => Set<ChecklistTelemando>();
    public DbSet<Seccion> Secciones => Set<Seccion>();
    public DbSet<LongitudBarra> LongitudesBarra => Set<LongitudBarra>();
    public DbSet<Perno> Pernos => Set<Perno>();
    public DbSet<Malla> Mallas => Set<Malla>();
    public DbSet<OrigenDestino> OrigenesDestino => Set<OrigenDestino>();
    public DbSet<Accesorio> Accesorios => Set<Accesorio>();
    public DbSet<Explosivo> Explosivos => Set<Explosivo>();
    public DbSet<ExplosivoUni> ExplosivosUni => Set<ExplosivoUni>();
    public DbSet<NumeroRetardo> NumerosRetardo => Set<NumeroRetardo>();
    public DbSet<OperacionTalLargo> OperacionesTalLargo => Set<OperacionTalLargo>();
    public DbSet<Domain.Operaciones.OperacionTalHorizontal> OperacionesTalHorizontal => Set<Domain.Operaciones.OperacionTalHorizontal>();
    public DbSet<OperacionEmpernador> OperacionesEmpernador => Set<OperacionEmpernador>();
    public DbSet<OperacionCarguio> OperacionesCarguio => Set<OperacionCarguio>();
    public DbSet<OperacionRompebanco> OperacionesRompebanco => Set<OperacionRompebanco>();
    public DbSet<OperacionScissor> OperacionesScissor => Set<OperacionScissor>();
    public DbSet<OperacionAnfochanger> OperacionesAnfochanger => Set<OperacionAnfochanger>();
    public DbSet<OperacionScalamin> OperacionesScalamin => Set<OperacionScalamin>();
    public DbSet<OperacionDumper> OperacionesDumper => Set<OperacionDumper>();
    public DbSet<PlanMensual> PlanesMensuales => Set<PlanMensual>();
    public DbSet<PlanMetraje> PlanesMetraje => Set<PlanMetraje>();
    public DbSet<PlanProduccion> PlanesProduccion => Set<PlanProduccion>();
    public DbSet<FechaPlanMensual> FechasPlanMensual => Set<FechaPlanMensual>();
    public DbSet<NubeExploracion> NubeExploraciones => Set<NubeExploracion>();
    public DbSet<NubeDespacho> NubeDespachos => Set<NubeDespacho>();
    public DbSet<NubeDespachoDetalle> NubeDespachoDetalles => Set<NubeDespachoDetalle>();
    public DbSet<NubeDetalleDespachoExplosivo> NubeDetalleDespachoExplosivos => Set<NubeDetalleDespachoExplosivo>();
    public DbSet<NubeDevolucion> NubeDevoluciones => Set<NubeDevolucion>();
    public DbSet<NubeDevolucionDetalle> NubeDevolucionDetalles => Set<NubeDevolucionDetalle>();
    public DbSet<NubeDetalleDevolucionExplosivo> NubeDetalleDevolucionExplosivos => Set<NubeDetalleDevolucionExplosivo>();
    public DbSet<MedicionHorizontal> MedicionesHorizontal => Set<MedicionHorizontal>();

    public DbSet<Seminco.Domain.OperacionesV2.OperacionTalHorizontal> OperacionesTalHorizontalV2 => Set<Domain.OperacionesV2.OperacionTalHorizontal>();
    public DbSet<OperacionTalHorizontalHorometro> OperacionesTalHorizontalHorometros => Set<OperacionTalHorizontalHorometro>();
    public DbSet<OperacionTalHorizontalCondicionEquipo> OperacionesTalHorizontalCondicionesEquipo => Set<OperacionTalHorizontalCondicionEquipo>();
    public DbSet<OperacionTalHorizontalChecklistRespuesta> OperacionesTalHorizontalChecklistRespuestas => Set<OperacionTalHorizontalChecklistRespuesta>();
    public DbSet<OperacionTalHorizontalControlLlanta> OperacionesTalHorizontalControlLlantas => Set<OperacionTalHorizontalControlLlanta>();
    public DbSet<OperacionTalHorizontalRegistro> OperacionesTalHorizontalRegistros => Set<OperacionTalHorizontalRegistro>();
    public DbSet<OperacionTalHorizontalRegistroDetalle> OperacionesTalHorizontalRegistroDetalles => Set<OperacionTalHorizontalRegistroDetalle>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("usuarios");
            entity.HasKey(user => user.Id);
            entity.Property(user => user.Id).HasColumnName("id");
            entity.Property(user => user.CodigoDni).HasColumnName("codigo_dni");
            entity.Property(user => user.Apellidos).HasColumnName("apellidos");
            entity.Property(user => user.Nombres).HasColumnName("nombres");
            entity.Property(user => user.Cargo).HasColumnName("cargo");
            entity.Property(user => user.Rol).HasColumnName("rol");
            entity.Property(user => user.Area).HasColumnName("area");
            entity.Property(user => user.Clasificacion).HasColumnName("clasificacion");
            entity.Property(user => user.Empresa).HasColumnName("empresa");
            entity.Property(user => user.Guardia).HasColumnName("guardia");
            entity.Property(user => user.AutorizadoEquipo).HasColumnName("autorizado_equipo");
            entity.Property(user => user.Correo).HasColumnName("correo");
            entity.Property(user => user.PasswordHash).HasColumnName("password");
            entity.Property(user => user.Firma).HasColumnName("firma");
            entity.Property(user => user.OperacionesAutorizadas).HasColumnName("operaciones_autorizadas").HasColumnType("jsonb");
            entity.Property(user => user.CreatedAt).HasColumnName("createdAt");
            entity.Property(user => user.UpdatedAt).HasColumnName("updatedAt");
            entity.HasIndex(user => user.CodigoDni).IsUnique().HasDatabaseName("ix_usuarios_codigo_dni");
            entity.HasIndex(user => user.Correo).IsUnique().HasDatabaseName("ix_usuarios_correo").HasFilter("\"correo\" IS NOT NULL");
        });

        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.ToTable("equipos");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
            entity.Property(e => e.Proceso).HasColumnName("proceso");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.Marca).HasColumnName("marca");
            entity.Property(e => e.Modelo).HasColumnName("modelo");
            entity.Property(e => e.Serie).HasColumnName("serie");
            entity.Property(e => e.AnioFabricacion).HasColumnName("anioFabricacion");
            entity.Property(e => e.FechaIngreso).HasColumnName("fechaIngreso");
            entity.Property(e => e.CapacidadYd3).HasColumnName("capacidadYd3");
            entity.Property(e => e.CapacidadM3).HasColumnName("capacidadM3");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.ToTable("estados");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstadoPrincipal).HasColumnName("estado_principal");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.TipoEstado).HasColumnName("tipo_estado");
            entity.Property(e => e.Categoria).HasColumnName("categoria");
            entity.Property(e => e.Proceso).HasColumnName("proceso");
        });

        modelBuilder.Entity<TipoPerforacion>(entity =>
        {
            entity.ToTable("tipoperforacions");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
            entity.Property(e => e.Proceso).HasColumnName("proceso");
            entity.Property(e => e.PermitidoMedicion).HasColumnName("permitido_medicion");
        });

        modelBuilder.Entity<TipoEquipo>(entity =>
        {
            entity.ToTable("tipo_equipos");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
        });

        modelBuilder.Entity<CheckListItem>(entity =>
        {
            entity.ToTable("checklist_items");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Proceso).HasColumnName("proceso");
            entity.Property(e => e.Categoria).HasColumnName("categoria");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
        });

        modelBuilder.Entity<ChecklistTelemando>(entity =>
        {
            entity.ToTable("checklists_telemando");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
        });

        modelBuilder.Entity<Seccion>(entity =>
        {
            entity.ToTable("secciones");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Proceso).HasColumnName("proceso");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
        });

        modelBuilder.Entity<LongitudBarra>(entity =>
        {
            entity.ToTable("longitud_barras");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Proceso).HasColumnName("proceso");
            entity.Property(e => e.LongitudPies).HasColumnName("longitud_pies");
        });

        modelBuilder.Entity<Perno>(entity =>
        {
            entity.ToTable("pernos");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TipoPerno).HasColumnName("tipo_perno");
            entity.Property(e => e.Longitud).HasColumnName("longitud");
        });

        modelBuilder.Entity<Malla>(entity =>
        {
            entity.ToTable("mallas");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TipoMalla).HasColumnName("tipo_malla");
        });

        modelBuilder.Entity<OrigenDestino>(entity =>
        {
            entity.ToTable("origen_destino");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Proceso).HasColumnName("proceso");
            entity.Property(e => e.Tipo).HasColumnName("tipo");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
        });

        modelBuilder.Entity<Accesorio>(entity =>
        {
            entity.ToTable("accesorios");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.TipoAccesorio).HasColumnName("tipo_accesorio");
            entity.Property(e => e.Costo).HasColumnName("costo");
            entity.Property(e => e.UnidadMedida).HasColumnName("unidad_medida");
            entity.HasIndex(e => e.Codigo).IsUnique().HasDatabaseName("ix_accesorios_codigo");
        });

        modelBuilder.Entity<Explosivo>(entity =>
        {
            entity.ToTable("explosivos");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.Property(e => e.TipoExplosivo).HasColumnName("tipo_explosivo");
            entity.Property(e => e.CantidadPorCaja).HasColumnName("cantidad_por_caja");
            entity.Property(e => e.PesoUnitario).HasColumnName("peso_unitario");
            entity.Property(e => e.CostoPorKg).HasColumnName("costo_por_kg");
            entity.Property(e => e.UnidadMedida).HasColumnName("unidad_medida");
            entity.HasIndex(e => e.Codigo).IsUnique().HasDatabaseName("ix_explosivos_codigo");
        });

        modelBuilder.Entity<ExplosivoUni>(entity =>
        {
            entity.ToTable("explisivos_uni");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Dato).HasColumnName("dato");
            entity.Property(e => e.Tipo).HasColumnName("tipo");
        });

        modelBuilder.Entity<NumeroRetardo>(entity =>
        {
            entity.ToTable("numero_retardos");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Longitud).HasColumnName("longitud");
            entity.Property(e => e.Tipo).HasColumnName("tipo");
            entity.Property(e => e.Codigo).HasColumnName("codigo");
            entity.HasIndex(e => e.Codigo).IsUnique().HasDatabaseName("ix_numero_retardos_codigo");
        });

        ConfigureOperaciones(modelBuilder);
        ConfigurePlanes(modelBuilder);
        ConfigureExploraciones(modelBuilder);
        ConfigureMediciones(modelBuilder);
        ConfigureOperacionesTalHorizontalV2(modelBuilder);
    }

    private static void ConfigureMediciones(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MedicionHorizontal>(e =>
        {
            e.ToTable("mediciones_horizontal");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            e.Property(x => x.Fecha).HasColumnName("fecha");
            e.Property(x => x.Turno).HasColumnName("turno");
            e.Property(x => x.Empresa).HasColumnName("empresa");
            e.Property(x => x.Zona).HasColumnName("zona");
            e.Property(x => x.Labor).HasColumnName("labor");
            e.Property(x => x.Veta).HasColumnName("veta");
            e.Property(x => x.TipoPerforacion).HasColumnName("tipo_perforacion");
            e.Property(x => x.KgExplosivos).HasColumnName("kg_explosivos");
            e.Property(x => x.AvanceProgramado).HasColumnName("avance_programado");
            e.Property(x => x.Ancho).HasColumnName("ancho");
            e.Property(x => x.Alto).HasColumnName("alto");
            e.Property(x => x.Envio).HasColumnName("envio").HasDefaultValue(0);
            e.Property(x => x.IdExplosivo).HasColumnName("id_explosivo");
            e.Property(x => x.IdNube).HasColumnName("idnube");
            e.Property(x => x.NoAplica).HasColumnName("no_aplica").HasDefaultValue(0);
            e.Property(x => x.Remanente).HasColumnName("remanente").HasDefaultValue(0);
            e.HasIndex(x => x.IdNube).IsUnique();
        });
    }

    private static void ConfigureExploraciones(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NubeExploracion>(e =>
        {
            e.ToTable("nube_datos_trabajo_exploraciones");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            e.Property(x => x.Fecha).HasColumnName("fecha");
            e.Property(x => x.Turno).HasColumnName("turno");
            e.Property(x => x.Taladro).HasColumnName("taladro");
            e.Property(x => x.PiesPorTaladro).HasColumnName("pies_por_taladro");
            e.Property(x => x.Zona).HasColumnName("zona");
            e.Property(x => x.TipoLabor).HasColumnName("tipo_labor");
            e.Property(x => x.Labor).HasColumnName("labor");
            e.Property(x => x.Ala).HasColumnName("ala");
            e.Property(x => x.Veta).HasColumnName("veta");
            e.Property(x => x.Nivel).HasColumnName("nivel");
            e.Property(x => x.TipoPerforacion).HasColumnName("tipo_perforacion");
            e.Property(x => x.Estado).HasColumnName("estado").HasDefaultValue("Creado");
            e.Property(x => x.Cerrado).HasColumnName("cerrado").HasDefaultValue(0);
            e.Property(x => x.Envio).HasColumnName("envio").HasDefaultValue(0);
            e.Property(x => x.SemanaDefault).HasColumnName("semanaDefault");
            e.Property(x => x.SemanaSelect).HasColumnName("semanaSelect");
            e.Property(x => x.Empresa).HasColumnName("empresa");
            e.Property(x => x.Seccion).HasColumnName("seccion");
            e.Property(x => x.Medicion).HasColumnName("medicion").HasDefaultValue(0);
            e.Property(x => x.CreatedAt).HasColumnName("createdAt");
            e.Property(x => x.UpdatedAt).HasColumnName("updatedAt");
            e.HasMany(x => x.Despachos).WithOne(x => x.DatosTrabajo).HasForeignKey(x => x.DatosTrabajoId).OnDelete(DeleteBehavior.Cascade);
            e.HasMany(x => x.Devoluciones).WithOne(x => x.DatosTrabajo).HasForeignKey(x => x.DatosTrabajoId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<NubeDespacho>(e =>
        {
            e.ToTable("nube_despacho");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            e.Property(x => x.DatosTrabajoId).HasColumnName("datos_trabajo_id");
            e.Property(x => x.MiliSegundo).HasColumnName("mili_segundo");
            e.Property(x => x.MedioSegundo).HasColumnName("medio_segundo");
            e.Property(x => x.Observaciones).HasColumnName("observaciones");
            e.Property(x => x.CreatedAt).HasColumnName("createdAt");
            e.Property(x => x.UpdatedAt).HasColumnName("updatedAt");
            e.HasMany(x => x.Detalles).WithOne(x => x.Despacho).HasForeignKey(x => x.DespachoId).OnDelete(DeleteBehavior.Cascade);
            e.HasMany(x => x.DetallesExplosivos).WithOne(x => x.Despacho).HasForeignKey(x => x.IdDespacho).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<NubeDespachoDetalle>(e =>
        {
            e.ToTable("nube_despacho_detalle");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            e.Property(x => x.DespachoId).HasColumnName("despacho_id");
            e.Property(x => x.NombreMaterial).HasColumnName("nombre_material");
            e.Property(x => x.Cantidad).HasColumnName("cantidad");
            e.Property(x => x.CreatedAt).HasColumnName("createdAt");
            e.Property(x => x.UpdatedAt).HasColumnName("updatedAt");
        });

        modelBuilder.Entity<NubeDetalleDespachoExplosivo>(e =>
        {
            e.ToTable("nube_detalle_despacho_explosivos");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            e.Property(x => x.IdDespacho).HasColumnName("id_despacho");
            e.Property(x => x.Longitud).HasColumnName("longitud");
            e.Property(x => x.Tipo).HasColumnName("tipo");
            e.Property(x => x.Retardos).HasColumnName("retardos").HasColumnType("jsonb");
            e.Property(x => x.CreatedAt).HasColumnName("createdAt");
            e.Property(x => x.UpdatedAt).HasColumnName("updatedAt");
        });

        modelBuilder.Entity<NubeDevolucion>(e =>
        {
            e.ToTable("nube_devoluciones");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            e.Property(x => x.DatosTrabajoId).HasColumnName("datos_trabajo_id");
            e.Property(x => x.MiliSegundo).HasColumnName("mili_segundo");
            e.Property(x => x.MedioSegundo).HasColumnName("medio_segundo");
            e.Property(x => x.Observaciones).HasColumnName("observaciones");
            e.Property(x => x.CreatedAt).HasColumnName("createdAt");
            e.Property(x => x.UpdatedAt).HasColumnName("updatedAt");
            e.HasMany(x => x.Detalles).WithOne(x => x.Devolucion).HasForeignKey(x => x.DevolucionId).OnDelete(DeleteBehavior.Cascade);
            e.HasMany(x => x.DetallesExplosivos).WithOne(x => x.Devolucion).HasForeignKey(x => x.IdDevolucion).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<NubeDevolucionDetalle>(e =>
        {
            e.ToTable("nube_devolucion_detalle");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            e.Property(x => x.DevolucionId).HasColumnName("devolucion_id");
            e.Property(x => x.NombreMaterial).HasColumnName("nombre_material");
            e.Property(x => x.Cantidad).HasColumnName("cantidad");
            e.Property(x => x.CreatedAt).HasColumnName("createdAt");
            e.Property(x => x.UpdatedAt).HasColumnName("updatedAt");
        });

        modelBuilder.Entity<NubeDetalleDevolucionExplosivo>(e =>
        {
            e.ToTable("nube_detalle_devoluciones_explosivos");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            e.Property(x => x.IdDevolucion).HasColumnName("id_devolucion");
            e.Property(x => x.Longitud).HasColumnName("longitud");
            e.Property(x => x.Tipo).HasColumnName("tipo");
            e.Property(x => x.Retardos).HasColumnName("retardos").HasColumnType("jsonb");
            e.Property(x => x.CreatedAt).HasColumnName("createdAt");
            e.Property(x => x.UpdatedAt).HasColumnName("updatedAt");
        });
    }

    private static void ConfigurePlanes(ModelBuilder modelBuilder)
    {
        void Base<T>(EntityTypeBuilder<T> e, string colPrefix) where T : PlanBase
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Id).HasColumnName("id").ValueGeneratedOnAdd();
            e.Property(p => p.Anio).HasColumnName("anio");
            e.Property(p => p.Mes).HasColumnName("mes");
            e.Property(p => p.Programado).HasColumnName("programado");
            e.Property(p => p.CreatedAt).HasColumnName("createdAt");
            e.Property(p => p.UpdatedAt).HasColumnName("updatedAt");
            for (var d = 1; d <= 28; d++)
            {
                e.Property<string?>($"Col{d}A").HasColumnName($"{colPrefix}{d}A");
                e.Property<string?>($"Col{d}B").HasColumnName($"{colPrefix}{d}B");
            }
        }

        void Map<T>(string table, string colPrefix) where T : PlanBase
        {
            modelBuilder.Entity<T>(e =>
            {
                e.ToTable(table);
                Base(e, colPrefix);
            });
        }

        Map<PlanMensual>("plan_mensual", "col_");
        Map<PlanMetraje>("planmetraje", "columna_");
        Map<PlanProduccion>("planproduccions", "columna_");

        modelBuilder.Entity<FechaPlanMensual>(e =>
        {
            e.ToTable("fechas_plan_mensual");
            e.HasKey(f => f.Id);
            e.Property(f => f.Id).HasColumnName("id").ValueGeneratedOnAdd();
            e.Property(f => f.Mes).HasColumnName("mes");
            e.Property(f => f.FechaIngreso).HasColumnName("fecha_ingreso");
        });
    }

    private static void ConfigureOperaciones(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OperacionBase>(entity =>
        {
            entity.UseTpcMappingStrategy();

            entity.HasKey(op => op.Id);

            entity.Property(op => op.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(op => op.Fecha).HasColumnName("fecha");
            entity.Property(op => op.Turno).HasColumnName("turno");
            entity.Property(op => op.Operador).HasColumnName("operador");
            entity.Property(op => op.JefeGuardia).HasColumnName("jefe_guardia");
            entity.Property(op => op.Equipo).HasColumnName("equipo");
            entity.Property(op => op.NEquipo).HasColumnName("n_equipo");
            entity.Property(op => op.Registros).HasColumnName("registros");
            entity.Property(op => op.Horometros).HasColumnName("horometros");
            entity.Property(op => op.CondicionesEquipo).HasColumnName("condiciones_equipo");
            entity.Property(op => op.CheckList).HasColumnName("check_list");
            entity.Property(op => op.ControlLlantas).HasColumnName("control_llantas");

            entity.Property(op => op.Estado)
                .HasColumnName("estado")
                .HasDefaultValue("activo");

            entity.Property(op => op.Envio)
                .HasColumnName("envio")
                .HasDefaultValue(0);

            entity.Property(op => op.Revisado)
                .HasColumnName("revisado")
                .HasDefaultValue(0);

            entity.Property(op => op.Aprobacion)
                .HasColumnName("aprobacion")
                .HasDefaultValue(0);

            entity.Property(op => op.ObservacionesJefe).HasColumnName("observaciones_jefe");
            entity.Property(op => op.ObservacionesJefe2).HasColumnName("observaciones_jefe2");
            entity.Property(op => op.ObservacionesJefe3).HasColumnName("observaciones_jefe3");
        });

        modelBuilder.Entity<OperacionTalLargo>(e =>
        {
            e.ToTable("Operacion_tal_largo");

            e.Property(op => op.Seccion).HasColumnName("seccion");
            e.Property(op => op.ModeloEquipo).HasColumnName("modelo_equipo");
        });

        modelBuilder.Entity<Domain.Operaciones.OperacionTalHorizontal>(e =>
        {
            e.ToTable("Operacion_tal_horizontal");

            e.Property(op => op.Seccion).HasColumnName("seccion");
            e.Property(op => op.ModeloEquipo).HasColumnName("modelo_equipo");
        });

        modelBuilder.Entity<OperacionEmpernador>(e =>
        {
            e.ToTable("Operacion_empernador");

            e.Property(op => op.Seccion).HasColumnName("seccion");
            e.Property(op => op.TipoEquipo).HasColumnName("tipo_equipo");
        });

        modelBuilder.Entity<OperacionCarguio>(e =>
        {
            e.ToTable("Operacion_carguio");

            e.Property(op => op.Seccion).HasColumnName("seccion");
            e.Property(op => op.Capacidad).HasColumnName("capacidad");
            e.Property(op => op.TipoEquipo).HasColumnName("tipo_equipo");
            e.Property(op => op.ProgramaTrabajo).HasColumnName("programa_trabajo");
        });

        modelBuilder.Entity<OperacionRompebanco>(e =>
        {
            e.ToTable("Operacion_rompebanco");
        });

        modelBuilder.Entity<OperacionScissor>(e =>
        {
            e.ToTable("Operacion_scissor");

            e.Property(op => op.Seccion).HasColumnName("seccion");
            e.Property(op => op.ModeloEquipo).HasColumnName("modelo_equipo");
        });

        modelBuilder.Entity<OperacionAnfochanger>(e =>
        {
            e.ToTable("Operacion_anfochanger");

            e.Property(op => op.Seccion).HasColumnName("seccion");
            e.Property(op => op.ModeloEquipo).HasColumnName("modelo_equipo");
        });

        modelBuilder.Entity<OperacionScalamin>(e =>
        {
            e.ToTable("Operacion_scalamin");

            e.Property(op => op.Seccion).HasColumnName("seccion");
            e.Property(op => op.ModeloEquipo).HasColumnName("modelo_equipo");
        });

        modelBuilder.Entity<OperacionDumper>(e =>
        {
            e.ToTable("Operacion_dumper");

            e.Property(op => op.Seccion).HasColumnName("seccion");
            e.Property(op => op.Capacidad).HasColumnName("capacidad");
            e.Property(op => op.TipoEquipo).HasColumnName("tipo_equipo");
            e.Property(op => op.ProgramaTrabajo).HasColumnName("programa_trabajo");
            e.Property(op => op.CheckListTelemando).HasColumnName("check_list_telemando");
        });
    }

    private static void ConfigureOperacionesTalHorizontalV2(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Domain.OperacionesV2.OperacionTalHorizontal>(e =>
    {
        e.ToTable("operacion_tal_horizontal_v2");
        e.HasKey(x => x.Id);

        e.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
        e.Property(x => x.Fecha).HasColumnName("fecha");
        e.Property(x => x.Turno).HasColumnName("turno");
        e.Property(x => x.Operador).HasColumnName("operador");
        e.Property(x => x.JefeGuardia).HasColumnName("jefe_guardia");
        e.Property(x => x.EquipoId).HasColumnName("equipo_id");
        e.Property(x => x.EquipoNombre).HasColumnName("equipo_nombre");
        e.Property(x => x.NEquipo).HasColumnName("n_equipo");
        e.Property(x => x.Seccion).HasColumnName("seccion");
        e.Property(x => x.ModeloEquipo).HasColumnName("modelo_equipo");
        e.Property(x => x.Estado).HasColumnName("estado");
        e.Property(x => x.Envio).HasColumnName("envio");
        e.Property(x => x.Revisado).HasColumnName("revisado");
        e.Property(x => x.Aprobacion).HasColumnName("aprobacion");
        e.Property(x => x.ObservacionesJefe).HasColumnName("observaciones_jefe");
        e.Property(x => x.ObservacionesJefe2).HasColumnName("observaciones_jefe2");
        e.Property(x => x.ObservacionesJefe3).HasColumnName("observaciones_jefe3");
        e.Property(x => x.PayloadOriginal).HasColumnName("payload_original").HasColumnType("jsonb");
        e.Property(x => x.PayloadVersion).HasColumnName("payload_version");
        e.Property(x => x.ExternalSyncId).HasColumnName("external_sync_id");
        e.Property(x => x.DeviceId).HasColumnName("device_id");
        e.Property(x => x.CreatedAt).HasColumnName("created_at");
        e.Property(x => x.UpdatedAt).HasColumnName("updated_at");

        e.HasOne(x => x.CondicionEquipo)
            .WithOne(x => x.Operacion)
            .HasForeignKey<OperacionTalHorizontalCondicionEquipo>(x => x.OperacionId)
            .OnDelete(DeleteBehavior.Cascade);

        e.HasMany(x => x.Horometros)
            .WithOne(x => x.Operacion)
            .HasForeignKey(x => x.OperacionId)
            .OnDelete(DeleteBehavior.Cascade);

        e.HasMany(x => x.ChecklistRespuestas)
            .WithOne(x => x.Operacion)
            .HasForeignKey(x => x.OperacionId)
            .OnDelete(DeleteBehavior.Cascade);

        e.HasMany(x => x.ControlLlantas)
            .WithOne(x => x.Operacion)
            .HasForeignKey(x => x.OperacionId)
            .OnDelete(DeleteBehavior.Cascade);

        e.HasMany(x => x.Registros)
            .WithOne(x => x.Operacion)
            .HasForeignKey(x => x.OperacionId)
            .OnDelete(DeleteBehavior.Cascade);
    });

    modelBuilder.Entity<OperacionTalHorizontalHorometro>(e =>
    {
        e.ToTable("operacion_tal_horizontal_horometro");
        e.HasKey(x => x.Id);

        e.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
        e.Property(x => x.OperacionId).HasColumnName("operacion_id");
        e.Property(x => x.Tipo).HasColumnName("tipo");
        e.Property(x => x.Inicio).HasColumnName("inicio").HasPrecision(10, 2);
        e.Property(x => x.Final).HasColumnName("final").HasPrecision(10, 2);
        e.Property(x => x.Op).HasColumnName("op");
        e.Property(x => x.Inop).HasColumnName("inop");

        e.HasIndex(x => new { x.OperacionId, x.Tipo }).IsUnique();
    });

    modelBuilder.Entity<OperacionTalHorizontalCondicionEquipo>(e =>
    {
        e.ToTable("operacion_tal_horizontal_condicion_equipo");
        e.HasKey(x => x.OperacionId);

        e.Property(x => x.OperacionId).HasColumnName("operacion_id");
        e.Property(x => x.Op).HasColumnName("op");
        e.Property(x => x.NoOp).HasColumnName("no_op");
        e.Property(x => x.Lugar).HasColumnName("lugar");
        e.Property(x => x.Descripcion).HasColumnName("descripcion");
        e.Property(x => x.AceiteMotor).HasColumnName("aceite_motor");
        e.Property(x => x.AceiteHidraulico).HasColumnName("aceite_hidraulico");
        e.Property(x => x.AceiteTransmision).HasColumnName("aceite_transmision");
        e.Property(x => x.Combustible).HasColumnName("combustible");
        e.Property(x => x.HoraLlenado).HasColumnName("hora_llenado");
    });

    modelBuilder.Entity<OperacionTalHorizontalChecklistRespuesta>(e =>
    {
        e.ToTable("operacion_tal_horizontal_checklist");
        e.HasKey(x => x.Id);

        e.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
        e.Property(x => x.OperacionId).HasColumnName("operacion_id");
        e.Property(x => x.ChecklistItemId).HasColumnName("checklist_item_id");
        e.Property(x => x.CategoriaSnapshot).HasColumnName("categoria_snapshot");
        e.Property(x => x.DescripcionSnapshot).HasColumnName("descripcion_snapshot");
        e.Property(x => x.Decision).HasColumnName("decision");
        e.Property(x => x.Observacion).HasColumnName("observacion");

        e.HasOne(x => x.ChecklistItem)
            .WithMany()
            .HasForeignKey(x => x.ChecklistItemId)
            .OnDelete(DeleteBehavior.SetNull);
    });

    modelBuilder.Entity<OperacionTalHorizontalControlLlanta>(e =>
    {
        e.ToTable("operacion_tal_horizontal_control_llanta");
        e.HasKey(x => x.Id);

        e.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
        e.Property(x => x.OperacionId).HasColumnName("operacion_id");
        e.Property(x => x.Posicion).HasColumnName("posicion");
        e.Property(x => x.Estado).HasColumnName("estado");
        e.Property(x => x.Presion).HasColumnName("presion").HasPrecision(10, 2);
        e.Property(x => x.Observacion).HasColumnName("observacion");
    });

    modelBuilder.Entity<OperacionTalHorizontalRegistro>(e =>
    {
        e.ToTable("operacion_tal_horizontal_registro");
        e.HasKey(x => x.Id);

        e.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
        e.Property(x => x.OperacionId).HasColumnName("operacion_id");
        e.Property(x => x.ExternalId).HasColumnName("external_id");
        e.Property(x => x.Numero).HasColumnName("numero");
        e.Property(x => x.EstadoPrincipal).HasColumnName("estado_principal");
        e.Property(x => x.CodigoEstado).HasColumnName("codigo_estado");
        e.Property(x => x.EstadoCatalogoId).HasColumnName("estado_catalogo_id");
        e.Property(x => x.HoraInicio).HasColumnName("hora_inicio");
        e.Property(x => x.HoraFinal).HasColumnName("hora_final");
        e.Property(x => x.PayloadOperacion).HasColumnName("payload_operacion").HasColumnType("jsonb");
        e.Property(x => x.CreatedAt).HasColumnName("created_at");
        e.Property(x => x.UpdatedAt).HasColumnName("updated_at");

        e.HasOne(x => x.Detalle)
            .WithOne(x => x.Registro)
            .HasForeignKey<OperacionTalHorizontalRegistroDetalle>(x => x.RegistroId)
            .OnDelete(DeleteBehavior.Cascade);

        e.HasIndex(x => x.OperacionId);
        e.HasIndex(x => x.CodigoEstado);
        e.HasIndex(x => x.EstadoPrincipal);
    });

    modelBuilder.Entity<OperacionTalHorizontalRegistroDetalle>(e =>
    {
        e.ToTable("operacion_tal_horizontal_registro_detalle");
        e.HasKey(x => x.RegistroId);

        e.Property(x => x.RegistroId).HasColumnName("registro_id");
        e.Property(x => x.Nivel).HasColumnName("nivel");
        e.Property(x => x.TipoLabor).HasColumnName("tipo_labor");
        e.Property(x => x.Labor).HasColumnName("labor");
        e.Property(x => x.Ala).HasColumnName("ala");
        e.Property(x => x.TalProd).HasColumnName("tal_prod").HasPrecision(10, 2);
        e.Property(x => x.TalRimados).HasColumnName("tal_rimados").HasPrecision(10, 2);
        e.Property(x => x.TalAlivio).HasColumnName("tal_alivio").HasPrecision(10, 2);
        e.Property(x => x.TalRepaso).HasColumnName("tal_repaso").HasPrecision(10, 2);
        e.Property(x => x.LongBarras).HasColumnName("long_barras").HasPrecision(10, 2);
        e.Property(x => x.NumBarras).HasColumnName("num_barras").HasPrecision(10, 2);
        e.Property(x => x.TipoPerforacion).HasColumnName("tipo_perforacion");
        e.Property(x => x.TipoPerforacionId).HasColumnName("tipo_perforacion_id");
        e.Property(x => x.Observaciones).HasColumnName("observaciones");
    });
}

}
