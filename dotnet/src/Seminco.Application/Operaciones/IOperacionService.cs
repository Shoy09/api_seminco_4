using System.Text.Json;

namespace Seminco.Application.Operaciones;

public interface IOperacionService
{
    Task<List<OperacionDto>> GetAllAsync(string tipo, string? estado, string? envio, CancellationToken cancellationToken);
    Task<OperacionDto?> GetByIdAsync(string tipo, int id, CancellationToken cancellationToken);
    Task<List<OperacionDto>> GetByAprobacionAsync(string tipo, string? estado, string? envio, CancellationToken cancellationToken);
    Task<List<OperacionDto>> GetByJefeAsync(string tipo, string jefeGuardia, int limit, int offset, CancellationToken cancellationToken);
    Task<Dictionary<string, JsonElement?>> GetUltimosHorometrosAsync(CancellationToken cancellationToken);
    Task<OperacionDto> CreateAsync(string tipo, JsonElement data, CancellationToken cancellationToken);
    Task<OperacionDto?> UpdateAsync(string tipo, int id, JsonElement data, CancellationToken cancellationToken);
}
