namespace Seminco.Domain.Catalogs;

public sealed class NumeroRetardo
{
    public int Id { get; set; }
    public decimal Longitud { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
}
