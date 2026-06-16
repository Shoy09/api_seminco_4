namespace Seminco.Domain.OperacionesV2;

public sealed class OperacionTalLargoCondicionEquipo
{
    public int OperacionId { get; set; }
    public bool Op { get; set; }
    public bool NoOp { get; set; }
    public string? Lugar { get; set; }
    public string? Descripcion { get; set; }
    public bool AceiteMotor { get; set; }
    public bool AceiteHidraulico { get; set; }
    public bool AceiteTransmision { get; set; }
    public string? Combustible { get; set; }
    public TimeOnly? HoraLlenado { get; set; }
    public OperacionTalLargo Operacion { get; set; } = null!;
}
