using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Seminco.Application.Mediciones;
using Seminco.Domain.Mediciones;
using Seminco.Infrastructure.Persistence;

namespace Seminco.Infrastructure.Mediciones;

public sealed class MedicionHorizontalService(SemincoDbContext db) : IMedicionHorizontalService
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public async Task<List<MedicionHorizontalDto>> GetAllAsync(int? remanente, CancellationToken ct)
    {
        var query = db.MedicionesHorizontal.AsNoTracking();
        if (remanente.HasValue)
            query = query.Where(x => x.Remanente == remanente.Value);

        var items = await query.ToListAsync(ct);
        return items.Select(ToDto).ToList();
    }

    public async Task<MedicionHorizontalDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        var entity = await db.MedicionesHorizontal.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<(bool Conflict, string? ConflictMessage, object Result)> CreateAsync(object body, CancellationToken ct)
    {
        var json = JsonSerializer.Serialize(body, JsonOptions);
        using var doc = JsonDocument.Parse(json);

        if (doc.RootElement.ValueKind == JsonValueKind.Array)
        {
            var entities = new List<MedicionHorizontal>();
            foreach (var item in doc.RootElement.EnumerateArray())
            {
                var dto = JsonSerializer.Deserialize<MedicionHorizontalDto>(item.GetRawText(), JsonOptions)!;
                if (dto.IdNube.HasValue)
                {
                    var exists = await db.MedicionesHorizontal.AnyAsync(x => x.IdNube == dto.IdNube.Value, ct);
                    if (exists)
                        return (true, $"Ya existe una medición horizontal con idnube {dto.IdNube.Value}", new object());
                }
                entities.Add(ToEntity(dto));
            }

            db.MedicionesHorizontal.AddRange(entities);
            await db.SaveChangesAsync(ct);
            return (false, null, entities.Select(ToDto).ToList());
        }

        var single = JsonSerializer.Deserialize<MedicionHorizontalDto>(doc.RootElement.GetRawText(), JsonOptions)!;
        if (single.IdNube.HasValue)
        {
            var exists = await db.MedicionesHorizontal.AnyAsync(x => x.IdNube == single.IdNube.Value, ct);
            if (exists)
                return (true, $"Ya existe una medición horizontal con idnube {single.IdNube.Value}", new object());
        }

        var entity = ToEntity(single);
        db.MedicionesHorizontal.Add(entity);
        await db.SaveChangesAsync(ct);
        return (false, null, ToDto(entity));
    }

    public async Task<MedicionHorizontalDto?> UpdateAsync(int id, MedicionHorizontalDto dto, CancellationToken ct)
    {
        var entity = await db.MedicionesHorizontal.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return null;

        entity.Fecha = dto.Fecha;
        entity.Turno = dto.Turno;
        entity.Empresa = dto.Empresa;
        entity.Zona = dto.Zona;
        entity.Labor = dto.Labor;
        entity.Veta = dto.Veta;
        entity.TipoPerforacion = dto.TipoPerforacion;
        entity.KgExplosivos = dto.KgExplosivos;
        entity.AvanceProgramado = dto.AvanceProgramado;
        entity.Ancho = dto.Ancho;
        entity.Alto = dto.Alto;
        entity.Envio = dto.Envio;
        entity.IdExplosivo = dto.IdExplosivo;
        entity.IdNube = dto.IdNube;
        entity.NoAplica = dto.NoAplica;
        entity.Remanente = dto.Remanente;

        await db.SaveChangesAsync(ct);
        return ToDto(entity);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var entity = await db.MedicionesHorizontal.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return false;
        db.MedicionesHorizontal.Remove(entity);
        await db.SaveChangesAsync(ct);
        return true;
    }

    private static MedicionHorizontalDto ToDto(MedicionHorizontal x) => new(
        x.Id,
        x.Fecha,
        x.Turno,
        x.Empresa,
        x.Zona,
        x.Labor,
        x.Veta,
        x.TipoPerforacion,
        x.KgExplosivos,
        x.AvanceProgramado,
        x.Ancho,
        x.Alto,
        x.Envio,
        x.IdExplosivo,
        x.IdNube,
        x.NoAplica,
        x.Remanente);

    private static MedicionHorizontal ToEntity(MedicionHorizontalDto x) => new()
    {
        Fecha = x.Fecha,
        Turno = x.Turno,
        Empresa = x.Empresa,
        Zona = x.Zona,
        Labor = x.Labor,
        Veta = x.Veta,
        TipoPerforacion = x.TipoPerforacion,
        KgExplosivos = x.KgExplosivos,
        AvanceProgramado = x.AvanceProgramado,
        Ancho = x.Ancho,
        Alto = x.Alto,
        Envio = x.Envio,
        IdExplosivo = x.IdExplosivo,
        IdNube = x.IdNube,
        NoAplica = x.NoAplica,
        Remanente = x.Remanente,
    };
}
