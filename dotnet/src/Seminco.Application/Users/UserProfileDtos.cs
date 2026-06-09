using System.Text.Json.Serialization;

namespace Seminco.Application.Users;

public sealed record UserProfileResponse(
    int Id,
    [property: JsonPropertyName("codigo_dni")]
    string CodigoDni,
    string Apellidos,
    string Nombres,
    string? Cargo,
    string? Empresa,
    string? Guardia,
    [property: JsonPropertyName("autorizado_equipo")]
    string? AutorizadoEquipo,
    string? Correo,
    string? Firma,
    string? Rol,
    [property: JsonPropertyName("operaciones_autorizadas")]
    object? OperacionesAutorizadas);
