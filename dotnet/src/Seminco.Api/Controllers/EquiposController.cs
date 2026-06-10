using Microsoft.AspNetCore.Mvc;
using Seminco.Application.Catalogs;

namespace Seminco.Api.Controllers;

[Route("api/Equipo")]
public sealed class EquiposController(ICatalogService<EquipoDto> service) : CatalogController<EquipoDto>(service)
{
    [HttpGet("proceso/{proceso}")]
    public async Task<ActionResult<List<EquipoDto>>> GetByProceso(string proceso, CancellationToken ct)
    {
        var all = await service.GetAllAsync(ct);
        return Ok(all.Where(e => e.Proceso == proceso).ToList());
    }
}
