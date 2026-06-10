namespace Seminco.Application.Catalogs;

public interface ICatalogService<TDto> where TDto : class
{
    Task<List<TDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<TDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<TDto> CreateAsync(TDto dto, CancellationToken cancellationToken);
    Task<TDto?> UpdateAsync(int id, TDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}
