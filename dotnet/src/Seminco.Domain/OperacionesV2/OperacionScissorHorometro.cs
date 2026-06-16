namespace Seminco.Domain.OperacionesV2;

public sealed class OperacionScissorHorometro
{
    public int Id { get; set; }
    public int OperacionId { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public decimal? Inicio { get; set; }
    public decimal? Final { get; set; }
    public bool Op { get; set; }
    public bool Inop { get; set; }
    public OperacionScissor Operacion { get; set; } = null!;
}
