using System.Text.Json;
using System.Text.Json.Serialization;

namespace Seminco.Application.Exploraciones;

public sealed record NubeExploracionDto(
    int Id,
    [property: JsonPropertyName("fecha")] string Fecha,
    [property: JsonPropertyName("turno")] string Turno,
    [property: JsonPropertyName("taladro")] string Taladro,
    [property: JsonPropertyName("pies_por_taladro")] string PiesPorTaladro,
    [property: JsonPropertyName("zona")] string Zona,
    [property: JsonPropertyName("tipo_labor")] string TipoLabor,
    [property: JsonPropertyName("labor")] string Labor,
    [property: JsonPropertyName("ala")] string? Ala,
    [property: JsonPropertyName("veta")] string Veta,
    [property: JsonPropertyName("nivel")] string Nivel,
    [property: JsonPropertyName("tipo_perforacion")] string TipoPerforacion,
    [property: JsonPropertyName("estado")] string Estado,
    [property: JsonPropertyName("cerrado")] int Cerrado,
    [property: JsonPropertyName("envio")] int Envio,
    [property: JsonPropertyName("semanaDefault")] string? SemanaDefault,
    [property: JsonPropertyName("semanaSelect")] string? SemanaSelect,
    [property: JsonPropertyName("empresa")] string? Empresa,
    [property: JsonPropertyName("seccion")] string? Seccion,
    [property: JsonPropertyName("medicion")] int Medicion,
    [property: JsonPropertyName("despachos")] List<NubeDespachoDto> Despachos,
    [property: JsonPropertyName("devoluciones")] List<NubeDevolucionDto> Devoluciones,
    [property: JsonPropertyName("createdAt")] DateTime CreatedAt,
    [property: JsonPropertyName("updatedAt")] DateTime UpdatedAt);

public sealed record NubeDespachoDto(
    int Id,
    [property: JsonPropertyName("datos_trabajo_id")] int DatosTrabajoId,
    [property: JsonPropertyName("mili_segundo")] double MiliSegundo,
    [property: JsonPropertyName("medio_segundo")] double MedioSegundo,
    [property: JsonPropertyName("observaciones")] string? Observaciones,
    [property: JsonPropertyName("detalles")] List<NubeMaterialDto> Detalles,
    [property: JsonPropertyName("detalles_explosivos")] List<NubeExplosivoDto> DetallesExplosivos,
    [property: JsonPropertyName("createdAt")] DateTime CreatedAt,
    [property: JsonPropertyName("updatedAt")] DateTime UpdatedAt);

public sealed record NubeDevolucionDto(
    int Id,
    [property: JsonPropertyName("datos_trabajo_id")] int DatosTrabajoId,
    [property: JsonPropertyName("mili_segundo")] double MiliSegundo,
    [property: JsonPropertyName("medio_segundo")] double MedioSegundo,
    [property: JsonPropertyName("observaciones")] string? Observaciones,
    [property: JsonPropertyName("detalles")] List<NubeMaterialDto> Detalles,
    [property: JsonPropertyName("detalles_explosivos")] List<NubeExplosivoDto> DetallesExplosivos,
    [property: JsonPropertyName("createdAt")] DateTime CreatedAt,
    [property: JsonPropertyName("updatedAt")] DateTime UpdatedAt);

public sealed record NubeMaterialDto(
    int Id,
    [property: JsonPropertyName("nombre_material")] string NombreMaterial,
    [property: JsonPropertyName("cantidad")] string Cantidad,
    [property: JsonPropertyName("createdAt")] DateTime CreatedAt,
    [property: JsonPropertyName("updatedAt")] DateTime UpdatedAt);

public sealed record NubeExplosivoDto(
    int Id,
    [property: JsonPropertyName("longitud")] double Longitud,
    [property: JsonPropertyName("tipo")] string Tipo,
    [property: JsonPropertyName("retardos")] JsonElement Retardos,
    [property: JsonPropertyName("createdAt")] DateTime CreatedAt,
    [property: JsonPropertyName("updatedAt")] DateTime UpdatedAt);

public sealed record NubeExploracionCreateRequest(
    [property: JsonPropertyName("fecha")] string? Fecha,
    [property: JsonPropertyName("turno")] string? Turno,
    [property: JsonPropertyName("taladro")] string? Taladro,
    [property: JsonPropertyName("pies_por_taladro")] string? PiesPorTaladro,
    [property: JsonPropertyName("zona")] string? Zona,
    [property: JsonPropertyName("tipo_labor")] string? TipoLabor,
    [property: JsonPropertyName("labor")] string? Labor,
    [property: JsonPropertyName("ala")] string? Ala,
    [property: JsonPropertyName("veta")] string? Veta,
    [property: JsonPropertyName("nivel")] string? Nivel,
    [property: JsonPropertyName("tipo_perforacion")] string? TipoPerforacion,
    [property: JsonPropertyName("estado")] string? Estado,
    [property: JsonPropertyName("cerrado")] int? Cerrado,
    [property: JsonPropertyName("envio")] int? Envio,
    [property: JsonPropertyName("semanaDefault")] string? SemanaDefault,
    [property: JsonPropertyName("semanaSelect")] string? SemanaSelect,
    [property: JsonPropertyName("empresa")] string? Empresa,
    [property: JsonPropertyName("seccion")] string? Seccion,
    [property: JsonPropertyName("medicion")] int? Medicion,
    [property: JsonPropertyName("despachos")] List<NubeDespachoCreateRequest>? Despachos,
    [property: JsonPropertyName("devoluciones")] List<NubeDevolucionCreateRequest>? Devoluciones);

public sealed record NubeDespachoCreateRequest(
    [property: JsonPropertyName("mili_segundo")] double MiliSegundo,
    [property: JsonPropertyName("medio_segundo")] double MedioSegundo,
    [property: JsonPropertyName("observaciones")] string? Observaciones,
    [property: JsonPropertyName("detalles_materiales")] List<NubeMaterialCreateRequest>? DetallesMateriales,
    [property: JsonPropertyName("detalles_explosivos")] List<NubeExplosivoCreateRequest>? DetallesExplosivos);

public sealed record NubeDevolucionCreateRequest(
    [property: JsonPropertyName("mili_segundo")] double MiliSegundo,
    [property: JsonPropertyName("medio_segundo")] double MedioSegundo,
    [property: JsonPropertyName("observaciones")] string? Observaciones,
    [property: JsonPropertyName("detalles_materiales")] List<NubeMaterialCreateRequest>? DetallesMateriales,
    [property: JsonPropertyName("detalles_explosivos")] List<NubeExplosivoCreateRequest>? DetallesExplosivos);

public sealed record NubeMaterialCreateRequest(
    [property: JsonPropertyName("nombre_material")] string NombreMaterial,
    [property: JsonPropertyName("cantidad")] string Cantidad);

public sealed record NubeExplosivoCreateRequest(
    [property: JsonPropertyName("longitud")] double Longitud,
    [property: JsonPropertyName("tipo")] string Tipo,
    [property: JsonPropertyName("retardos")] JsonElement Retardos);

public sealed record NubeExploracionCreateResponseDto(
    [property: JsonPropertyName("message")] string Message,
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("envio")] int Envio,
    [property: JsonPropertyName("estado")] string Estado);

public sealed record NubeExploracionMedicionDataDto(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("medicion")] int Medicion,
    [property: JsonPropertyName("estado")] string Estado);

public sealed record NubeExploracionMedicionResponseDto(
    [property: JsonPropertyName("message")] string Message,
    [property: JsonPropertyName("data")] NubeExploracionMedicionDataDto Data);

public sealed record NubeExploracionBulkMedicionResponseDto(
    [property: JsonPropertyName("message")] string Message,
    [property: JsonPropertyName("cantidad_actualizada")] int CantidadActualizada);
