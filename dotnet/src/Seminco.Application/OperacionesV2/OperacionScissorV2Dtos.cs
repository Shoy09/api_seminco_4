using System.Text.Json;
using System.Text.Json.Serialization;

namespace Seminco.Application.OperacionesV2;

public sealed record OperacionScissorV2ResponseDto(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("message")] string Message);

public sealed record OperacionScissorUpsertRequest(
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
    [property: JsonPropertyName("registros")] List<OperacionScissorRegistroRequest>? Registros,
    [property: JsonPropertyName("observaciones_jefe")] JsonElement? ObservacionesJefe,
    [property: JsonPropertyName("observaciones_jefe2")] JsonElement? ObservacionesJefe2,
    [property: JsonPropertyName("observaciones_jefe3")] JsonElement? ObservacionesJefe3);

public sealed record OperacionScissorRegistroRequest(
    [property: JsonPropertyName("id")] long? Id,
    [property: JsonPropertyName("numero")] int Numero,
    [property: JsonPropertyName("estado")] string Estado,
    [property: JsonPropertyName("codigo")] string Codigo,
    [property: JsonPropertyName("hora_inicio")] string HoraInicio,
    [property: JsonPropertyName("hora_final")] string HoraFinal,
    [property: JsonPropertyName("operacion")] OperacionScissorRegistroDetalleRequest? Operacion);

public sealed record OperacionScissorRegistroDetalleRequest(
    [property: JsonPropertyName("origen_nivel")] string? OrigenNivel,
    [property: JsonPropertyName("origen_tipo_labor")] string? OrigenTipoLabor,
    [property: JsonPropertyName("origen_labor")] string? OrigenLabor,
    [property: JsonPropertyName("origen_ala")] string? OrigenAla,
    [property: JsonPropertyName("destino_nivel")] string? DestinoNivel,
    [property: JsonPropertyName("destino_tipo_labor")] string? DestinoTipoLabor,
    [property: JsonPropertyName("destino_labor")] string? DestinoLabor,
    [property: JsonPropertyName("destino_ala")] string? DestinoAla,
    [property: JsonPropertyName("observaciones")] string? Observaciones);
