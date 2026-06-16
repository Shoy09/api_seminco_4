namespace Seminco.Domain.Operaciones;

public sealed class OperacionDumper : OperacionBase
{
    public string? Seccion { get; set; }
    public string? Capacidad { get; set; }
    public string? TipoEquipo { get; set; }
    public string? ProgramaTrabajo { get; set; }
    public string? CheckListTelemando { get; set; }
}
