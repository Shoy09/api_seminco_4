using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seminco.Application.Exploraciones;

namespace Seminco.Api.Controllers;

[ApiController]
[Route("api/NubeDatosExploraciones")]
public sealed class NubeDatosExploracionesController(INubeExploracionService service) : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<NubeExploracionCreateResponseDto>> Create([FromBody] JsonElement body, CancellationToken ct)
    {
        try
        {
            return StatusCode(StatusCodes.Status201Created, await service.CreateAsync(body, ct));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = "Error al crear exploración", details = ex.Message });
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<NubeExploracionDto>>> GetAll([FromQuery] int? envio, [FromQuery] int? cerrado, [FromQuery] string? empresa, CancellationToken ct) =>
        Ok(await service.GetAllAsync(envio, cerrado, empresa, ct));

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<ActionResult<NubeExploracionDto>> GetById(int id, CancellationToken ct)
    {
        var result = await service.GetByIdAsync(id, ct);
        if (result is null) return NotFound(new { message = "Exploración no encontrada" });
        return Ok(result);
    }

    [HttpPut("{id:int}/medicion")]
    [AllowAnonymous]
    public async Task<ActionResult<NubeExploracionMedicionResponseDto>> UpdateMedicion(int id, [FromBody] JsonElement body, CancellationToken ct)
    {
        if (!body.TryGetProperty("medicion", out var medicionElement) || medicionElement.ValueKind != JsonValueKind.Number)
            return BadRequest(new { error = "Error al actualizar medición", details = "El campo medicion es requerido" });

        var result = await service.UpdateMedicionAsync(id, medicionElement.GetInt32(), ct);
        if (result is null) return NotFound(new { error = "Error al actualizar medición", details = "Exploración no encontrada" });
        return Ok(result);
    }

    [HttpPut("Explo-medicion")]
    [AllowAnonymous]
    public async Task<ActionResult<NubeExploracionBulkMedicionResponseDto>> MarcarComoUsadosEnMediciones([FromBody] JsonElement body, CancellationToken ct)
    {
        try
        {
            return Ok(await service.MarcarComoUsadosEnMedicionesAsync(body, ct));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
