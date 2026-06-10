namespace Seminco.Domain.Catalogs;

public sealed class Explosivo
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string? TipoExplosivo { get; set; }
    public int? CantidadPorCaja { get; set; }
    public double? PesoUnitario { get; set; }
    public decimal? CostoPorKg { get; set; }
    public string? UnidadMedida { get; set; }
}
