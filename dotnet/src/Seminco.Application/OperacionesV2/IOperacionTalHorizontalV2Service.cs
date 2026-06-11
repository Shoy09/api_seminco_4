using System.Text.Json;

namespace Seminco.Application.OperacionesV2;

public interface IOperacionTalHorizontalV2Service
{
    Task<OperacionTalHorizontalV2ResponseDto> CreateAsync(JsonElement body, CancellationToken ct);
    Task<OperacionTalHorizontalV2ResponseDto?> UpdateAsync(int id, JsonElement body, CancellationToken ct);
}
