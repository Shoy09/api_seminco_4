using Microsoft.AspNetCore.Mvc;
using Seminco.Application.Planes;

namespace Seminco.Api.Controllers;

[Route("api/fechas-plan-mensual")]
[ApiController]
public sealed class FechasPlanMensualController(IFechaPlanMensualService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<FechaPlanMensualDto>>> GetAll(CancellationToken ct) =>
        Ok(await service.GetAllAsync(ct));

    [HttpPost]
    public async Task<ActionResult<FechaPlanMensualDto>> Create([FromBody] CreateFechaRequest request, CancellationToken ct)
    {
        var result = await service.CreateAsync(request.Mes, ct);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    [HttpGet("ultima")]
    public async Task<ActionResult<UltimaFechaDto>> GetUltima(CancellationToken ct)
    {
        var result = await service.GetUltimaAsync(ct);
        if (result is null) return NotFound(new { error = "No se encontró ninguna fecha registrada" });
        return Ok(result);
    }
}

public sealed record CreateFechaRequest(string Mes);
