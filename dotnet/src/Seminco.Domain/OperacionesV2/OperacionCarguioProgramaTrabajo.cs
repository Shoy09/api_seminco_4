namespace Seminco.Domain.OperacionesV2;

public sealed class OperacionCarguioProgramaTrabajo
{
    public int OperacionId { get; set; }
    public int? NCucharasProgramado { get; set; }
    public int? NCucharasRealizado { get; set; }

    public OperacionCarguio Operacion { get; set; } = null!;
}
