using Microsoft.AspNetCore.Mvc;
using Seminco.Application.Catalogs;

namespace Seminco.Api.Controllers;

[Route("api/secciones")]
public sealed class SeccionesController(ICatalogService<SeccionDto> service) : CatalogController<SeccionDto>(service)
{
    [HttpGet("proceso/{proceso}")]
    public async Task<ActionResult<List<SeccionDto>>> GetByProceso(string proceso, CancellationToken ct)
    {
        var all = await service.GetAllAsync(ct);
        return Ok(all.Where(e => e.Proceso == proceso).ToList());
    }
}
