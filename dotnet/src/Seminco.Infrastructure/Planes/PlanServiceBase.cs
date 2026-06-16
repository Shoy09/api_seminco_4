using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;
using Seminco.Application.Planes;
using Seminco.Domain.Planes;
using Seminco.Infrastructure.Catalogs;
using Seminco.Infrastructure.Persistence;

namespace Seminco.Infrastructure.Planes;

public abstract class PlanServiceBase<TEntity, TDto>(SemincoDbContext db, string columnPrefix)
    : CatalogService<TEntity, TDto>(db), IPlanService<TDto>
    where TEntity : PlanBase
    where TDto : class
{
    protected string ColumnPrefix { get; } = columnPrefix;

    public async Task<List<TDto>> GetByYearAndMonthAsync(int anio, string mes, CancellationToken ct)
    {
        var entities = await Db.Set<TEntity>()
            .Where(e => e.Anio == anio && e.Mes == mes)
            .AsNoTracking()
            .ToListAsync(ct);
        return entities.Select(ToDto).ToList();
    }

    private static readonly PropertyInfo[] ColumnProps = typeof(PlanBase)
        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
        .Where(p => p.Name.StartsWith("Col") && p.PropertyType == typeof(string) && p.Name.Length >= 4)
        .ToArray();

    protected void SetEntityColumns(PlanBase entity, Dictionary<string, JsonElement>? columnas)
    {
        foreach (var prop in ColumnProps)
        {
            var day = prop.Name[3..^1];
            var shift = prop.Name[^1..];
            var colName = $"{ColumnPrefix}{day}{shift}";

            if (columnas?.TryGetValue(colName, out var val) == true && val.ValueKind != JsonValueKind.Null)
                prop.SetValue(entity, val.GetString());
            else
                prop.SetValue(entity, null);
        }
    }

    protected void SetDtoColumns(PlanBase entity, Dictionary<string, JsonElement> columnas)
    {
        columnas.Clear();
        foreach (var prop in ColumnProps)
        {
            var day = prop.Name[3..^1];
            var shift = prop.Name[^1..];
            var colName = $"{ColumnPrefix}{day}{shift}";

            var val = (string?)prop.GetValue(entity);
            if (val is not null)
                columnas[colName] = JsonSerializer.SerializeToElement(val);
        }
    }
}
