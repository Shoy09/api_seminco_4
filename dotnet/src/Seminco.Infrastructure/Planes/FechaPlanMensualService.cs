using Microsoft.EntityFrameworkCore;
using Seminco.Application.Planes;
using Seminco.Domain.Planes;
using Seminco.Infrastructure.Persistence;

namespace Seminco.Infrastructure.Planes;

public sealed class FechaPlanMensualService(SemincoDbContext db) : IFechaPlanMensualService
{
    public async Task<List<FechaPlanMensualDto>> GetAllAsync(CancellationToken ct)
    {
        var entities = await db.FechasPlanMensual
            .AsNoTracking()
            .OrderBy(f => f.Id)
            .ToListAsync(ct);

        return entities.Select(e => new FechaPlanMensualDto(e.Id, e.Mes, e.FechaIngreso)).ToList();
    }

    public async Task<FechaPlanMensualDto> CreateAsync(string mes, CancellationToken ct)
    {
        var entity = new FechaPlanMensual
        {
            Mes = mes,
            FechaIngreso = DateTime.UtcNow,
        };

        db.FechasPlanMensual.Add(entity);
        await db.SaveChangesAsync(ct);
        return new FechaPlanMensualDto(entity.Id, entity.Mes, entity.FechaIngreso);
    }

    public async Task<UltimaFechaDto?> GetUltimaAsync(CancellationToken ct)
    {
        var entity = await db.FechasPlanMensual
            .OrderByDescending(f => f.Id)
            .AsNoTracking()
            .FirstOrDefaultAsync(ct);

        if (entity is null) return null;

        return new UltimaFechaDto(entity.Id, entity.Mes, entity.FechaIngreso.Year);
    }
}
