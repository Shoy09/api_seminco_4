using System.Text.Json.Serialization;

namespace Seminco.Application.Planes;

public sealed record FechaPlanMensualDto(
    int Id,
    [property: JsonPropertyName("mes")] string Mes,
    [property: JsonPropertyName("fecha_ingreso")] DateTime FechaIngreso
);

public sealed record UltimaFechaDto(
    int Id,
    [property: JsonPropertyName("mes")] string Mes,
    [property: JsonPropertyName("fecha_ingreso")] int FechaIngreso
);
