using System.Text.Json.Serialization;

namespace Seminco.Application.Catalogs;

public sealed record EquipoDto(
    int Id,
    [property: JsonPropertyName("nombre")] string Nombre,
    [property: JsonPropertyName("proceso")] string Proceso,
    [property: JsonPropertyName("codigo")] string? Codigo,
    [property: JsonPropertyName("marca")] string? Marca,
    [property: JsonPropertyName("modelo")] string? Modelo,
    [property: JsonPropertyName("serie")] string? Serie,
    [property: JsonPropertyName("anioFabricacion")] string? AnioFabricacion,
    [property: JsonPropertyName("fechaIngreso")] DateTime? FechaIngreso,
    [property: JsonPropertyName("capacidadYd3")] double? CapacidadYd3,
    [property: JsonPropertyName("capacidadM3")] double? CapacidadM3);

public sealed record EstadoDto(
    int Id,
    [property: JsonPropertyName("estado_principal")] string EstadoPrincipal,
    [property: JsonPropertyName("codigo")] string? Codigo,
    [property: JsonPropertyName("tipo_estado")] string? TipoEstado,
    [property: JsonPropertyName("categoria")] string? Categoria,
    [property: JsonPropertyName("proceso")] string? Proceso);

public sealed record TipoPerforacionDto(
    int Id,
    [property: JsonPropertyName("nombre")] string Nombre,
    [property: JsonPropertyName("proceso")] string? Proceso,
    [property: JsonPropertyName("permitido_medicion")] int? PermitidoMedicion);

public sealed record TipoEquipoDto(
    int Id,
    [property: JsonPropertyName("nombre")] string Nombre);

public sealed record CheckListItemDto(
    int Id,
    [property: JsonPropertyName("proceso")] string? Proceso,
    [property: JsonPropertyName("categoria")] string? Categoria,
    [property: JsonPropertyName("nombre")] string? Nombre);

public sealed record ChecklistTelemandoDto(
    int Id,
    [property: JsonPropertyName("nombre")] string Nombre);

public sealed record SeccionDto(
    int Id,
    [property: JsonPropertyName("proceso")] string? Proceso,
    [property: JsonPropertyName("nombre")] string? Nombre);

public sealed record LongitudBarraDto(
    int Id,
    [property: JsonPropertyName("proceso")] string? Proceso,
    [property: JsonPropertyName("longitud_pies")] double LongitudPies);

public sealed record PernoDto(
    int Id,
    [property: JsonPropertyName("tipo_perno")] string? TipoPerno,
    [property: JsonPropertyName("longitud")] double Longitud);

public sealed record MallaDto(
    int Id,
    [property: JsonPropertyName("tipo_malla")] string? TipoMalla);

public sealed record OrigenDestinoDto(
    int Id,
    [property: JsonPropertyName("proceso")] string? Proceso,
    [property: JsonPropertyName("tipo")] string? Tipo,
    [property: JsonPropertyName("nombre")] string? Nombre);

public sealed record AccesorioDto(
    int Id,
    [property: JsonPropertyName("codigo")] string Codigo,
    [property: JsonPropertyName("tipo_accesorio")] string? TipoAccesorio,
    [property: JsonPropertyName("costo")] decimal Costo,
    [property: JsonPropertyName("unidad_medida")] string? UnidadMedida);

public sealed record ExplosivoDto(
    int Id,
    [property: JsonPropertyName("codigo")] string Codigo,
    [property: JsonPropertyName("tipo_explosivo")] string? TipoExplosivo,
    [property: JsonPropertyName("cantidad_por_caja")] int? CantidadPorCaja,
    [property: JsonPropertyName("peso_unitario")] double? PesoUnitario,
    [property: JsonPropertyName("costo_por_kg")] decimal? CostoPorKg,
    [property: JsonPropertyName("unidad_medida")] string? UnidadMedida);

public sealed record ExplosivoUniDto(
    int Id,
    [property: JsonPropertyName("dato")] double Dato,
    [property: JsonPropertyName("tipo")] string? Tipo);

public sealed record NumeroRetardoDto(
    int Id,
    [property: JsonPropertyName("longitud")] decimal Longitud,
    [property: JsonPropertyName("tipo")] string Tipo,
    [property: JsonPropertyName("codigo")] string Codigo);
