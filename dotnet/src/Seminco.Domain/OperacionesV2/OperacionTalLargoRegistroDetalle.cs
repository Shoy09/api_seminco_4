namespace Seminco.Domain.OperacionesV2;

public sealed class OperacionTalLargoRegistroDetalle
{
    public int RegistroId { get; set; }
    public string? Nivel { get; set; }
    public string? TipoLabor { get; set; }
    public string? Labor { get; set; }
    public string? Ala { get; set; }
    public decimal? TalProd { get; set; }
    public decimal? TalRimados { get; set; }
    public decimal? TalAlivio { get; set; }
    public decimal? TalRepaso { get; set; }
    public decimal? LongBarras { get; set; }
    public decimal? NumBarras { get; set; }
    public string? TipoPerforacion { get; set; }
    public int? TipoPerforacionId { get; set; }
    public string? Observaciones { get; set; }
    public OperacionTalLargoRegistro Registro { get; set; } = null!;
}
