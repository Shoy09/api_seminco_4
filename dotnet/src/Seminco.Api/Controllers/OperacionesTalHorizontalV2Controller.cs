using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seminco.Application.OperacionesV2;

namespace Seminco.Api.Controllers;

[ApiController]
[Route("api/operaciones-v2/tal-horizontal")]
public sealed class OperacionesTalHorizontalV2Controller(IOperacionTalHorizontalV2Service service) : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<OperacionTalHorizontalV2ResponseDto>> Create([FromBody] JsonElement body, CancellationToken ct)
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
    public async Task<ActionResult<OperacionTalHorizontalV2ResponseDto>> Update(int id, [FromBody] JsonElement body, CancellationToken ct)
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
