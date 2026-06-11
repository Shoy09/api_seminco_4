namespace Seminco.Domain.OperacionesV2;

public sealed class OperacionTalHorizontal
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string? Turno { get; set; }
    public string? Operador { get; set; }
    public string? JefeGuardia { get; set; }

    public int? EquipoId { get; set; }
    public string? EquipoNombre { get; set; }
    public string? NEquipo { get; set; }

    public string? Seccion { get; set; }
    public string? ModeloEquipo { get; set; }

    public string Estado { get; set; } = "activo";
    public int Envio { get; set; }
    public int Revisado { get; set; }
    public int Aprobacion { get; set; }

    public string? ObservacionesJefe { get; set; }
    public string? ObservacionesJefe2 { get; set; }
    public string? ObservacionesJefe3 { get; set; }

    public string? PayloadOriginal { get; set; }
    public string? PayloadVersion { get; set; }
    public string? ExternalSyncId { get; set; }
    public string? DeviceId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public OperacionTalHorizontalCondicionEquipo? CondicionEquipo { get; set; }
    public List<OperacionTalHorizontalHorometro> Horometros { get; set; } = [];
    public List<OperacionTalHorizontalChecklistRespuesta> ChecklistRespuestas { get; set; } = [];
    public List<OperacionTalHorizontalControlLlanta> ControlLlantas { get; set; } = [];
    public List<OperacionTalHorizontalRegistro> Registros { get; set; } = [];
}