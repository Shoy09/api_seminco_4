using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seminco.Domain.Catalogs;
using Seminco.Domain.Operaciones;
using Seminco.Domain.Users;

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
    public DbSet<OperacionTalHorizontal> OperacionesTalHorizontal => Set<OperacionTalHorizontal>();
    public DbSet<OperacionEmpernador> OperacionesEmpernador => Set<OperacionEmpernador>();
    public DbSet<OperacionCarguio> OperacionesCarguio => Set<OperacionCarguio>();
    public DbSet<OperacionRompebanco> OperacionesRompebanco => Set<OperacionRompebanco>();
    public DbSet<OperacionScissor> OperacionesScissor => Set<OperacionScissor>();
    public DbSet<OperacionAnfochanger> OperacionesAnfochanger => Set<OperacionAnfochanger>();
    public DbSet<OperacionScalamin> OperacionesScalamin => Set<OperacionScalamin>();
    public DbSet<OperacionDumper> OperacionesDumper => Set<OperacionDumper>();

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
    }

    private static void ConfigureOperaciones(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OperacionBase>(entity => entity.UseTpcMappingStrategy());

        void Base<T>(EntityTypeBuilder<T> e) where T : OperacionBase
        {
            e.HasKey(op => op.Id);
            e.Property(op => op.Id).HasColumnName("id").ValueGeneratedOnAdd();
            e.Property(op => op.Fecha).HasColumnName("fecha");
            e.Property(op => op.Turno).HasColumnName("turno");
            e.Property(op => op.Operador).HasColumnName("operador");
            e.Property(op => op.JefeGuardia).HasColumnName("jefe_guardia");
            e.Property(op => op.Equipo).HasColumnName("equipo");
            e.Property(op => op.NEquipo).HasColumnName("n_equipo");
            e.Property(op => op.Registros).HasColumnName("registros");
            e.Property(op => op.Horometros).HasColumnName("horometros");
            e.Property(op => op.CondicionesEquipo).HasColumnName("condiciones_equipo");
            e.Property(op => op.CheckList).HasColumnName("check_list");
            e.Property(op => op.ControlLlantas).HasColumnName("control_llantas");
            e.Property(op => op.Estado).HasColumnName("estado").HasDefaultValue("activo");
            e.Property(op => op.Envio).HasColumnName("envio").HasDefaultValue(0);
            e.Property(op => op.Revisado).HasColumnName("revisado").HasDefaultValue(0);
            e.Property(op => op.Aprobacion).HasColumnName("aprobacion").HasDefaultValue(0);
            e.Property(op => op.ObservacionesJefe).HasColumnName("observaciones_jefe");
            e.Property(op => op.ObservacionesJefe2).HasColumnName("observaciones_jefe2");
            e.Property(op => op.ObservacionesJefe3).HasColumnName("observaciones_jefe3");
        }

        modelBuilder.Entity<OperacionTalLargo>(e => { Base<OperacionTalLargo>(e); e.ToTable("Operacion_tal_largo"); e.Property(op => op.Seccion).HasColumnName("seccion"); e.Property(op => op.ModeloEquipo).HasColumnName("modelo_equipo"); });
        modelBuilder.Entity<OperacionTalHorizontal>(e => { Base<OperacionTalHorizontal>(e); e.ToTable("Operacion_tal_horizontal"); e.Property(op => op.Seccion).HasColumnName("seccion"); e.Property(op => op.ModeloEquipo).HasColumnName("modelo_equipo"); });
        modelBuilder.Entity<OperacionEmpernador>(e => { Base<OperacionEmpernador>(e); e.ToTable("Operacion_empernador"); e.Property(op => op.Seccion).HasColumnName("seccion"); e.Property(op => op.TipoEquipo).HasColumnName("tipo_equipo"); });
        modelBuilder.Entity<OperacionCarguio>(e => { Base<OperacionCarguio>(e); e.ToTable("Operacion_carguio"); e.Property(op => op.Seccion).HasColumnName("seccion"); e.Property(op => op.Capacidad).HasColumnName("capacidad"); e.Property(op => op.TipoEquipo).HasColumnName("tipo_equipo"); e.Property(op => op.ProgramaTrabajo).HasColumnName("programa_trabajo"); });
        modelBuilder.Entity<OperacionRompebanco>(e => { Base<OperacionRompebanco>(e); e.ToTable("Operacion_rompebanco"); });
        modelBuilder.Entity<OperacionScissor>(e => { Base<OperacionScissor>(e); e.ToTable("Operacion_scissor"); e.Property(op => op.Seccion).HasColumnName("seccion"); e.Property(op => op.ModeloEquipo).HasColumnName("modelo_equipo"); });
        modelBuilder.Entity<OperacionAnfochanger>(e => { Base<OperacionAnfochanger>(e); e.ToTable("Operacion_anfochanger"); e.Property(op => op.Seccion).HasColumnName("seccion"); e.Property(op => op.ModeloEquipo).HasColumnName("modelo_equipo"); });
        modelBuilder.Entity<OperacionScalamin>(e => { Base<OperacionScalamin>(e); e.ToTable("Operacion_scalamin"); e.Property(op => op.Seccion).HasColumnName("seccion"); e.Property(op => op.ModeloEquipo).HasColumnName("modelo_equipo"); });
        modelBuilder.Entity<OperacionDumper>(e => { Base<OperacionDumper>(e); e.ToTable("Operacion_dumper"); e.Property(op => op.Seccion).HasColumnName("seccion"); e.Property(op => op.Capacidad).HasColumnName("capacidad"); e.Property(op => op.TipoEquipo).HasColumnName("tipo_equipo"); e.Property(op => op.ProgramaTrabajo).HasColumnName("programa_trabajo"); e.Property(op => op.CheckListTelemando).HasColumnName("check_list_telemando"); });
    }
}
