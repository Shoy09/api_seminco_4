using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seminco.Application.Catalogs;

namespace Seminco.Api.Controllers;

[ApiController]
[Authorize]
public abstract class CatalogController<TDto>(ICatalogService<TDto> service) : ControllerBase where TDto : class
{
    [HttpGet]
    public async Task<ActionResult<List<TDto>>> GetAll(CancellationToken ct) =>
        Ok(await service.GetAllAsync(ct));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TDto>> GetById(int id, CancellationToken ct)
    {
        var result = await service.GetByIdAsync(id, ct);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TDto>> Create(TDto dto, CancellationToken ct)
    {
        var result = await service.CreateAsync(dto, ct);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<TDto>> Update(int id, TDto dto, CancellationToken ct)
    {
        var result = await service.UpdateAsync(id, dto, ct);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id, CancellationToken ct)
    {
        var deleted = await service.DeleteAsync(id, ct);
        if (!deleted) return NotFound();
        return Ok(new { message = "Eliminado correctamente" });
    }
}
