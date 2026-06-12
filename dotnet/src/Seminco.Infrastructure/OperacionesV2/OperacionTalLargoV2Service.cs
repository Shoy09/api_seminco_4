using System.Globalization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Seminco.Application.Operaciones;
using Seminco.Application.OperacionesV2;
using Seminco.Domain.OperacionesV2;
using Seminco.Infrastructure.Persistence;

namespace Seminco.Infrastructure.OperacionesV2;

public sealed class OperacionTalLargoV2Service(SemincoDbContext db) : IOperacionTalLargoV2Service
{
    private const string TipoOperacion = "tal_largo";
    private const string Proceso = "PERFORACIÓN TALADROS LARGOS";
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    private IQueryable<OperacionTalLargo> ReadQuery() => db.OperacionesTalLargoV2.AsNoTracking().AsSplitQuery()
        .Include(x => x.CondicionEquipo)
        .Include(x => x.Horometros)
        .Include(x => x.ChecklistRespuestas)
        .Include(x => x.ControlLlantas)
        .Include(x => x.Registros.OrderBy(r => r.Id)).ThenInclude(x => x.Detalle);

    public async Task<List<OperacionDto>> GetAllAsync(string? estado, string? envio, CancellationToken ct)
    {
        var query = ReadQuery();
        if (estado is not null) query = query.Where(x => x.Estado == estado);
        if (envio is not null) query = query.Where(x => x.Envio == int.Parse(envio));
        return (await query.OrderByDescending(x => x.Id).ToListAsync(ct)).Select(MapToDto).ToList();
    }

    public async Task<List<OperacionDto>> GetByAprobacionAsync(string? estado, string? envio, CancellationToken ct)
    {
        var query = ReadQuery().Where(x => x.Aprobacion == 0 || x.Aprobacion == 1);
        if (estado is not null) query = query.Where(x => x.Estado == estado);
        if (envio is not null) query = query.Where(x => x.Envio == int.Parse(envio));
        return (await query.OrderByDescending(x => x.Id).ToListAsync(ct)).Select(MapToDto).ToList();
    }

    public async Task<OperacionDto?> GetByIdAsync(int id, CancellationToken ct)
        => await ReadQuery().FirstOrDefaultAsync(x => x.Id == id, ct) is { } entity ? MapToDto(entity) : null;

    public async Task<List<OperacionDto>> GetByJefeAsync(string jefeGuardia, int limit, int offset, CancellationToken ct)
        => (await ReadQuery().Where(x => x.JefeGuardia == jefeGuardia).OrderByDescending(x => x.Id).Skip(offset).Take(limit).ToListAsync(ct)).Select(MapToDto).ToList();

    public async Task<OperacionTalLargoV2ResponseDto> CreateAsync(JsonElement body, CancellationToken ct)
    {
        var request = Deserialize(body);
        var entity = new OperacionTalLargo { CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
        await ApplyRequestAsync(entity, request, body.GetRawText(), ct, false);
        db.OperacionesTalLargoV2.Add(entity);
        await db.SaveChangesAsync(ct);
        return new OperacionTalLargoV2ResponseDto(entity.Id, "Operación tal_largo V2 creada correctamente");
    }

    public async Task<OperacionTalLargoV2ResponseDto?> UpdateAsync(int id, JsonElement body, CancellationToken ct)
    {
        var entity = await db.OperacionesTalLargoV2
            .Include(x => x.CondicionEquipo).Include(x => x.Horometros).Include(x => x.ChecklistRespuestas).Include(x => x.ControlLlantas)
            .Include(x => x.Registros).ThenInclude(x => x.Detalle).FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return null;
        await ApplyRequestAsync(entity, Deserialize(body), body.GetRawText(), ct, true);
        await db.SaveChangesAsync(ct);
        return new OperacionTalLargoV2ResponseDto(entity.Id, "Operación tal_largo V2 actualizada correctamente");
    }

    private async Task ApplyRequestAsync(OperacionTalLargo entity, OperacionTalLargoUpsertRequest request, string rawPayload, CancellationToken ct, bool isUpdate)
    {
        entity.Fecha = ParseFecha(request.Fecha);
        entity.Turno = request.Turno;
        entity.Operador = request.Operador;
        entity.JefeGuardia = request.JefeGuardia;
        entity.EquipoNombre = request.Equipo;
        entity.NEquipo = request.NEquipo;
        entity.Seccion = request.Seccion;
        entity.SeccionId = await ResolveSeccionIdAsync(request.Seccion, ct);
        entity.ModeloEquipo = request.ModeloEquipo;
        entity.Estado = string.IsNullOrWhiteSpace(request.Estado) ? "activo" : request.Estado;
        entity.Envio = request.Envio ?? 0;
        entity.Revisado = request.Revisado ?? 0;
        entity.Aprobacion = request.Aprobacion ?? 0;
        entity.ObservacionesJefe = request.ObservacionesJefe?.GetRawText();
        entity.ObservacionesJefe2 = request.ObservacionesJefe2?.GetRawText();
        entity.ObservacionesJefe3 = request.ObservacionesJefe3?.GetRawText();
        entity.PayloadOriginal = rawPayload;
        entity.PayloadVersion = "v2-json-input";
        entity.UpdatedAt = DateTime.UtcNow;
        if (isUpdate)
        {
            if (entity.CondicionEquipo is not null) db.Remove(entity.CondicionEquipo);
            if (entity.Horometros.Count > 0) db.RemoveRange(entity.Horometros);
            if (entity.ChecklistRespuestas.Count > 0) db.RemoveRange(entity.ChecklistRespuestas);
            if (entity.ControlLlantas.Count > 0) db.RemoveRange(entity.ControlLlantas);
            if (entity.Registros.Count > 0) db.RemoveRange(entity.Registros);
            entity.CondicionEquipo = null;
            entity.Horometros.Clear();
            entity.ChecklistRespuestas.Clear();
            entity.ControlLlantas.Clear();
            entity.Registros.Clear();
        }

        entity.Horometros = BuildHorometros(request.Horometros);
        entity.CondicionEquipo = BuildCondicionEquipo(request.CondicionesEquipo);
        entity.ChecklistRespuestas = await BuildChecklistAsync(request.CheckList, ct);
        entity.ControlLlantas = BuildControlLlantas(request.ControlLlantas);
        entity.Registros = await BuildRegistrosAsync(request.Registros, ct);
    }

    private static OperacionTalLargoUpsertRequest Deserialize(JsonElement body) => JsonSerializer.Deserialize<OperacionTalLargoUpsertRequest>(body.GetRawText(), JsonOptions) ?? throw new InvalidOperationException("No se pudo deserializar la operación tal_largo V2");
    private static DateTime ParseFecha(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new InvalidOperationException("El campo fecha es obligatorio");
        if (DateTime.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var exact)) return DateTime.SpecifyKind(exact, DateTimeKind.Utc);
        if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var parsed)) return parsed.ToUniversalTime();
        throw new InvalidOperationException($"Fecha inválida: {value}");
    }
    private static TimeOnly? ParseHoraNullable(string? value) => string.IsNullOrWhiteSpace(value) ? null : TimeOnly.TryParse(value, CultureInfo.InvariantCulture, out var time) ? time : null;
    private static TimeOnly ParseHoraRequired(string value, string fieldName) => TimeOnly.TryParse(value, CultureInfo.InvariantCulture, out var time) ? time : throw new InvalidOperationException($"Hora inválida en {fieldName}: {value}");
    private static decimal? ParseDecimal(string? value) => string.IsNullOrWhiteSpace(value) ? null : decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsed) ? parsed : null;
    private static JsonElement? ParseJson(string? value) { if (string.IsNullOrWhiteSpace(value)) return null; try { return JsonSerializer.Deserialize<JsonElement>(value); } catch { return JsonDocument.Parse($"\"{value}\"").RootElement.Clone(); } }
    private static JsonElement? ToJsonElement<T>(T value) => value is null ? null : JsonSerializer.SerializeToElement(value, JsonOptions);

    private static List<OperacionTalLargoHorometro> BuildHorometros(JsonElement? horometros)
    {
        var result = new List<OperacionTalLargoHorometro>();
        if (horometros is null || horometros.Value.ValueKind != JsonValueKind.Object) return result;
        foreach (var prop in horometros.Value.EnumerateObject())
        {
            if (prop.Value.ValueKind != JsonValueKind.Object) continue;
            var detalle = JsonSerializer.Deserialize<HorometroDetalleRequest>(prop.Value.GetRawText(), JsonOptions);
            if (detalle is null) continue;
            result.Add(new OperacionTalLargoHorometro { Tipo = prop.Name.ToLowerInvariant(), Inicio = detalle.Inicio, Final = detalle.Final, Op = detalle.Op, Inop = detalle.Inop });
        }
        return result;
    }

    private static OperacionTalLargoCondicionEquipo? BuildCondicionEquipo(CondicionEquipoRequest? request)
        => request is null ? null : new OperacionTalLargoCondicionEquipo { Op = request.Op, NoOp = request.NoOp, Lugar = request.Lugar, Descripcion = request.Descripcion, AceiteMotor = request.AceiteMotor, AceiteHidraulico = request.AceiteHidraulico, AceiteTransmision = request.AceiteTransmision, Combustible = request.Combustible, HoraLlenado = ParseHoraNullable(request.HoraLlenado) };

    private static List<OperacionTalLargoControlLlanta> BuildControlLlantas(ControlLlantasRequest? request)
        => request is null ? [] : [new() { Posicion = 1, Estado = request.Numero1 ?? false }, new() { Posicion = 2, Estado = request.Numero2 ?? false }, new() { Posicion = 3, Estado = request.Numero3 ?? false }, new() { Posicion = 4, Estado = request.Numero4 ?? false }];

    private async Task<List<OperacionTalLargoChecklistRespuesta>> BuildChecklistAsync(List<ChecklistRespuestaRequest>? request, CancellationToken ct)
    {
        if (request is null || request.Count == 0) return [];
        var catalog = await db.CheckListItems.AsNoTracking().Where(x => x.Proceso == Proceso).ToListAsync(ct);
        return request.Select(item =>
        {
            var matched = catalog.FirstOrDefault(c => string.Equals(c.Categoria?.Trim(), item.Categoria.Trim(), StringComparison.OrdinalIgnoreCase) && string.Equals(c.Nombre?.Trim(), item.Descripcion.Trim(), StringComparison.OrdinalIgnoreCase));
            return new OperacionTalLargoChecklistRespuesta { ChecklistItemId = matched?.Id, CategoriaSnapshot = item.Categoria, DescripcionSnapshot = item.Descripcion, Decision = item.Decision, Observacion = item.Observacion };
        }).ToList();
    }

    private async Task<List<OperacionTalLargoRegistro>> BuildRegistrosAsync(List<RegistroRequest>? request, CancellationToken ct)
    {
        if (request is null || request.Count == 0) return [];
        var estados = await db.Estados.AsNoTracking().Where(x => x.Proceso == Proceso).ToListAsync(ct);
        var now = DateTime.UtcNow;
        return request.Select(item =>
        {
            var estado = estados.FirstOrDefault(x => string.Equals(x.Codigo, item.Codigo, StringComparison.OrdinalIgnoreCase));
            var esOperativo = string.Equals(item.Estado?.Trim(), "OPERATIVO", StringComparison.OrdinalIgnoreCase);
            return new OperacionTalLargoRegistro
            {
                ExternalId = item.Id,
                Numero = item.Numero,
                EstadoPrincipal = item.Estado ?? string.Empty,
                CodigoEstado = item.Codigo ?? string.Empty,
                EstadoCatalogoId = estado?.Id,
                HoraInicio = ParseHoraRequired(item.HoraInicio, nameof(item.HoraInicio)),
                HoraFinal = ParseHoraRequired(item.HoraFinal, nameof(item.HoraFinal)),
                PayloadOperacion = JsonSerializer.Serialize(item.Operacion, JsonOptions),
                CreatedAt = now,
                UpdatedAt = now,
                Detalle = esOperativo ? new OperacionTalLargoRegistroDetalle { Nivel = item.Operacion.Nivel, TipoLabor = item.Operacion.TipoLabor, Labor = item.Operacion.Labor, Ala = item.Operacion.Ala, NTaladrosProduccion = item.Operacion.NTaladrosProduccion, MetrosPerforadosProduccion = ParseDecimal(item.Operacion.MetrosPerforadosProduccion), NTaladrosRimados = item.Operacion.NTaladrosRimados, MetrosPerforadosRimados = ParseDecimal(item.Operacion.MetrosPerforadosRimados), NTaladrosAlivio = item.Operacion.NTaladrosAlivio, MetrosPerforadosAlivio = ParseDecimal(item.Operacion.MetrosPerforadosAlivio), NTaladrosRepaso = item.Operacion.NTaladrosRepaso, MetrosPerforadosRepaso = ParseDecimal(item.Operacion.MetrosPerforadosRepaso), LongBarras = item.Operacion.LongBarras, NumBarras = item.Operacion.NumBarras, TipoPerforacion = item.Operacion.TipoPerforacion, TipoPerforacionId = item.Operacion.TipoPerforacionId, Observaciones = item.Operacion.Observaciones } : null
            };
        }).ToList();
    }

    private static OperacionDto MapToDto(OperacionTalLargo entity)
    {
        var horometros = entity.Horometros.Count == 0 ? null : ToJsonElement(entity.Horometros.ToDictionary(x => x.Tipo, x => new HorometroDetalleRequest(x.Inicio, x.Final, x.Op, x.Inop)));
        var condicionEquipo = entity.CondicionEquipo is null ? null : ToJsonElement(new CondicionEquipoRequest(entity.CondicionEquipo.Op, entity.CondicionEquipo.NoOp, entity.CondicionEquipo.Lugar, entity.CondicionEquipo.Descripcion, entity.CondicionEquipo.AceiteMotor, entity.CondicionEquipo.AceiteHidraulico, entity.CondicionEquipo.AceiteTransmision, entity.CondicionEquipo.Combustible, entity.CondicionEquipo.HoraLlenado?.ToString("HH:mm:ss", CultureInfo.InvariantCulture)));
        var checklist = entity.ChecklistRespuestas.Count == 0 ? null : ToJsonElement(entity.ChecklistRespuestas.OrderBy(x => x.Id).Select(x => new ChecklistRespuestaRequest(x.DescripcionSnapshot, x.Decision, x.Observacion, x.CategoriaSnapshot)).ToList());
        var controlLlantas = entity.ControlLlantas.Count == 0 ? null : ToJsonElement(new ControlLlantasRequest(entity.ControlLlantas.FirstOrDefault(x => x.Posicion == 1)?.Estado, entity.ControlLlantas.FirstOrDefault(x => x.Posicion == 2)?.Estado, entity.ControlLlantas.FirstOrDefault(x => x.Posicion == 3)?.Estado, entity.ControlLlantas.FirstOrDefault(x => x.Posicion == 4)?.Estado));
        var registros = entity.Registros.Count == 0 ? null : ToJsonElement(entity.Registros.OrderBy(x => x.Id).Select(x => new RegistroRequest(x.ExternalId, x.Numero, x.EstadoPrincipal, x.CodigoEstado, x.HoraInicio.ToString("HH:mm:ss", CultureInfo.InvariantCulture), x.HoraFinal.ToString("HH:mm:ss", CultureInfo.InvariantCulture), x.Detalle is null ? new RegistroOperacionDetalleRequest(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null) : new RegistroOperacionDetalleRequest(x.Detalle.Nivel, x.Detalle.TipoLabor, x.Detalle.Labor, x.Detalle.Ala, null, null, null, null, x.Detalle.LongBarras, x.Detalle.NumBarras, x.Detalle.NTaladrosProduccion, x.Detalle.MetrosPerforadosProduccion?.ToString(CultureInfo.InvariantCulture), x.Detalle.NTaladrosRimados, x.Detalle.MetrosPerforadosRimados?.ToString(CultureInfo.InvariantCulture), x.Detalle.NTaladrosAlivio, x.Detalle.MetrosPerforadosAlivio?.ToString(CultureInfo.InvariantCulture), x.Detalle.NTaladrosRepaso, x.Detalle.MetrosPerforadosRepaso?.ToString(CultureInfo.InvariantCulture), x.Detalle.TipoPerforacion, x.Detalle.TipoPerforacionId, x.Detalle.Observaciones))).ToList());
        return new OperacionDto(entity.Id, TipoOperacion, entity.Fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), entity.Turno, entity.Seccion, entity.Operador, entity.JefeGuardia, entity.EquipoNombre, entity.NEquipo, entity.ModeloEquipo, null, null, null, null, registros, horometros, condicionEquipo, checklist, controlLlantas, entity.Estado, entity.Envio, entity.Revisado, entity.Aprobacion, ParseJson(entity.ObservacionesJefe), ParseJson(entity.ObservacionesJefe2), ParseJson(entity.ObservacionesJefe3));
    }

    private async Task<int?> ResolveSeccionIdAsync(string? seccion, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(seccion)) return null;
        var parts = seccion.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        var proceso = parts.Length == 2 ? parts[0].Trim().ToUpperInvariant() : Proceso;
        var nombre = parts.Length switch { 1 => parts[0].Trim().ToUpperInvariant(), 2 => parts[1].Trim().ToUpperInvariant(), _ => string.Empty };
        if (string.IsNullOrWhiteSpace(nombre)) return null;
        return await db.Secciones.AsNoTracking().Where(x => x.Proceso != null && x.Nombre != null && x.Proceso.ToUpper() == proceso && x.Nombre.ToUpper() == nombre).Select(x => (int?)x.Id).FirstOrDefaultAsync(ct);
    }
}
