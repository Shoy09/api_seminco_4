using System.Text.Json;
using System.Text.Json.Serialization;

namespace Seminco.Application.OperacionesV2;

public sealed record OperacionTalHorizontalV2ResponseDto(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("message")] string Message);

public sealed record OperacionTalHorizontalUpsertRequest(
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
    [property: JsonPropertyName("horometros")] HorometrosRequest? Horometros,
    [property: JsonPropertyName("condiciones_equipo")] CondicionEquipoRequest? CondicionesEquipo,
    [property: JsonPropertyName("check_list")] List<ChecklistRespuestaRequest>? CheckList,
    [property: JsonPropertyName("control_llantas")] ControlLlantasRequest? ControlLlantas,
    [property: JsonPropertyName("registros")] List<RegistroRequest>? Registros,
    [property: JsonPropertyName("observaciones_jefe")] JsonElement? ObservacionesJefe,
    [property: JsonPropertyName("observaciones_jefe2")] JsonElement? ObservacionesJefe2,
    [property: JsonPropertyName("observaciones_jefe3")] JsonElement? ObservacionesJefe3);

public sealed record HorometrosRequest(
    [property: JsonPropertyName("diesel")] HorometroDetalleRequest? Diesel,
    [property: JsonPropertyName("electrico")] HorometroDetalleRequest? Electrico,
    [property: JsonPropertyName("percusion")] HorometroDetalleRequest? Percusion);

public sealed record HorometroDetalleRequest(
    [property: JsonPropertyName("inicio")] decimal? Inicio,
    [property: JsonPropertyName("final")] decimal? Final,
    [property: JsonPropertyName("op")] bool Op,
    [property: JsonPropertyName("inop")] bool Inop);

public sealed record CondicionEquipoRequest(
    [property: JsonPropertyName("op")] bool Op,
    [property: JsonPropertyName("noOp")] bool NoOp,
    [property: JsonPropertyName("lugar")] string? Lugar,
    [property: JsonPropertyName("descripcion")] string? Descripcion,
    [property: JsonPropertyName("aceiteMotor")] bool AceiteMotor,
    [property: JsonPropertyName("aceiteHidraulico")] bool AceiteHidraulico,
    [property: JsonPropertyName("aceiteTransmision")] bool AceiteTransmision,
    [property: JsonPropertyName("combustible")] string? Combustible,
    [property: JsonPropertyName("horaLlenado")] string? HoraLlenado);

public sealed record ChecklistRespuestaRequest(
    [property: JsonPropertyName("descripcion")] string Descripcion,
    [property: JsonPropertyName("decision")] int Decision,
    [property: JsonPropertyName("observacion")] string? Observacion,
    [property: JsonPropertyName("categoria")] string Categoria);

public sealed record ControlLlantasRequest(
    [property: JsonPropertyName("numero1")] bool? Numero1,
    [property: JsonPropertyName("numero2")] bool? Numero2,
    [property: JsonPropertyName("numero3")] bool? Numero3,
    [property: JsonPropertyName("numero4")] bool? Numero4);

public sealed record RegistroRequest(
    [property: JsonPropertyName("id")] long? Id,
    [property: JsonPropertyName("numero")] int Numero,
    [property: JsonPropertyName("estado")] string Estado,
    [property: JsonPropertyName("codigo")] string Codigo,
    [property: JsonPropertyName("hora_inicio")] string HoraInicio,
    [property: JsonPropertyName("hora_final")] string HoraFinal,
    [property: JsonPropertyName("operacion")] RegistroOperacionDetalleRequest Operacion);

public sealed record RegistroOperacionDetalleRequest(
    [property: JsonPropertyName("nivel")] string? Nivel,
    [property: JsonPropertyName("tipo_labor")] string? TipoLabor,
    [property: JsonPropertyName("labor")] string? Labor,
    [property: JsonPropertyName("ala")] string? Ala,
    [property: JsonPropertyName("tal_prod")] string? TalProd,
    [property: JsonPropertyName("tal_rimados")] string? TalRimados,
    [property: JsonPropertyName("tal_alivio")] string? TalAlivio,
    [property: JsonPropertyName("tal_repaso")] string? TalRepaso,
    [property: JsonPropertyName("long_barras")] string? LongBarras,
    [property: JsonPropertyName("num_barras")] string? NumBarras,
    [property: JsonPropertyName("n_taladros_produccion")] string? NTaladrosProduccion,
    [property: JsonPropertyName("metros_perforados_produccion")] string? MetrosPerforadosProduccion,
    [property: JsonPropertyName("n_taladros_rimados")] string? NTaladrosRimados,
    [property: JsonPropertyName("metros_perforados_rimados")] string? MetrosPerforadosRimados,
    [property: JsonPropertyName("n_taladros_alivio")] string? NTaladrosAlivio,
    [property: JsonPropertyName("metros_perforados_alivio")] string? MetrosPerforadosAlivio,
    [property: JsonPropertyName("n_taladros_repaso")] string? NTaladrosRepaso,
    [property: JsonPropertyName("metros_perforados_repaso")] string? MetrosPerforadosRepaso,
    [property: JsonPropertyName("tipo_perforacion")] string? TipoPerforacion,
    [property: JsonPropertyName("tipo_perforacion_id")] int? TipoPerforacionId,
    [property: JsonPropertyName("observaciones")] string? Observaciones);
