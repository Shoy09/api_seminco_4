namespace Seminco.Domain.Planes;

public sealed class PlanProduccion : PlanBase
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
    public double? AgGr { get; set; }
    public double? PorcentajeCu { get; set; }
    public double? PorcentajePb { get; set; }
    public double? PorcentajeZn { get; set; }
    public double? VptAct { get; set; }
    public double? VptFinal { get; set; }
    public double? CutOff1 { get; set; }
    public double? CutOff2 { get; set; }
}
