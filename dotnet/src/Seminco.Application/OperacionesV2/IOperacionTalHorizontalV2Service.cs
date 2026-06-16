using System.Text.Json;
using Seminco.Application.Operaciones;

namespace Seminco.Application.OperacionesV2;

public interface IOperacionTalHorizontalV2Service
{
    Task<List<OperacionDto>> GetAllAsync(string? estado, string? envio, CancellationToken ct);
    Task<OperacionTalHorizontalV2ResponseDto> CreateAsync(JsonElement body, CancellationToken ct);
    Task<OperacionTalHorizontalV2ResponseDto?> UpdateAsync(int id, JsonElement body, CancellationToken ct);
    Task<List<OperacionDto>> GetByAprobacionAsync(string? estado, string? envio, CancellationToken ct);
    Task<OperacionDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<List<OperacionDto>> GetByJefeAsync(string jefeGuardia, int limit, int offset, CancellationToken ct);
}
