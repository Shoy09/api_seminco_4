using System.Text.Json;
using System.Text.Json.Serialization;

namespace Seminco.Application.Planes;

[method: JsonConstructor]
public sealed record PlanProduccionDto(
    int Id,
    [property: JsonPropertyName("anio")] int Anio,
    [property: JsonPropertyName("mes")] string? Mes,
    [property: JsonPropertyName("semana")] string? Semana,
    [property: JsonPropertyName("mina")] string? Mina,
    [property: JsonPropertyName("zona")] string? Zona,
    [property: JsonPropertyName("area")] string? Area,
    [property: JsonPropertyName("fase")] string? Fase,
    [property: JsonPropertyName("minado_tipo")] string? MinadoTipo,
    [property: JsonPropertyName("tipo_labor")] string? TipoLabor,
    [property: JsonPropertyName("tipo_mineral")] string? TipoMineral,
    [property: JsonPropertyName("estructura_veta")] string? EstructuraVeta,
    [property: JsonPropertyName("nivel")] string? Nivel,
    [property: JsonPropertyName("block")] string? Block,
    [property: JsonPropertyName("labor")] string? Labor,
    [property: JsonPropertyName("ala")] string? Ala,
    [property: JsonPropertyName("ancho_veta")] double? AnchoVeta,
    [property: JsonPropertyName("ancho_minado_sem")] double? AnchoMinadoSem,
    [property: JsonPropertyName("ancho_minado_mes")] double? AnchoMinadoMes,
    [property: JsonPropertyName("ag_gr")] double? AgGr,
    [property: JsonPropertyName("porcentaje_cu")] double? PorcentajeCu,
    [property: JsonPropertyName("porcentaje_pb")] double? PorcentajePb,
    [property: JsonPropertyName("porcentaje_zn")] double? PorcentajeZn,
    [property: JsonPropertyName("vpt_act")] double? VptAct,
    [property: JsonPropertyName("vpt_final")] double? VptFinal,
    [property: JsonPropertyName("cut_off_1")] double? CutOff1,
    [property: JsonPropertyName("cut_off_2")] double? CutOff2,
    [property: JsonPropertyName("programado")] string? Programado,
    [property: JsonPropertyName("createdAt")] DateTime? CreatedAt,
    [property: JsonPropertyName("updatedAt")] DateTime? UpdatedAt
)
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? Columnas { get; set; }
}
