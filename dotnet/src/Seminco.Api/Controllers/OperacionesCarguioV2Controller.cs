using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seminco.Application.OperacionesV2;

namespace Seminco.Api.Controllers;

[ApiController]
[Route("api/operaciones-v2/carguio")]
public sealed class OperacionesCarguioV2Controller(IOperacionCarguioV2Service service) : ControllerBase
{
    [HttpGet("aprobacion")]
    public async Task<ActionResult> ObtenerPorAprobacion([FromQuery] string? estado, [FromQuery] string? envio, CancellationToken ct)
    {
        try
        {
            var result = await service.GetByAprobacionAsync(estado, envio, ct);
            return Ok(new { ok = true, data = result });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { ok = false, error = ex.Message });
        }
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<OperacionCarguioV2ResponseDto>> Create([FromBody] JsonElement body, CancellationToken ct)
    {
        try
        {
            return StatusCode(StatusCodes.Status201Created, await service.CreateAsync(body, ct));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<OperacionCarguioV2ResponseDto>> Update(int id, [FromBody] JsonElement body, CancellationToken ct)
    {
        try
        {
            var result = await service.UpdateAsync(id, body, ct);
            if (result is null) return NotFound(new { error = "Registro no encontrado" });
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
