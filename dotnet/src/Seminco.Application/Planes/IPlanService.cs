using Seminco.Application.Catalogs;

namespace Seminco.Application.Planes;

public interface IPlanService<TDto> : ICatalogService<TDto> where TDto : class
{
    Task<List<TDto>> GetByYearAndMonthAsync(int anio, string mes, CancellationToken ct);
}
