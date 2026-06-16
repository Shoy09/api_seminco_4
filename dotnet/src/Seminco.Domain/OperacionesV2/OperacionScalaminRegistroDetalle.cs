namespace Seminco.Domain.OperacionesV2;

public sealed class OperacionScalaminRegistroDetalle
{
    public int RegistroId { get; set; }
    public string? Nivel { get; set; }
    public string? TipoLabor { get; set; }
    public string? Labor { get; set; }
    public string? Ala { get; set; }
    public string? Observaciones { get; set; }
    public OperacionScalaminRegistro Registro { get; set; } = null!;
}
