using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seminco.Application.Mediciones;

namespace Seminco.Api.Controllers;

[ApiController]
[Route("api/medicion-tal-horizontal")]
[Authorize]
public sealed class MedicionesHorizontalController(IMedicionHorizontalService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<MedicionHorizontalDto>>> GetAll([FromQuery] int? remanente, CancellationToken ct) =>
        Ok(await service.GetAllAsync(remanente, ct));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MedicionHorizontalDto>> GetById(int id, CancellationToken ct)
    {
        var result = await service.GetByIdAsync(id, ct);
        if (result is null) return NotFound(new { message = "Medición horizontal no encontrada" });
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> Create([FromBody] JsonElement body, CancellationToken ct)
    {
        var payload = JsonSerializer.Deserialize<object>(body.GetRawText())!;
        var result = await service.CreateAsync(payload, ct);
        if (result.Conflict) return Conflict(new { message = result.ConflictMessage });
        return StatusCode(StatusCodes.Status201Created, result.Result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<MedicionHorizontalDto>> Update(int id, [FromBody] MedicionHorizontalDto dto, CancellationToken ct)
    {
        var result = await service.UpdateAsync(id, dto, ct);
        if (result is null) return NotFound(new { message = "Medición horizontal no encontrada" });
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id, CancellationToken ct)
    {
        var deleted = await service.DeleteAsync(id, ct);
        if (!deleted) return NotFound(new { message = "Medición horizontal no encontrada" });
        return Ok(new { message = "Medición horizontal eliminada correctamente" });
    }
}
