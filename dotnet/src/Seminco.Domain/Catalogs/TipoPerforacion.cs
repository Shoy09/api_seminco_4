namespace Seminco.Domain.Catalogs;

public sealed class TipoPerforacion
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Proceso { get; set; }
    public int? PermitidoMedicion { get; set; }
}
