namespace Seminco.Domain.OperacionesV2;

public sealed class OperacionCarguioRegistroDetalle
{
    public int RegistroId { get; set; }

    public string? NivelInicio { get; set; }
    public string? TipoLaborInicio { get; set; }
    public string? LaborInicio { get; set; }
    public string? AlaInicio { get; set; }
    public string? UbicacionDestino { get; set; }
    public int? NCucharas { get; set; }
    public string? Observaciones { get; set; }

    public OperacionCarguioRegistro Registro { get; set; } = null!;
}
