namespace Seminco.Domain.Planes;

public sealed class PlanMensual : PlanBase
{
    public string? MinadoTipo { get; set; }
    public string? Empresa { get; set; }
    public string? Zona { get; set; }
    public string? Area { get; set; }
    public string? TipoMineral { get; set; }
    public string? Fase { get; set; }
    public string? EstructuraVeta { get; set; }
    public string? Nivel { get; set; }
    public string? TipoLabor { get; set; }
    public string? Labor { get; set; }
    public string? Ala { get; set; }
    public double? AvanceM { get; set; }
    public double? AnchoM { get; set; }
    public double? AltoM { get; set; }
    public double? Tms { get; set; }
}
