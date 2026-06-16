namespace Seminco.Domain.OperacionesV2;

public sealed class OperacionEmpernadorRegistroDetalle
{
    public int RegistroId { get; set; }
    public string? Nivel { get; set; }
    public string? TipoLabor { get; set; }
    public string? Labor { get; set; }
    public string? Ala { get; set; }
    public string? TipoPernos { get; set; }
    public decimal? LogPernos { get; set; }
    public decimal? NPernosInstalados { get; set; }
    public string? TipoMalla { get; set; }
    public decimal? Mt52Malla { get; set; }
    public string? SistematicoPuntual { get; set; }
    public string? Observaciones { get; set; }
    public OperacionEmpernadorRegistro Registro { get; set; } = null!;
}
