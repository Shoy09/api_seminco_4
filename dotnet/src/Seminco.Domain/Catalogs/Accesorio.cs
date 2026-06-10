namespace Seminco.Domain.Catalogs;

public sealed class Accesorio
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string? TipoAccesorio { get; set; }
    public decimal Costo { get; set; }
    public string? UnidadMedida { get; set; }
}
