namespace Seminco.Domain.Users;

public sealed class User
{
    public int Id { get; set; }
    public string CodigoDni { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Nombres { get; set; } = string.Empty;
    public string? Cargo { get; set; }
    public string? Rol { get; set; }
    public string? Area { get; set; }
    public string? Clasificacion { get; set; }
    public string? Empresa { get; set; }
    public string? Guardia { get; set; }
    public string? AutorizadoEquipo { get; set; }
    public string? Correo { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public string? Firma { get; set; }
    public string? OperacionesAutorizadas { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
