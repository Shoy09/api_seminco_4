using System.Text.Json;
using System.Text.Json.Serialization;

namespace Seminco.Application.Planes;

[method: JsonConstructor]
public sealed record PlanMensualDto(
    int Id,
    [property: JsonPropertyName("anio")] int Anio,
    [property: JsonPropertyName("mes")] string? Mes,
    [property: JsonPropertyName("minado_tipo")] string? MinadoTipo,
    [property: JsonPropertyName("empresa")] string? Empresa,
    [property: JsonPropertyName("zona")] string? Zona,
    [property: JsonPropertyName("area")] string? Area,
    [property: JsonPropertyName("tipo_mineral")] string? TipoMineral,
    [property: JsonPropertyName("fase")] string? Fase,
    [property: JsonPropertyName("estructura_veta")] string? EstructuraVeta,
    [property: JsonPropertyName("nivel")] string? Nivel,
    [property: JsonPropertyName("tipo_labor")] string? TipoLabor,
    [property: JsonPropertyName("labor")] string? Labor,
    [property: JsonPropertyName("ala")] string? Ala,
    [property: JsonPropertyName("avance_m")] double? AvanceM,
    [property: JsonPropertyName("ancho_m")] double? AnchoM,
    [property: JsonPropertyName("alto_m")] double? AltoM,
    [property: JsonPropertyName("tms")] double? Tms,
    [property: JsonPropertyName("programado")] string? Programado,
    [property: JsonPropertyName("createdAt")] DateTime? CreatedAt,
    [property: JsonPropertyName("updatedAt")] DateTime? UpdatedAt
)
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? Columnas { get; set; }
}
