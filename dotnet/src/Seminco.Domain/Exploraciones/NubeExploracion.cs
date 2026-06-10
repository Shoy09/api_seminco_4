namespace Seminco.Domain.Exploraciones;

public sealed class NubeExploracion
{
    public int Id { get; set; }
    public string Fecha { get; set; } = string.Empty;
    public string Turno { get; set; } = string.Empty;
    public string Taladro { get; set; } = string.Empty;
    public string PiesPorTaladro { get; set; } = string.Empty;
    public string Zona { get; set; } = string.Empty;
    public string TipoLabor { get; set; } = string.Empty;
    public string Labor { get; set; } = string.Empty;
    public string? Ala { get; set; }
    public string Veta { get; set; } = string.Empty;
    public string Nivel { get; set; } = string.Empty;
    public string TipoPerforacion { get; set; } = string.Empty;
    public string Estado { get; set; } = "Creado";
    public int Cerrado { get; set; }
    public int Envio { get; set; }
    public string? SemanaDefault { get; set; }
    public string? SemanaSelect { get; set; }
    public string? Empresa { get; set; }
    public string? Seccion { get; set; }
    public int Medicion { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<NubeDespacho> Despachos { get; set; } = [];
    public List<NubeDevolucion> Devoluciones { get; set; } = [];
}

public sealed class NubeDespacho
{
    public int Id { get; set; }
    public int DatosTrabajoId { get; set; }
    public double MiliSegundo { get; set; }
    public double MedioSegundo { get; set; }
    public string? Observaciones { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public NubeExploracion? DatosTrabajo { get; set; }
    public List<NubeDespachoDetalle> Detalles { get; set; } = [];
    public List<NubeDetalleDespachoExplosivo> DetallesExplosivos { get; set; } = [];
}

public sealed class NubeDespachoDetalle
{
    public int Id { get; set; }
    public int DespachoId { get; set; }
    public string NombreMaterial { get; set; } = string.Empty;
    public string Cantidad { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public NubeDespacho? Despacho { get; set; }
}

public sealed class NubeDetalleDespachoExplosivo
{
    public int Id { get; set; }
    public int IdDespacho { get; set; }
    public double Longitud { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Retardos { get; set; } = "[]";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public NubeDespacho? Despacho { get; set; }
}

public sealed class NubeDevolucion
{
    public int Id { get; set; }
    public int DatosTrabajoId { get; set; }
    public double MiliSegundo { get; set; }
    public double MedioSegundo { get; set; }
    public string? Observaciones { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public NubeExploracion? DatosTrabajo { get; set; }
    public List<NubeDevolucionDetalle> Detalles { get; set; } = [];
    public List<NubeDetalleDevolucionExplosivo> DetallesExplosivos { get; set; } = [];
}

public sealed class NubeDevolucionDetalle
{
    public int Id { get; set; }
    public int DevolucionId { get; set; }
    public string NombreMaterial { get; set; } = string.Empty;
    public string Cantidad { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public NubeDevolucion? Devolucion { get; set; }
}

public sealed class NubeDetalleDevolucionExplosivo
{
    public int Id { get; set; }
    public int IdDevolucion { get; set; }
    public double Longitud { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Retardos { get; set; } = "[]";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public NubeDevolucion? Devolucion { get; set; }
}
