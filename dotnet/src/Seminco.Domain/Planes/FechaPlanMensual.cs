namespace Seminco.Domain.Planes;

public sealed class FechaPlanMensual
{
    public int Id { get; set; }
    public string Mes { get; set; } = string.Empty;
    public DateTime FechaIngreso { get; set; }
}
