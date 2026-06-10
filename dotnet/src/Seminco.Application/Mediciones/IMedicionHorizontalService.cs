namespace Seminco.Application.Mediciones;

public interface IMedicionHorizontalService
{
    Task<List<MedicionHorizontalDto>> GetAllAsync(int? remanente, CancellationToken ct);
    Task<MedicionHorizontalDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<(bool Conflict, string? ConflictMessage, object Result)> CreateAsync(object body, CancellationToken ct);
    Task<MedicionHorizontalDto?> UpdateAsync(int id, MedicionHorizontalDto dto, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
