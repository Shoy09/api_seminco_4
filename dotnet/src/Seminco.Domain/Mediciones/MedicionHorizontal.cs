namespace Seminco.Domain.Mediciones;

public sealed class MedicionHorizontal
{
    public int Id { get; set; }
    public string Fecha { get; set; } = string.Empty;
    public string? Turno { get; set; }
    public string? Empresa { get; set; }
    public string? Zona { get; set; }
    public string? Labor { get; set; }
    public string? Veta { get; set; }
    public string? TipoPerforacion { get; set; }
    public double? KgExplosivos { get; set; }
    public double? AvanceProgramado { get; set; }
    public double? Ancho { get; set; }
    public double? Alto { get; set; }
    public int Envio { get; set; }
    public int? IdExplosivo { get; set; }
    public int? IdNube { get; set; }
    public int NoAplica { get; set; }
    public int Remanente { get; set; }
}
