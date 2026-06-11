using Seminco.Domain.Catalogs;

namespace Seminco.Domain.OperacionesV2;

public sealed class OperacionCarguio
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
    public int? SeccionId { get; set; }
    public Seccion? SeccionNav { get; set; }
    public string? Capacidad { get; set; }

    public bool? TipoEquipoDiesel { get; set; }
    public bool? TipoEquipoElectrico { get; set; }

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

    public OperacionCarguioCondicionEquipo? CondicionEquipo { get; set; }
    public OperacionCarguioProgramaTrabajo? ProgramaTrabajo { get; set; }
    public List<OperacionCarguioHorometro> Horometros { get; set; } = [];
    public List<OperacionCarguioChecklistRespuesta> ChecklistRespuestas { get; set; } = [];
    public List<OperacionCarguioControlLlanta> ControlLlantas { get; set; } = [];
    public List<OperacionCarguioRegistro> Registros { get; set; } = [];
}
