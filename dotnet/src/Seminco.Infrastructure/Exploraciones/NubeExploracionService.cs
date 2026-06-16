using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Seminco.Application.Exploraciones;
using Seminco.Domain.Exploraciones;
using Seminco.Infrastructure.Persistence;

namespace Seminco.Infrastructure.Exploraciones;

public sealed class NubeExploracionService(SemincoDbContext db) : INubeExploracionService
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public async Task<NubeExploracionCreateResponseDto> CreateAsync(JsonElement body, CancellationToken ct)
    {
        var payload = body.ValueKind == JsonValueKind.Array
            ? body.EnumerateArray().FirstOrDefault()
            : body;

        if (payload.ValueKind is JsonValueKind.Undefined or JsonValueKind.Null)
            throw new InvalidOperationException("Faltan campos obligatorios: fecha y taladro.");

        var request = JsonSerializer.Deserialize<NubeExploracionCreateRequest>(payload.GetRawText(), JsonOptions)
            ?? throw new InvalidOperationException("Faltan campos obligatorios: fecha y taladro.");

        if (string.IsNullOrWhiteSpace(request.Fecha) || string.IsNullOrWhiteSpace(request.Taladro))
            throw new InvalidOperationException("Faltan campos obligatorios: fecha y taladro.");

        var now = DateTime.UtcNow;
        var entity = new NubeExploracion
        {
            Fecha = request.Fecha,
            Turno = request.Turno ?? string.Empty,
            Taladro = request.Taladro,
            PiesPorTaladro = request.PiesPorTaladro ?? string.Empty,
            Zona = request.Zona ?? string.Empty,
            TipoLabor = request.TipoLabor ?? string.Empty,
            Labor = request.Labor ?? string.Empty,
            Ala = request.Ala,
            Veta = request.Veta ?? string.Empty,
            Nivel = request.Nivel ?? string.Empty,
            TipoPerforacion = request.TipoPerforacion ?? string.Empty,
            Estado = string.IsNullOrWhiteSpace(request.Estado) ? "Creado" : request.Estado,
            Cerrado = request.Cerrado ?? 0,
            Envio = request.Envio ?? 0,
            SemanaDefault = request.SemanaDefault,
            SemanaSelect = request.SemanaSelect,
            Empresa = request.Empresa,
            Seccion = request.Seccion,
            Medicion = request.Medicion ?? 0,
            CreatedAt = now,
            UpdatedAt = now,
            Despachos = (request.Despachos ?? []).Select(d => new NubeDespacho
            {
                MiliSegundo = d.MiliSegundo,
                MedioSegundo = d.MedioSegundo,
                Observaciones = d.Observaciones,
                CreatedAt = now,
                UpdatedAt = now,
                Detalles = (d.DetallesMateriales ?? []).Select(m => new NubeDespachoDetalle
                {
                    NombreMaterial = m.NombreMaterial,
                    Cantidad = m.Cantidad,
                    CreatedAt = now,
                    UpdatedAt = now,
                }).ToList(),
                DetallesExplosivos = (d.DetallesExplosivos ?? []).Select(e => new NubeDetalleDespachoExplosivo
                {
                    Longitud = e.Longitud,
                    Tipo = e.Tipo,
                    Retardos = e.Retardos.GetRawText(),
                    CreatedAt = now,
                    UpdatedAt = now,
                }).ToList(),
            }).ToList(),
            Devoluciones = (request.Devoluciones ?? []).Select(d => new NubeDevolucion
            {
                MiliSegundo = d.MiliSegundo,
                MedioSegundo = d.MedioSegundo,
                Observaciones = d.Observaciones,
                CreatedAt = now,
                UpdatedAt = now,
                Detalles = (d.DetallesMateriales ?? []).Select(m => new NubeDevolucionDetalle
                {
                    NombreMaterial = m.NombreMaterial,
                    Cantidad = m.Cantidad,
                    CreatedAt = now,
                    UpdatedAt = now,
                }).ToList(),
                DetallesExplosivos = (d.DetallesExplosivos ?? []).Select(e => new NubeDetalleDevolucionExplosivo
                {
                    Longitud = e.Longitud,
                    Tipo = e.Tipo,
                    Retardos = e.Retardos.GetRawText(),
                    CreatedAt = now,
                    UpdatedAt = now,
                }).ToList(),
            }).ToList(),
        };

        await using var tx = await db.Database.BeginTransactionAsync(ct);
        db.NubeExploraciones.Add(entity);
        await db.SaveChangesAsync(ct);
        await tx.CommitAsync(ct);

        return new NubeExploracionCreateResponseDto("Exploración creada con éxito", entity.Id, entity.Envio, entity.Estado);
    }

    public async Task<List<NubeExploracionDto>> GetAllAsync(int? envio, int? cerrado, string? empresa, CancellationToken ct)
    {
        var query = QueryBase();
        if (envio.HasValue) query = query.Where(x => x.Envio == envio.Value);
        if (cerrado.HasValue) query = query.Where(x => x.Cerrado == cerrado.Value);
        if (!string.IsNullOrWhiteSpace(empresa)) query = query.Where(x => x.Empresa == empresa);

        var entities = await query
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(ct);

        return entities.Select(ToDto).ToList();
    }

    public async Task<NubeExploracionDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        var entity = await QueryBase().FirstOrDefaultAsync(x => x.Id == id, ct);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<NubeExploracionMedicionResponseDto?> UpdateMedicionAsync(int id, int medicion, CancellationToken ct)
    {
        var entity = await db.NubeExploraciones.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return null;

        entity.Medicion = medicion;
        entity.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync(ct);

        return new NubeExploracionMedicionResponseDto(
            "Medición actualizada correctamente",
            new NubeExploracionMedicionDataDto(entity.Id, entity.Medicion, entity.Estado));
    }

    public async Task<NubeExploracionBulkMedicionResponseDto> MarcarComoUsadosEnMedicionesAsync(JsonElement body, CancellationToken ct)
    {
        if (!body.TryGetProperty("ids", out var idsElement))
            throw new InvalidOperationException("No se recibieron IDs para actualizar.");

        var ids = idsElement.ValueKind == JsonValueKind.Array
            ? idsElement.EnumerateArray().Select(x => x.GetInt32()).ToList()
            : [idsElement.GetInt32()];

        if (ids.Count == 0)
            throw new InvalidOperationException("No se recibieron IDs para actualizar.");

        var entities = await db.NubeExploraciones.Where(x => ids.Contains(x.Id)).ToListAsync(ct);
        foreach (var entity in entities)
        {
            entity.Medicion = 1;
            entity.UpdatedAt = DateTime.UtcNow;
        }

        await db.SaveChangesAsync(ct);
        return new NubeExploracionBulkMedicionResponseDto("Registros actualizados correctamente", entities.Count);
    }

    private IQueryable<NubeExploracion> QueryBase() => db.NubeExploraciones
        .AsNoTracking()
        .Include(x => x.Despachos)
            .ThenInclude(x => x.Detalles)
        .Include(x => x.Despachos)
            .ThenInclude(x => x.DetallesExplosivos)
        .Include(x => x.Devoluciones)
            .ThenInclude(x => x.Detalles)
        .Include(x => x.Devoluciones)
            .ThenInclude(x => x.DetallesExplosivos);

    private static NubeExploracionDto ToDto(NubeExploracion entity) => new(
        entity.Id,
        entity.Fecha,
        entity.Turno,
        entity.Taladro,
        entity.PiesPorTaladro,
        entity.Zona,
        entity.TipoLabor,
        entity.Labor,
        entity.Ala,
        entity.Veta,
        entity.Nivel,
        entity.TipoPerforacion,
        entity.Estado,
        entity.Cerrado,
        entity.Envio,
        entity.SemanaDefault,
        entity.SemanaSelect,
        entity.Empresa,
        entity.Seccion,
        entity.Medicion,
        entity.Despachos.OrderBy(x => x.CreatedAt).Select(ToDto).ToList(),
        entity.Devoluciones.OrderBy(x => x.CreatedAt).Select(ToDto).ToList(),
        entity.CreatedAt,
        entity.UpdatedAt);

    private static NubeDespachoDto ToDto(NubeDespacho entity) => new(
        entity.Id,
        entity.DatosTrabajoId,
        entity.MiliSegundo,
        entity.MedioSegundo,
        entity.Observaciones,
        entity.Detalles.OrderBy(x => x.CreatedAt).Select(x => new NubeMaterialDto(x.Id, x.NombreMaterial, x.Cantidad, x.CreatedAt, x.UpdatedAt)).ToList(),
        entity.DetallesExplosivos.OrderBy(x => x.CreatedAt).Select(ToDto).ToList(),
        entity.CreatedAt,
        entity.UpdatedAt);

    private static NubeDevolucionDto ToDto(NubeDevolucion entity) => new(
        entity.Id,
        entity.DatosTrabajoId,
        entity.MiliSegundo,
        entity.MedioSegundo,
        entity.Observaciones,
        entity.Detalles.OrderBy(x => x.CreatedAt).Select(x => new NubeMaterialDto(x.Id, x.NombreMaterial, x.Cantidad, x.CreatedAt, x.UpdatedAt)).ToList(),
        entity.DetallesExplosivos.OrderBy(x => x.CreatedAt).Select(ToDto).ToList(),
        entity.CreatedAt,
        entity.UpdatedAt);

    private static NubeExplosivoDto ToDto(NubeDetalleDespachoExplosivo entity) => new(
        entity.Id,
        entity.Longitud,
        entity.Tipo,
        ParseJson(entity.Retardos),
        entity.CreatedAt,
        entity.UpdatedAt);

    private static NubeExplosivoDto ToDto(NubeDetalleDevolucionExplosivo entity) => new(
        entity.Id,
        entity.Longitud,
        entity.Tipo,
        ParseJson(entity.Retardos),
        entity.CreatedAt,
        entity.UpdatedAt);

    private static JsonElement ParseJson(string value)
    {
        try { return JsonSerializer.Deserialize<JsonElement>(value); }
        catch { return JsonSerializer.Deserialize<JsonElement>("[]"); }
    }
}
