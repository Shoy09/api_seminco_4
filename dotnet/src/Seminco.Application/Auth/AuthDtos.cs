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
