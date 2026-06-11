using System.Text.Json;
using System.Text.Json.Serialization;

namespace Seminco.Application.OperacionesV2;

public sealed record OperacionCarguioV2ResponseDto(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("message")] string Message);

public sealed record OperacionCarguioUpsertRequest(
    [property: JsonPropertyName("fecha")] string? Fecha,
    [property: JsonPropertyName("turno")] string? Turno,
    [property: JsonPropertyName("operador")] string? Operador,
    [property: JsonPropertyName("jefe_guardia")] string? JefeGuardia,
    [property: JsonPropertyName("equipo")] string? Equipo,
    [property: JsonPropertyName("n_equipo")] string? NEquipo,
    [property: JsonPropertyName("seccion")] string? Seccion,
    [property: JsonPropertyName("capacidad")] string? Capacidad,
    [property: JsonPropertyName("tipo_equipo")] OperacionCarguioTipoEquipoRequest? TipoEquipo,
    [property: JsonPropertyName("estado")] string? Estado,
    [property: JsonPropertyName("envio")] int? Envio,
    [property: JsonPropertyName("revisado")] int? Revisado,
    [property: JsonPropertyName("aprobacion")] int? Aprobacion,
    [property: JsonPropertyName("horometros")] OperacionCarguioHorometrosRequest? Horometros,
    [property: JsonPropertyName("condiciones_equipo")] OperacionCarguioCondicionEquipoRequest? CondicionesEquipo,
    [property: JsonPropertyName("check_list")] List<OperacionCarguioChecklistRespuestaRequest>? CheckList,
    [property: JsonPropertyName("control_llantas")] OperacionCarguioControlLlantasRequest? ControlLlantas,
    [property: JsonPropertyName("programa_trabajo")] OperacionCarguioProgramaTrabajoRequest? ProgramaTrabajo,
    [property: JsonPropertyName("registros")] List<OperacionCarguioRegistroRequest>? Registros,
    [property: JsonPropertyName("observaciones_jefe")] JsonElement? ObservacionesJefe,
    [property: JsonPropertyName("observaciones_jefe2")] JsonElement? ObservacionesJefe2,
    [property: JsonPropertyName("observaciones_jefe3")] JsonElement? ObservacionesJefe3);

public sealed record OperacionCarguioTipoEquipoRequest(
    [property: JsonPropertyName("diesel")] bool Diesel,
    [property: JsonPropertyName("electrico")] bool Electrico);

public sealed record OperacionCarguioHorometrosRequest(
    [property: JsonPropertyName("horometro")] OperacionCarguioHorometroDetalleRequest? Horometro);

public sealed record OperacionCarguioHorometroDetalleRequest(
    [property: JsonPropertyName("inicio")] decimal? Inicio,
    [property: JsonPropertyName("final")] decimal? Final,
    [property: JsonPropertyName("op")] bool Op,
    [property: JsonPropertyName("inop")] bool Inop);

public sealed record OperacionCarguioCondicionEquipoRequest(
    [property: JsonPropertyName("op")] bool Op,
    [property: JsonPropertyName("noOp")] bool NoOp,
    [property: JsonPropertyName("lugar")] string? Lugar,
    [property: JsonPropertyName("descripcion")] string? Descripcion,
    [property: JsonPropertyName("aceiteMotor")] bool AceiteMotor,
    [property: JsonPropertyName("aceiteHidraulico")] bool AceiteHidraulico,
    [property: JsonPropertyName("aceiteTransmision")] bool AceiteTransmision,
    [property: JsonPropertyName("combustible")] string? Combustible,
    [property: JsonPropertyName("horaLlenado")] string? HoraLlenado);

public sealed record OperacionCarguioChecklistRespuestaRequest(
    [property: JsonPropertyName("descripcion")] string Descripcion,
    [property: JsonPropertyName("decision")] int Decision,
    [property: JsonPropertyName("observacion")] string? Observacion,
    [property: JsonPropertyName("categoria")] string Categoria);

public sealed record OperacionCarguioControlLlantasRequest(
    [property: JsonPropertyName("numero1")] bool? Numero1,
    [property: JsonPropertyName("numero2")] bool? Numero2,
    [property: JsonPropertyName("numero3")] bool? Numero3,
    [property: JsonPropertyName("numero4")] bool? Numero4);

public sealed record OperacionCarguioProgramaTrabajoRequest(
    [property: JsonPropertyName("n_cucharas_programado")] int? NCucharasProgramado,
    [property: JsonPropertyName("n_cucharas_realizado")] int? NCucharasRealizado);

public sealed record OperacionCarguioRegistroRequest(
    [property: JsonPropertyName("id")] long? Id,
    [property: JsonPropertyName("numero")] int Numero,
    [property: JsonPropertyName("estado")] string Estado,
    [property: JsonPropertyName("codigo")] string Codigo,
    [property: JsonPropertyName("hora_inicio")] string HoraInicio,
    [property: JsonPropertyName("hora_final")] string HoraFinal,
    [property: JsonPropertyName("operacion")] OperacionCarguioRegistroDetalleRequest? Operacion);

public sealed record OperacionCarguioRegistroDetalleRequest(
    [property: JsonPropertyName("nivel_inicio")] string? NivelInicio,
    [property: JsonPropertyName("tipo_labor_inicio")] string? TipoLaborInicio,
    [property: JsonPropertyName("labor_inicio")] string? LaborInicio,
    [property: JsonPropertyName("ala_inicio")] string? AlaInicio,
    [property: JsonPropertyName("ubicacion_destino")] string? UbicacionDestino,
    [property: JsonPropertyName("n_cucharas")] int? NCucharas,
    [property: JsonPropertyName("observaciones")] string? Observaciones);
