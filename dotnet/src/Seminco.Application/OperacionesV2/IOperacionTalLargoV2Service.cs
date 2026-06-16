using System.Text.Json;
using Seminco.Application.Operaciones;

namespace Seminco.Application.OperacionesV2;

public interface IOperacionTalLargoV2Service
{
    Task<List<OperacionDto>> GetAllAsync(string? estado, string? envio, CancellationToken ct);
    Task<OperacionTalLargoV2ResponseDto> CreateAsync(JsonElement body, CancellationToken ct);
    Task<OperacionTalLargoV2ResponseDto?> UpdateAsync(int id, JsonElement body, CancellationToken ct);
    Task<List<OperacionDto>> GetByAprobacionAsync(string? estado, string? envio, CancellationToken ct);
    Task<OperacionDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<List<OperacionDto>> GetByJefeAsync(string jefeGuardia, int limit, int offset, CancellationToken ct);
}
