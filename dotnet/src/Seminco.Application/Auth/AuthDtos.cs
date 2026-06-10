using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Seminco.Application.Auth;

public sealed record LoginRequest(
    [property: Required, JsonPropertyName("codigo_dni")] string CodigoDni,
    [property: Required] string Password);

public sealed record LoginResponse(
    [property: JsonPropertyName("token")] string Token,
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("codigo_dni")] string CodigoDni,
    [property: JsonPropertyName("apellidos")] string Apellidos,
    [property: JsonPropertyName("nombres")] string Nombres);

public class RegisterRequest
{
    [Required]
    [JsonPropertyName("codigo_dni")]
    public string CodigoDni { get; set;} = null!;
    [Required]
    [JsonPropertyName("nombres")]
    public string Nombres { get; set;} = null!;
    [Required]
    [JsonPropertyName("apellidos")]
    public string Apellidos { get; set;} = null!;
    [Required]
    [JsonPropertyName("password")]
    [MinLength(6)]
    public string Password { get; set;} = null!;
    [JsonPropertyName("correo")]
    public string? Correo { get; set;}
    [JsonPropertyName("cargo")]
    public string? Cargo { get; set;}
    [JsonPropertyName("area")]
    public string? Area { get; set;}
    [JsonPropertyName("clasificacion")]
    public string? Clasificacion { get; set;}
    [JsonPropertyName("empresa")]
    public string? Empresa { get; set;}
    [JsonPropertyName("guardia")]
    public string? Guardia { get; set;}
    [JsonPropertyName("autorizado_equipo")]
    public string? AutorizadoEquipo { get; set;}

    [JsonPropertyName("rol")]
    public string? Rol { get; set;} = "trabajador";
}


public sealed record RegisterResponse(
    int Id,
    [property: JsonPropertyName("codigo_dni")] string CodigoDni,
    string Apellidos,
    string Nombres,
    string? Cargo,
    string? Empresa,
    string? Guardia,
    [property: JsonPropertyName("autorizado_equipo")] string? AutorizadoEquipo,
    string? Correo,
    string? Rol,
    [property: JsonPropertyName("operaciones_autorizadas")] object? OperacionesAutorizadas);
