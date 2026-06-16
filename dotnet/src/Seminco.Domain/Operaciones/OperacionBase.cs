namespace Seminco.Domain.Operaciones;

public abstract class OperacionBase
{
    public int Id { get; set; }
    public string? Fecha { get; set; }
    public string? Turno { get; set; }
    public string? Operador { get; set; }
    public string? JefeGuardia { get; set; }
    public string? Equipo { get; set; }
    public string? NEquipo { get; set; }
    public string? Registros { get; set; }
    public string? Horometros { get; set; }
    public string? CondicionesEquipo { get; set; }
    public string? CheckList { get; set; }
    public string? ControlLlantas { get; set; }
    public string Estado { get; set; } = "activo";
    public int Envio { get; set; }
    public int Revisado { get; set; }
    public int Aprobacion { get; set; }
    public string? ObservacionesJefe { get; set; }
    public string? ObservacionesJefe2 { get; set; }
    public string? ObservacionesJefe3 { get; set; }
}
