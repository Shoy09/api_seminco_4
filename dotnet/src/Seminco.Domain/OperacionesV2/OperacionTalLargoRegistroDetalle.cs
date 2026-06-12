namespace Seminco.Domain.OperacionesV2;

public sealed class OperacionTalLargoRegistroDetalle
{
    public int RegistroId { get; set; }
    public string? Nivel { get; set; }
    public string? TipoLabor { get; set; }
    public string? Labor { get; set; }
    public string? Ala { get; set; }
    public string? NTaladrosProduccion { get; set; }
    public decimal? MetrosPerforadosProduccion { get; set; }
    public string? NTaladrosRimados { get; set; }
    public decimal? MetrosPerforadosRimados { get; set; }
    public string? NTaladrosAlivio { get; set; }
    public decimal? MetrosPerforadosAlivio { get; set; }
    public string? NTaladrosRepaso { get; set; }
    public decimal? MetrosPerforadosRepaso { get; set; }
    public string? LongBarras { get; set; }
    public string? NumBarras { get; set; }
    public string? TipoPerforacion { get; set; }
    public int? TipoPerforacionId { get; set; }
    public string? Observaciones { get; set; }
    public OperacionTalLargoRegistro Registro { get; set; } = null!;
}
