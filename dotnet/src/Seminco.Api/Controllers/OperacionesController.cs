using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seminco.Application.Operaciones;

namespace Seminco.Api.Controllers;

[ApiController]
[Route("api/operaciones")]
public sealed class OperacionesController(IOperacionService service) : ControllerBase
{
    [HttpPost("crear")]
    [AllowAnonymous]
    public async Task<ActionResult> Crear([FromBody] JsonElement body, CancellationToken ct)
    {
        try
        {
            var tipo = body.GetProperty("tipo").GetString()!;
            var data = body.GetProperty("data");
            var result = await service.CreateAsync(tipo, data, ct);
            return Ok(new { ok = true, data = result });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { ok = false, error = ex.Message });
        }
    }

    [HttpGet("{tipo}")]
    public async Task<ActionResult> Obtener(string tipo, [FromQuery] string? estado, [FromQuery] string? envio, CancellationToken ct)
    {
        try
        {
            var result = await service.GetAllAsync(tipo, estado, envio, ct);
            return Ok(new { ok = true, data = result });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { ok = false, error = ex.Message });
        }
    }

    [HttpGet("aprobacion/{tipo}")]
    public async Task<ActionResult> ObtenerPorAprobacion(string tipo, [FromQuery] string? estado, [FromQuery] string? envio, CancellationToken ct)
    {
        try
        {
            var result = await service.GetByAprobacionAsync(tipo, estado, envio, ct);
            return Ok(new { ok = true, data = result });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { ok = false, error = ex.Message });
        }
    }

    [HttpGet("horometros/ultimos")]
    public async Task<ActionResult> ObtenerUltimosHorometros(CancellationToken ct)
    {
        var result = await service.GetUltimosHorometrosAsync(ct);
        return Ok(new { ok = true, data = result });
    }

    [HttpGet("{tipo}/jefe")]
    public async Task<ActionResult> ObtenerPorJefe(
        string tipo,
        [FromQuery] string jefe_guardia,
        CancellationToken ct,
        [FromQuery] int limit = 50,
        [FromQuery] int offset = 0)
    {
        if (string.IsNullOrWhiteSpace(jefe_guardia))
            return BadRequest(new { ok = false, error = "Debe enviar el jefe_guardia" });

        try
        {
            var result = await service.GetByJefeAsync(tipo, jefe_guardia, limit, offset, ct);
            return Ok(new { ok = true, data = result });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { ok = false, error = ex.Message });
        }
    }

    [HttpGet("{tipo}/id/{id:int}")]
    public async Task<ActionResult> ObtenerPorId(string tipo, int id, CancellationToken ct)
    {
        try
        {
            var result = await service.GetByIdAsync(tipo, id, ct);
            if (result is null)
                return NotFound(new { ok = false, error = "Registro no encontrado" });
            return Ok(new { ok = true, data = result });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { ok = false, error = ex.Message });
        }
    }

    [HttpPut("update/{tipo}/{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult> Actualizar(string tipo, int id, [FromBody] JsonElement data, CancellationToken ct)
    {
        try
        {
            var result = await service.UpdateAsync(tipo, id, data, ct);
            if (result is null)
                return NotFound(new { ok = false, error = "Registro no encontrado" });
            return Ok(new { ok = true, data = result });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { ok = false, error = ex.Message });
        }
    }
}
