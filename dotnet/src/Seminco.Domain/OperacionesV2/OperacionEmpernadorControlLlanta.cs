namespace Seminco.Domain.OperacionesV2;

public sealed class OperacionEmpernadorControlLlanta
{
    public int Id { get; set; }
    public int OperacionId { get; set; }
    public short Posicion { get; set; }
    public bool Estado { get; set; }
    public decimal? Presion { get; set; }
    public string? Observacion { get; set; }
    public OperacionEmpernador Operacion { get; set; } = null!;
}
