using Microsoft.EntityFrameworkCore;
using Seminco.Domain.Users;

namespace Seminco.Infrastructure.Persistence;

public sealed class SemincoDbContext(DbContextOptions<SemincoDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

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
    }
}
