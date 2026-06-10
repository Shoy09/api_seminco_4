using System.Text.Json;
using System.Text.Json.Serialization;

namespace Seminco.Application.Operaciones;

public sealed record OperacionDto(
    int Id,
    [property: JsonPropertyName("tipo")] string Tipo,
    [property: JsonPropertyName("fecha")] string? Fecha,
    [property: JsonPropertyName("turno")] string? Turno,
    [property: JsonPropertyName("seccion")] string? Seccion,
    [property: JsonPropertyName("operador")] string? Operador,
    [property: JsonPropertyName("jefe_guardia")] string? JefeGuardia,
    [property: JsonPropertyName("equipo")] string? Equipo,
    [property: JsonPropertyName("n_equipo")] string? NEquipo,
    [property: JsonPropertyName("modelo_equipo")] string? ModeloEquipo,
    [property: JsonPropertyName("capacidad")] string? Capacidad,
    [property: JsonPropertyName("tipo_equipo")] string? TipoEquipo,
    [property: JsonPropertyName("programa_trabajo")] JsonElement? ProgramaTrabajo,
    [property: JsonPropertyName("check_list_telemando")] JsonElement? CheckListTelemando,
    [property: JsonPropertyName("registros")] JsonElement? Registros,
    [property: JsonPropertyName("horometros")] JsonElement? Horometros,
    [property: JsonPropertyName("condiciones_equipo")] JsonElement? CondicionesEquipo,
    [property: JsonPropertyName("check_list")] JsonElement? CheckList,
    [property: JsonPropertyName("control_llantas")] JsonElement? ControlLlantas,
    [property: JsonPropertyName("estado")] string? Estado,
    [property: JsonPropertyName("envio")] int? Envio,
    [property: JsonPropertyName("revisado")] int? Revisado,
    [property: JsonPropertyName("aprobacion")] int? Aprobacion,
    [property: JsonPropertyName("observaciones_jefe")] JsonElement? ObservacionesJefe,
    [property: JsonPropertyName("observaciones_jefe2")] JsonElement? ObservacionesJefe2,
    [property: JsonPropertyName("observaciones_jefe3")] JsonElement? ObservacionesJefe3);
