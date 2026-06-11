namespace Seminco.Domain.OperacionesV2;

public sealed class OperacionTalHorizontalRegistro
{
    public int Id { get; set; }
    public int OperacionId { get; set; }

    public long? ExternalId { get; set; }
    public int Numero { get; set; }

    public string EstadoPrincipal { get; set; } = string.Empty;
    public string CodigoEstado { get; set; } = string.Empty;
    public int? EstadoCatalogoId { get; set; }

    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFinal { get; set; }

    public string? PayloadOperacion { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public OperacionTalHorizontal Operacion { get; set; } = null!;
    public OperacionTalHorizontalRegistroDetalle? Detalle { get; set; }
}