using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Seminco.Application.Operaciones;
using Seminco.Domain.Operaciones;
using Seminco.Infrastructure.Persistence;

namespace Seminco.Infrastructure.Operaciones;

public sealed class OperacionService(SemincoDbContext db) : IOperacionService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    private static readonly Dictionary<string, Type> EntityTypeMap = new()
    {
        ["tal_largo"] = typeof(OperacionTalLargo),
        ["tal_horizontal"] = typeof(OperacionTalHorizontal),
        ["empernador"] = typeof(OperacionEmpernador),
        ["carguio"] = typeof(OperacionCarguio),
        ["rompebanco"] = typeof(OperacionRompebanco),
        ["scissor"] = typeof(OperacionScissor),
        ["anfochanger"] = typeof(OperacionAnfochanger),
        ["scalamin"] = typeof(OperacionScalamin),
        ["dumper"] = typeof(OperacionDumper),
    };

    private static readonly Dictionary<string, Func<SemincoDbContext, IQueryable<OperacionBase>>> QueryFactories = new()
    {
        ["tal_largo"] = db => db.Set<OperacionTalLargo>().AsNoTracking().Cast<OperacionBase>(),
        ["tal_horizontal"] = db => db.Set<OperacionTalHorizontal>().AsNoTracking().Cast<OperacionBase>(),
        ["empernador"] = db => db.Set<OperacionEmpernador>().AsNoTracking().Cast<OperacionBase>(),
        ["carguio"] = db => db.Set<OperacionCarguio>().AsNoTracking().Cast<OperacionBase>(),
        ["rompebanco"] = db => db.Set<OperacionRompebanco>().AsNoTracking().Cast<OperacionBase>(),
        ["scissor"] = db => db.Set<OperacionScissor>().AsNoTracking().Cast<OperacionBase>(),
        ["anfochanger"] = db => db.Set<OperacionAnfochanger>().AsNoTracking().Cast<OperacionBase>(),
        ["scalamin"] = db => db.Set<OperacionScalamin>().AsNoTracking().Cast<OperacionBase>(),
        ["dumper"] = db => db.Set<OperacionDumper>().AsNoTracking().Cast<OperacionBase>(),
    };

    private static readonly Dictionary<string, Func<SemincoDbContext, int, CancellationToken, Task<OperacionBase?>>> FindFactories = new()
    {
        ["tal_largo"] = async (db, id, ct) => await db.Set<OperacionTalLargo>().FindAsync([id], ct) is OperacionTalLargo e ? e : null,
        ["tal_horizontal"] = async (db, id, ct) => await db.Set<OperacionTalHorizontal>().FindAsync([id], ct) is OperacionTalHorizontal e ? e : null,
        ["empernador"] = async (db, id, ct) => await db.Set<OperacionEmpernador>().FindAsync([id], ct) is OperacionEmpernador e ? e : null,
        ["carguio"] = async (db, id, ct) => await db.Set<OperacionCarguio>().FindAsync([id], ct) is OperacionCarguio e ? e : null,
        ["rompebanco"] = async (db, id, ct) => await db.Set<OperacionRompebanco>().FindAsync([id], ct) is OperacionRompebanco e ? e : null,
        ["scissor"] = async (db, id, ct) => await db.Set<OperacionScissor>().FindAsync([id], ct) is OperacionScissor e ? e : null,
        ["anfochanger"] = async (db, id, ct) => await db.Set<OperacionAnfochanger>().FindAsync([id], ct) is OperacionAnfochanger e ? e : null,
        ["scalamin"] = async (db, id, ct) => await db.Set<OperacionScalamin>().FindAsync([id], ct) is OperacionScalamin e ? e : null,
        ["dumper"] = async (db, id, ct) => await db.Set<OperacionDumper>().FindAsync([id], ct) is OperacionDumper e ? e : null,
    };

    private static JsonElement? ParseJson(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return null;
        try { return JsonSerializer.Deserialize<JsonElement>(value); }
        catch { return JsonDocument.Parse($"\"{value}\"").RootElement.Clone(); }
    }

    private OperacionDto MapToDto(string tipo, OperacionBase entity)
    {
        static JsonElement? Parse(string? v) => string.IsNullOrWhiteSpace(v) ? null : JsonSerializer.Deserialize<JsonElement>(v);
        return new OperacionDto(
            entity.Id, tipo, entity.Fecha, entity.Turno,
            entity is OperacionTalLargo tl ? tl.Seccion :
            entity is OperacionTalHorizontal th ? th.Seccion :
            entity is OperacionEmpernador em ? em.Seccion :
            entity is OperacionCarguio ca ? ca.Seccion :
            entity is OperacionScissor sc ? sc.Seccion :
            entity is OperacionAnfochanger an ? an.Seccion :
            entity is OperacionScalamin sl ? sl.Seccion :
            entity is OperacionDumper du ? du.Seccion : null,
            entity.Operador, entity.JefeGuardia, entity.Equipo, entity.NEquipo,
            entity is OperacionTalLargo tla ? tla.ModeloEquipo :
            entity is OperacionTalHorizontal tha ? tha.ModeloEquipo :
            entity is OperacionScissor sci ? sci.ModeloEquipo :
            entity is OperacionAnfochanger anf ? anf.ModeloEquipo :
            entity is OperacionScalamin sca ? sca.ModeloEquipo : null,
            entity is OperacionCarguio c ? c.Capacidad :
            entity is OperacionDumper d ? d.Capacidad : null,
            entity is OperacionEmpernador e ? e.TipoEquipo :
            entity is OperacionCarguio cc ? cc.TipoEquipo :
            entity is OperacionDumper dd ? dd.TipoEquipo : null,
            entity is OperacionCarguio caa ? Parse(caa.ProgramaTrabajo) :
            entity is OperacionDumper ddd ? Parse(ddd.ProgramaTrabajo) : null,
            entity is OperacionDumper dddd ? Parse(dddd.CheckListTelemando) : null,
            Parse(entity.Registros), Parse(entity.Horometros),
            Parse(entity.CondicionesEquipo), Parse(entity.CheckList),
            Parse(entity.ControlLlantas),
            entity.Estado, entity.Envio, entity.Revisado, entity.Aprobacion,
            Parse(entity.ObservacionesJefe), Parse(entity.ObservacionesJefe2), Parse(entity.ObservacionesJefe3));
    }

    private static Type ResolveType(string tipo) =>
        EntityTypeMap.TryGetValue(tipo, out var type) ? type
            : throw new ArgumentException($"Tipo de operación inválido: {tipo}");

    public async Task<List<OperacionDto>> GetAllAsync(string tipo, string? estado, string? envio, CancellationToken ct)
    {
        var query = QueryFactories[tipo](db);
        if (estado is not null) query = query.Where(e => e.Estado == estado);
        if (envio is not null) query = query.Where(e => e.Envio == int.Parse(envio));
        query = query.OrderByDescending(e => e.Id);

        var entities = await query.ToListAsync(ct);
        return entities.Select(e => MapToDto(tipo, e)).ToList();
    }

    public async Task<OperacionDto?> GetByIdAsync(string tipo, int id, CancellationToken ct)
    {
        var entity = await QueryFactories[tipo](db).FirstOrDefaultAsync(e => e.Id == id, ct);
        return entity is not null ? MapToDto(tipo, entity) : null;
    }

    public async Task<List<OperacionDto>> GetByAprobacionAsync(string tipo, string? estado, string? envio, CancellationToken ct)
    {
        var query = QueryFactories[tipo](db);
        query = query.Where(e => e.Aprobacion == 0 || e.Aprobacion == 1);
        if (estado is not null) query = query.Where(e => e.Estado == estado);
        if (envio is not null) query = query.Where(e => e.Envio == int.Parse(envio));
        query = query.OrderByDescending(e => e.Id);

        var entities = await query.ToListAsync(ct);
        return entities.Select(e => MapToDto(tipo, e)).ToList();
    }

    public async Task<List<OperacionDto>> GetByJefeAsync(string tipo, string jefeGuardia, int limit, int offset, CancellationToken ct)
    {
        var query = QueryFactories[tipo](db)
            .Where(e => e.JefeGuardia == jefeGuardia)
            .OrderByDescending(e => e.Id)
            .Skip(offset).Take(limit);

        var entities = await query.ToListAsync(ct);
        return entities.Select(e => MapToDto(tipo, e)).ToList();
    }

    public async Task<Dictionary<string, JsonElement?>> GetUltimosHorometrosAsync(CancellationToken ct)
    {
        var result = new Dictionary<string, JsonElement?>();
        foreach (var (tipo, factory) in QueryFactories)
        {
            var ultimo = await factory(db)
                .OrderByDescending(e => e.Id)
                .Select(e => e.Horometros)
                .FirstOrDefaultAsync(ct);
            result[tipo] = ParseJson(ultimo);
        }
        return result;
    }

    public async Task<OperacionDto> CreateAsync(string tipo, JsonElement data, CancellationToken ct)
    {
        var type = ResolveType(tipo);

        var entity = JsonSerializer.Deserialize(data.GetRawText(), type, JsonOptions) as OperacionBase
            ?? throw new InvalidOperationException("Error al deserializar la operación");

        if (data.TryGetProperty("registros", out var r) && r.ValueKind == JsonValueKind.Object) entity.Registros = r.GetRawText();
        if (data.TryGetProperty("horometros", out var h) && h.ValueKind == JsonValueKind.Object) entity.Horometros = h.GetRawText();
        if (data.TryGetProperty("condiciones_equipo", out var ce) && ce.ValueKind == JsonValueKind.Object) entity.CondicionesEquipo = ce.GetRawText();
        if (data.TryGetProperty("check_list", out var cl) && cl.ValueKind == JsonValueKind.Object) entity.CheckList = cl.GetRawText();
        if (data.TryGetProperty("control_llantas", out var cll) && cll.ValueKind == JsonValueKind.Object) entity.ControlLlantas = cll.GetRawText();

        db.Add(entity);
        await db.SaveChangesAsync(ct);

        return MapToDto(tipo, entity);
    }

    public async Task<OperacionDto?> UpdateAsync(string tipo, int id, JsonElement data, CancellationToken ct)
    {
        var entity = await FindFactories[tipo](db, id, ct);
        if (entity is null) return null;

        foreach (var prop in data.EnumerateObject())
        {
            var efProp = db.Entry(entity).Property(prop.Name);
            if (efProp.Metadata.Name is not null)
            {
                var value = prop.Value.ValueKind switch
                {
                    JsonValueKind.Object or JsonValueKind.Array => prop.Value.GetRawText(),
                    JsonValueKind.String => prop.Value.GetString(),
                    JsonValueKind.Number => prop.Value.GetRawText(),
                    JsonValueKind.Null => null,
                    _ => prop.Value.GetRawText()
                };
                efProp.CurrentValue = value;
            }
        }

        await db.SaveChangesAsync(ct);
        return MapToDto(tipo, entity);
    }
}
