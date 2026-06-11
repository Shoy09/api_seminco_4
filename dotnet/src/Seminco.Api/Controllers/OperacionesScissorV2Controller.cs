using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seminco.Application.OperacionesV2;

namespace Seminco.Api.Controllers;

[ApiController]
[Route("api/operaciones-v2/scissor")]
public sealed class OperacionesScissorV2Controller(IOperacionScissorV2Service service) : ControllerBase
{
    [HttpGet("aprobacion")]
    public async Task<ActionResult> ObtenerPorAprobacion([FromQuery] string? estado, [FromQuery] string? envio, CancellationToken ct)
    {
        var result = await service.GetByAprobacionAsync(estado, envio, ct);
        return Ok(new { ok = true, data = result });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<OperacionScissorV2ResponseDto>> Create([FromBody] JsonElement body, CancellationToken ct)
        => StatusCode(StatusCodes.Status201Created, await service.CreateAsync(body, ct));

    [HttpPut("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<OperacionScissorV2ResponseDto>> Update(int id, [FromBody] JsonElement body, CancellationToken ct)
    {
        var result = await service.UpdateAsync(id, body, ct);
        if (result is null) return NotFound(new { error = "Registro no encontrado" });
        return Ok(result);
    }
}
