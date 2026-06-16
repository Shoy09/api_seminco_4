namespace Seminco.Domain.OperacionesV2;

public sealed class OperacionTalLargoControlLlanta
{
    public int Id { get; set; }
    public int OperacionId { get; set; }
    public short Posicion { get; set; }
    public bool Estado { get; set; }
    public decimal? Presion { get; set; }
    public string? Observacion { get; set; }
    public OperacionTalLargo Operacion { get; set; } = null!;
}
