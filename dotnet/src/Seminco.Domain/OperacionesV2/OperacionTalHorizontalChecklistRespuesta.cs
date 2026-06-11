namespace Seminco.Domain.OperacionesV2;

public sealed class OperacionTalHorizontalChecklistRespuesta
{
    public int Id { get; set; }
    public int OperacionId { get; set; }
    public int? ChecklistItemId { get; set; }

    public string CategoriaSnapshot { get; set; } = string.Empty;
    public string DescripcionSnapshot { get; set; } = string.Empty;

    public int Decision { get; set; }
    public string? Observacion { get; set; }

    public OperacionTalHorizontal Operacion { get; set; } = null!;
    public ChecklistItemCatalog? ChecklistItem { get; set; }
}