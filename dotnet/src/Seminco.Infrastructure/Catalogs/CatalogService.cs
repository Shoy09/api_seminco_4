using Microsoft.EntityFrameworkCore;
using Seminco.Application.Catalogs;
using Seminco.Infrastructure.Persistence;

namespace Seminco.Infrastructure.Catalogs;

public abstract class CatalogService<TEntity, TDto>(SemincoDbContext db) : ICatalogService<TDto>
    where TEntity : class
    where TDto : class
{
    protected SemincoDbContext Db => db;

    protected abstract TDto ToDto(TEntity entity);
    protected abstract TEntity ToEntity(TDto dto);
    protected abstract void ApplyUpdate(TEntity entity, TDto dto);

    public async Task<List<TDto>> GetAllAsync(CancellationToken ct)
    {
        var entities = await Db.Set<TEntity>().AsNoTracking().ToListAsync(ct);
        return entities.Select(ToDto).ToList();
    }

    public async Task<TDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        var entity = await Db.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id, ct);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<TDto> CreateAsync(TDto dto, CancellationToken ct)
    {
        var entity = ToEntity(dto);
        Db.Set<TEntity>().Add(entity);
        await Db.SaveChangesAsync(ct);
        return ToDto(entity);
    }

    public async Task<TDto?> UpdateAsync(int id, TDto dto, CancellationToken ct)
    {
        var entity = await Db.Set<TEntity>().FindAsync([id], ct);
        if (entity is null) return null;
        ApplyUpdate(entity, dto);
        await Db.SaveChangesAsync(ct);
        return ToDto(entity);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var entity = await Db.Set<TEntity>().FindAsync([id], ct);
        if (entity is null) return false;
        Db.Set<TEntity>().Remove(entity);
        await Db.SaveChangesAsync(ct);
        return true;
    }
}
