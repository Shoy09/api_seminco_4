namespace Seminco.Domain.OperacionesV2;

public sealed class ChecklistItemCatalog
{
    public int Id { get; set; }
    public string Proceso { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int Orden { get; set; }
    public bool Activo { get; set; } = true;
    public int Version { get; set; } = 1;

    public List<OperacionTalHorizontalChecklistRespuesta> Respuestas { get; set; } = [];
}