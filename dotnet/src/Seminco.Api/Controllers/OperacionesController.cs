using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seminco.Application.Operaciones;
using Seminco.Application.OperacionesV2;

namespace Seminco.Api.Controllers;

[ApiController]
[Route("api/operaciones")]
public sealed class OperacionesController(
    IOperacionService service,
    IOperacionTalHorizontalV2Service talHorizontalV2Service,
    IOperacionCarguioV2Service carguioV2Service,
    IOperacionTalLargoV2Service talLargoV2Service,
    IOperacionEmpernadorV2Service empernadorV2Service,
    IOperacionScalaminV2Service scalaminV2Service,
    IOperacionScissorV2Service scissorV2Service) : ControllerBase
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
            var result = tipo.ToLowerInvariant() switch
            {
                "tal_horizontal" => await talHorizontalV2Service.GetAllAsync(estado, envio, ct),
                "carguio" => await carguioV2Service.GetAllAsync(estado, envio, ct),
                "tal_largo" => await talLargoV2Service.GetAllAsync(estado, envio, ct),
                "empernador" => await empernadorV2Service.GetAllAsync(estado, envio, ct),
                "scalamin" => await scalaminV2Service.GetAllAsync(estado, envio, ct),
                "scissor" => await scissorV2Service.GetAllAsync(estado, envio, ct),
                _ => await service.GetAllAsync(tipo, estado, envio, ct)
            };
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
            var result = tipo.ToLowerInvariant() switch
            {
                "tal_horizontal" => await talHorizontalV2Service.GetByAprobacionAsync(estado, envio, ct),
                "carguio" => await carguioV2Service.GetByAprobacionAsync(estado, envio, ct),
                "tal_largo" => await talLargoV2Service.GetByAprobacionAsync(estado, envio, ct),
                "empernador" => await empernadorV2Service.GetByAprobacionAsync(estado, envio, ct),
                "scalamin" => await scalaminV2Service.GetByAprobacionAsync(estado, envio, ct),
                "scissor" => await scissorV2Service.GetByAprobacionAsync(estado, envio, ct),
                _ => await service.GetByAprobacionAsync(tipo, estado, envio, ct)
            };
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
            var result = tipo.ToLowerInvariant() switch
            {
                "tal_horizontal" => await talHorizontalV2Service.GetByJefeAsync(jefe_guardia, limit, offset, ct),
                "carguio" => await carguioV2Service.GetByJefeAsync(jefe_guardia, limit, offset, ct),
                "tal_largo" => await talLargoV2Service.GetByJefeAsync(jefe_guardia, limit, offset, ct),
                "empernador" => await empernadorV2Service.GetByJefeAsync(jefe_guardia, limit, offset, ct),
                "scalamin" => await scalaminV2Service.GetByJefeAsync(jefe_guardia, limit, offset, ct),
                "scissor" => await scissorV2Service.GetByJefeAsync(jefe_guardia, limit, offset, ct),
                _ => await service.GetByJefeAsync(tipo, jefe_guardia, limit, offset, ct)
            };
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
            var result = tipo.ToLowerInvariant() switch
            {
                "tal_horizontal" => await talHorizontalV2Service.GetByIdAsync(id, ct),
                "carguio" => await carguioV2Service.GetByIdAsync(id, ct),
                "tal_largo" => await talLargoV2Service.GetByIdAsync(id, ct),
                "empernador" => await empernadorV2Service.GetByIdAsync(id, ct),
                "scalamin" => await scalaminV2Service.GetByIdAsync(id, ct),
                "scissor" => await scissorV2Service.GetByIdAsync(id, ct),
                _ => await service.GetByIdAsync(tipo, id, ct)
            };
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
