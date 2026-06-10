namespace Seminco.Domain.Planes;

public sealed class PlanMetraje : PlanBase
{
    public string? Semana { get; set; }
    public string? Mina { get; set; }
    public string? Zona { get; set; }
    public string? Area { get; set; }
    public string? Fase { get; set; }
    public string? MinadoTipo { get; set; }
    public string? TipoLabor { get; set; }
    public string? TipoMineral { get; set; }
    public string? EstructuraVeta { get; set; }
    public string? Nivel { get; set; }
    public string? Block { get; set; }
    public string? Labor { get; set; }
    public string? Ala { get; set; }
    public double? AnchoVeta { get; set; }
    public double? AnchoMinadoSem { get; set; }
    public double? AnchoMinadoMes { get; set; }
    public double? Burden { get; set; }
    public double? Espaciamiento { get; set; }
    public double? LongitudPerforacion { get; set; }
}
