using System.Text.Json;

namespace Seminco.Application.Exploraciones;

public interface INubeExploracionService
{
    Task<NubeExploracionCreateResponseDto> CreateAsync(JsonElement body, CancellationToken ct);
    Task<List<NubeExploracionDto>> GetAllAsync(int? envio, int? cerrado, string? empresa, CancellationToken ct);
    Task<NubeExploracionDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<NubeExploracionMedicionResponseDto?> UpdateMedicionAsync(int id, int medicion, CancellationToken ct);
    Task<NubeExploracionBulkMedicionResponseDto> MarcarComoUsadosEnMedicionesAsync(JsonElement body, CancellationToken ct);
}
