namespace Seminco.Domain.Catalogs;

public sealed class Estado
{
    public int Id { get; set; }
    public string EstadoPrincipal { get; set; } = string.Empty;
    public string? Codigo { get; set; }
    public string? TipoEstado { get; set; }
    public string? Categoria { get; set; }
    public string? Proceso { get; set; }
}
