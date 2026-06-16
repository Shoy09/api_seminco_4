using System.Text.Json.Serialization;

namespace Seminco.Application.Mediciones;

public sealed record MedicionHorizontalDto(
    int Id,
    [property: JsonPropertyName("fecha")] string Fecha,
    [property: JsonPropertyName("turno")] string? Turno,
    [property: JsonPropertyName("empresa")] string? Empresa,
    [property: JsonPropertyName("zona")] string? Zona,
    [property: JsonPropertyName("labor")] string? Labor,
    [property: JsonPropertyName("veta")] string? Veta,
    [property: JsonPropertyName("tipo_perforacion")] string? TipoPerforacion,
    [property: JsonPropertyName("kg_explosivos")] double? KgExplosivos,
    [property: JsonPropertyName("avance_programado")] double? AvanceProgramado,
    [property: JsonPropertyName("ancho")] double? Ancho,
    [property: JsonPropertyName("alto")] double? Alto,
    [property: JsonPropertyName("envio")] int Envio,
    [property: JsonPropertyName("id_explosivo")] int? IdExplosivo,
    [property: JsonPropertyName("idnube")] int? IdNube,
    [property: JsonPropertyName("no_aplica")] int NoAplica,
    [property: JsonPropertyName("remanente")] int Remanente);
