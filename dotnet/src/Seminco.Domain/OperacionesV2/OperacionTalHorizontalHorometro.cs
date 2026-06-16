namespace Seminco.Domain.OperacionesV2;

public sealed class OperacionTalHorizontalHorometro
{
    public int Id { get; set; }
    public int OperacionId { get; set; }

    public string Tipo { get; set; } = string.Empty; // diesel, electrico, percusion
    public decimal? Inicio { get; set; }
    public decimal? Final { get; set; }
    public bool Op { get; set; }
    public bool Inop { get; set; }

    public OperacionTalHorizontal Operacion { get; set; } = null!;
}