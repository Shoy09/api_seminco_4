using System.Text.Json;
using System.Text.Json.Serialization;

namespace Seminco.Application.OperacionesV2;

public sealed record OperacionScalaminV2ResponseDto(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("message")] string Message);

public sealed record OperacionScalaminUpsertRequest(
    [property: JsonPropertyName("fecha")] string? Fecha,
    [property: JsonPropertyName("turno")] string? Turno,
    [property: JsonPropertyName("operador")] string? Operador,
    [property: JsonPropertyName("jefe_guardia")] string? JefeGuardia,
    [property: JsonPropertyName("equipo")] string? Equipo,
    [property: JsonPropertyName("n_equipo")] string? NEquipo,
    [property: JsonPropertyName("seccion")] string? Seccion,
    [property: JsonPropertyName("modelo_equipo")] string? ModeloEquipo,
    [property: JsonPropertyName("estado")] string? Estado,
    [property: JsonPropertyName("envio")] int? Envio,
    [property: JsonPropertyName("revisado")] int? Revisado,
    [property: JsonPropertyName("aprobacion")] int? Aprobacion,
    [property: JsonPropertyName("horometros")] JsonElement? Horometros,
    [property: JsonPropertyName("condiciones_equipo")] CondicionEquipoRequest? CondicionesEquipo,
    [property: JsonPropertyName("check_list")] List<ChecklistRespuestaRequest>? CheckList,
    [property: JsonPropertyName("control_llantas")] ControlLlantasRequest? ControlLlantas,
    [property: JsonPropertyName("registros")] List<OperacionScalaminRegistroRequest>? Registros,
    [property: JsonPropertyName("observaciones_jefe")] JsonElement? ObservacionesJefe,
    [property: JsonPropertyName("observaciones_jefe2")] JsonElement? ObservacionesJefe2,
    [property: JsonPropertyName("observaciones_jefe3")] JsonElement? ObservacionesJefe3);

public sealed record OperacionScalaminRegistroRequest(
    [property: JsonPropertyName("id")] long? Id,
    [property: JsonPropertyName("numero")] int Numero,
    [property: JsonPropertyName("estado")] string Estado,
    [property: JsonPropertyName("codigo")] string Codigo,
    [property: JsonPropertyName("hora_inicio")] string HoraInicio,
    [property: JsonPropertyName("hora_final")] string HoraFinal,
    [property: JsonPropertyName("operacion")] OperacionScalaminRegistroDetalleRequest? Operacion);

public sealed record OperacionScalaminRegistroDetalleRequest(
    [property: JsonPropertyName("nivel")] string? Nivel,
    [property: JsonPropertyName("tipo_labor")] string? TipoLabor,
    [property: JsonPropertyName("labor")] string? Labor,
    [property: JsonPropertyName("ala")] string? Ala,
    [property: JsonPropertyName("observaciones")] string? Observaciones);
