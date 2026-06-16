using Seminco.Domain.Catalogs;

namespace Seminco.Domain.OperacionesV2;

public sealed class OperacionCarguioChecklistRespuesta
{
    public int Id { get; set; }
    public int OperacionId { get; set; }
    public int? ChecklistItemId { get; set; }

    public string CategoriaSnapshot { get; set; } = string.Empty;
    public string DescripcionSnapshot { get; set; } = string.Empty;

    public int Decision { get; set; }
    public string? Observacion { get; set; }

    public OperacionCarguio Operacion { get; set; } = null!;
    public CheckListItem? ChecklistItem { get; set; }
}
