using Seminco.Domain.Catalogs;

namespace Seminco.Domain.OperacionesV2;

public sealed class OperacionScissor
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
    public OperacionScissorCondicionEquipo? CondicionEquipo { get; set; }
    public List<OperacionScissorHorometro> Horometros { get; set; } = [];
    public List<OperacionScissorChecklistRespuesta> ChecklistRespuestas { get; set; } = [];
    public List<OperacionScissorControlLlanta> ControlLlantas { get; set; } = [];
    public List<OperacionScissorRegistro> Registros { get; set; } = [];
}
