namespace Seminco.Domain.OperacionesV2;

public sealed class OperacionScissorRegistroDetalle
{
    public int RegistroId { get; set; }
    public string? OrigenNivel { get; set; }
    public string? OrigenTipoLabor { get; set; }
    public string? OrigenLabor { get; set; }
    public string? OrigenAla { get; set; }
    public string? DestinoNivel { get; set; }
    public string? DestinoTipoLabor { get; set; }
    public string? DestinoLabor { get; set; }
    public string? DestinoAla { get; set; }
    public string? Observaciones { get; set; }
    public OperacionScissorRegistro Registro { get; set; } = null!;
}
