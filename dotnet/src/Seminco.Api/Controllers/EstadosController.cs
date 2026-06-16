using Microsoft.AspNetCore.Mvc;
using Seminco.Application.Catalogs;

namespace Seminco.Api.Controllers;

[Route("api/estado")]
public sealed class EstadosController(ICatalogService<EstadoDto> service) : CatalogController<EstadoDto>(service)
{
    [HttpGet("proceso/{proceso}")]
    public async Task<ActionResult<List<EstadoDto>>> GetByProceso(string proceso, CancellationToken ct)
    {
        var all = await service.GetAllAsync(ct);
        return Ok(all.Where(e => e.Proceso == proceso).ToList());
    }
}
