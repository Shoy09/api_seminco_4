namespace Seminco.Domain.Catalogs;

public sealed class Equipo
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Proceso { get; set; } = string.Empty;
    public string? Codigo { get; set; }
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public string? Serie { get; set; }
    public string? AnioFabricacion { get; set; }
    public DateTime? FechaIngreso { get; set; }
    public double? CapacidadYd3 { get; set; }
    public double? CapacidadM3 { get; set; }
}
