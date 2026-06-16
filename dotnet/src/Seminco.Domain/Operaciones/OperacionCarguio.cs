namespace Seminco.Domain.Operaciones;

public sealed class OperacionCarguio : OperacionBase
{
    public string? Seccion { get; set; }
    public string? Capacidad { get; set; }
    public string? TipoEquipo { get; set; }
    public string? ProgramaTrabajo { get; set; }
}
