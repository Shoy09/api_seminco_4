using System.Globalization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Seminco.Application.Operaciones;
using Seminco.Application.OperacionesV2;
using Seminco.Domain.OperacionesV2;
using Seminco.Infrastructure.Persistence;

namespace Seminco.Infrastructure.OperacionesV2;

public sealed class OperacionCarguioV2Service(SemincoDbContext db) : IOperacionCarguioV2Service
{
    private const string TipoOperacion = "carguio";
    private const string ProcesoChecklist = "SCOOPTRAM";
    private const string ProcesoEstado = "SCOOPTRAM";
    private const string ProcesoSeccion = "SCOOPTRAM";
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    private IQueryable<Domain.OperacionesV2.OperacionCarguio> ReadQuery() => db.OperacionesCarguioV2
        .AsNoTracking()
        .AsSplitQuery()
        .Include(x => x.CondicionEquipo)
        .Include(x => x.ProgramaTrabajo)
        .Include(x => x.Horometros)
        .Include(x => x.ChecklistRespuestas)
        .Include(x => x.ControlLlantas)
        .Include(x => x.Registros.OrderBy(r => r.Id))
            .ThenInclude(x => x.Detalle);

    public async Task<List<OperacionDto>> GetAllAsync(string? estado, string? envio, CancellationToken ct)
    {
        var query = ReadQuery();

        if (estado is not null) query = query.Where(x => x.Estado == estado);
        if (envio is not null) query = query.Where(x => x.Envio == int.Parse(envio));

        var entities = await query
            .OrderByDescending(x => x.Id)
            .ToListAsync(ct);

        return entities.Select(MapToDto).ToList();
    }

    public async Task<List<OperacionDto>> GetByAprobacionAsync(string? estado, string? envio, CancellationToken ct)
    {
        var query = ReadQuery()
            .Where(x => x.Aprobacion == 0 || x.Aprobacion == 1);

        if (estado is not null) query = query.Where(x => x.Estado == estado);
        if (envio is not null) query = query.Where(x => x.Envio == int.Parse(envio));

        var entities = await query
            .OrderByDescending(x => x.Id)
            .ToListAsync(ct);

        return entities.Select(MapToDto).ToList();
    }

    public async Task<OperacionDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        var entity = await ReadQuery().FirstOrDefaultAsync(x => x.Id == id, ct);
        return entity is null ? null : MapToDto(entity);
    }

    public async Task<List<OperacionDto>> GetByJefeAsync(string jefeGuardia, int limit, int offset, CancellationToken ct)
    {
        var entities = await ReadQuery()
            .Where(x => x.JefeGuardia == jefeGuardia)
            .OrderByDescending(x => x.Id)
            .Skip(offset)
            .Take(limit)
            .ToListAsync(ct);

        return entities.Select(MapToDto).ToList();
    }

    public async Task<OperacionCarguioV2ResponseDto> CreateAsync(JsonElement body, CancellationToken ct)
    {
        var request = Deserialize(body);
        var now = DateTime.UtcNow;

        var entity = new Domain.OperacionesV2.OperacionCarguio
        {
            CreatedAt = now,
            UpdatedAt = now,
        };

        await ApplyRequestAsync(entity, request, body.GetRawText(), ct, isUpdate: false);

        db.OperacionesCarguioV2.Add(entity);
        await db.SaveChangesAsync(ct);

        return new OperacionCarguioV2ResponseDto(entity.Id, "Operación carguio V2 creada correctamente");
    }

    public async Task<OperacionCarguioV2ResponseDto?> UpdateAsync(int id, JsonElement body, CancellationToken ct)
    {
        var entity = await db.OperacionesCarguioV2
            .Include(x => x.CondicionEquipo)
            .Include(x => x.ProgramaTrabajo)
            .Include(x => x.Horometros)
            .Include(x => x.ChecklistRespuestas)
            .Include(x => x.ControlLlantas)
            .Include(x => x.Registros)
                .ThenInclude(x => x.Detalle)
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (entity is null) return null;

        var request = Deserialize(body);
        await ApplyRequestAsync(entity, request, body.GetRawText(), ct, isUpdate: true);
        await db.SaveChangesAsync(ct);

        return new OperacionCarguioV2ResponseDto(entity.Id, "Operación carguio V2 actualizada correctamente");
    }

    private async Task ApplyRequestAsync(Domain.OperacionesV2.OperacionCarguio entity, OperacionCarguioUpsertRequest request, string rawPayload, CancellationToken ct, bool isUpdate)
    {
        entity.Fecha = ParseFecha(request.Fecha);
        entity.Turno = request.Turno;
        entity.Operador = request.Operador;
        entity.JefeGuardia = request.JefeGuardia;
        entity.EquipoNombre = request.Equipo;
        entity.NEquipo = request.NEquipo;
        entity.Seccion = request.Seccion;
        entity.SeccionId = await ResolveSeccionIdAsync(request.Seccion, ct);
        entity.Capacidad = request.Capacidad;
        entity.TipoEquipoDiesel = request.TipoEquipo?.Diesel;
        entity.TipoEquipoElectrico = request.TipoEquipo?.Electrico;
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
            if (entity.ProgramaTrabajo is not null) db.Remove(entity.ProgramaTrabajo);
            if (entity.Horometros.Count > 0) db.RemoveRange(entity.Horometros);
            if (entity.ChecklistRespuestas.Count > 0) db.RemoveRange(entity.ChecklistRespuestas);
            if (entity.ControlLlantas.Count > 0) db.RemoveRange(entity.ControlLlantas);
            if (entity.Registros.Count > 0) db.RemoveRange(entity.Registros);
            entity.CondicionEquipo = null;
            entity.ProgramaTrabajo = null;
            entity.Horometros.Clear();
            entity.ChecklistRespuestas.Clear();
            entity.ControlLlantas.Clear();
            entity.Registros.Clear();
        }

        entity.Horometros = BuildHorometros(request.Horometros);
        entity.CondicionEquipo = BuildCondicionEquipo(request.CondicionesEquipo);
        entity.ControlLlantas = BuildControlLlantas(request.ControlLlantas);
        entity.ProgramaTrabajo = BuildProgramaTrabajo(request.ProgramaTrabajo);
        entity.ChecklistRespuestas = await BuildChecklistAsync(request.CheckList, ct);
        entity.Registros = await BuildRegistrosAsync(request.Registros, ct);
    }

    private static OperacionCarguioUpsertRequest Deserialize(JsonElement body) =>
        JsonSerializer.Deserialize<OperacionCarguioUpsertRequest>(body.GetRawText(), JsonOptions)
        ?? throw new InvalidOperationException("No se pudo deserializar la operación carguio V2");

    private static JsonElement? ParseJson(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return null;
        try { return JsonSerializer.Deserialize<JsonElement>(value); }
        catch { return JsonDocument.Parse($"\"{value}\"").RootElement.Clone(); }
    }

    private static JsonElement? ToJsonElement<T>(T value)
    {
        if (value is null) return null;
        return JsonSerializer.SerializeToElement(value, JsonOptions);
    }

    private static OperacionCarguioRegistroDetalleRequest EmptyRegistroOperacion() =>
        new(null, null, null, null, null, null, null);

    private static OperacionCarguioRegistroDetalleRequest BuildRegistroOperacion(Domain.OperacionesV2.OperacionCarguioRegistro registro)
    {
        if (registro.Detalle is not null)
        {
            return new OperacionCarguioRegistroDetalleRequest(
                registro.Detalle.NivelInicio,
                registro.Detalle.TipoLaborInicio,
                registro.Detalle.LaborInicio,
                registro.Detalle.AlaInicio,
                registro.Detalle.UbicacionDestino,
                registro.Detalle.NCucharas,
                registro.Detalle.Observaciones);
        }

        if (!string.IsNullOrWhiteSpace(registro.PayloadOperacion))
        {
            try
            {
                return JsonSerializer.Deserialize<OperacionCarguioRegistroDetalleRequest>(registro.PayloadOperacion, JsonOptions)
                    ?? EmptyRegistroOperacion();
            }
            catch
            {
            }
        }

        return EmptyRegistroOperacion();
    }

    private static OperacionDto MapToDto(Domain.OperacionesV2.OperacionCarguio entity)
    {
        var horometros = entity.Horometros.Count == 0
            ? null
            : ToJsonElement(new OperacionCarguioHorometrosRequest(
                entity.Horometros
                    .Where(x => string.Equals(x.Tipo, "horometro", StringComparison.OrdinalIgnoreCase))
                    .Select(x => new OperacionCarguioHorometroDetalleRequest(x.Inicio, x.Final, x.Op, x.Inop))
                    .FirstOrDefault()));

        var condicionEquipo = entity.CondicionEquipo is null
            ? null
            : ToJsonElement(new OperacionCarguioCondicionEquipoRequest(
                entity.CondicionEquipo.Op,
                entity.CondicionEquipo.NoOp,
                entity.CondicionEquipo.Lugar,
                entity.CondicionEquipo.Descripcion,
                entity.CondicionEquipo.AceiteMotor,
                entity.CondicionEquipo.AceiteHidraulico,
                entity.CondicionEquipo.AceiteTransmision,
                entity.CondicionEquipo.Combustible,
                entity.CondicionEquipo.HoraLlenado?.ToString("HH:mm:ss", CultureInfo.InvariantCulture)));

        var checklist = entity.ChecklistRespuestas.Count == 0
            ? null
            : ToJsonElement(entity.ChecklistRespuestas
                .OrderBy(x => x.Id)
                .Select(x => new OperacionCarguioChecklistRespuestaRequest(
                    x.DescripcionSnapshot,
                    x.Decision,
                    x.Observacion,
                    x.CategoriaSnapshot))
                .ToList());

        var controlLlantas = entity.ControlLlantas.Count == 0
            ? null
            : ToJsonElement(new OperacionCarguioControlLlantasRequest(
                entity.ControlLlantas.FirstOrDefault(x => x.Posicion == 1)?.Estado,
                entity.ControlLlantas.FirstOrDefault(x => x.Posicion == 2)?.Estado,
                entity.ControlLlantas.FirstOrDefault(x => x.Posicion == 3)?.Estado,
                entity.ControlLlantas.FirstOrDefault(x => x.Posicion == 4)?.Estado));

        var programaTrabajo = entity.ProgramaTrabajo is null
            ? null
            : ToJsonElement(new OperacionCarguioProgramaTrabajoRequest(
                entity.ProgramaTrabajo.NCucharasProgramado,
                entity.ProgramaTrabajo.NCucharasRealizado));

        var registros = entity.Registros.Count == 0
            ? null
            : ToJsonElement(entity.Registros
                .OrderBy(x => x.Id)
                .Select(x => new OperacionCarguioRegistroRequest(
                    x.ExternalId,
                    x.Numero,
                    x.EstadoPrincipal,
                    x.CodigoEstado,
                    x.HoraInicio.ToString("HH:mm:ss", CultureInfo.InvariantCulture),
                    x.HoraFinal.ToString("HH:mm:ss", CultureInfo.InvariantCulture),
                    BuildRegistroOperacion(x)))
                .ToList());

        var tipoEquipo = entity.TipoEquipoDiesel is null && entity.TipoEquipoElectrico is null
            ? null
            : JsonSerializer.Serialize(new OperacionCarguioTipoEquipoRequest(
                entity.TipoEquipoDiesel ?? false,
                entity.TipoEquipoElectrico ?? false));

        return new OperacionDto(
            entity.Id,
            TipoOperacion,
            entity.Fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            entity.Turno,
            entity.Seccion,
            entity.Operador,
            entity.JefeGuardia,
            entity.EquipoNombre,
            entity.NEquipo,
            null,
            entity.Capacidad,
            tipoEquipo,
            programaTrabajo,
            null,
            registros,
            horometros,
            condicionEquipo,
            checklist,
            controlLlantas,
            entity.Estado,
            entity.Envio,
            entity.Revisado,
            entity.Aprobacion,
            ParseJson(entity.ObservacionesJefe),
            ParseJson(entity.ObservacionesJefe2),
            ParseJson(entity.ObservacionesJefe3));
    }

    private async Task<int?> ResolveSeccionIdAsync(string? seccion, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(seccion)) return null;

        var parts = seccion.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        var proceso = parts.Length == 2 ? parts[0].Trim().ToUpperInvariant() : ProcesoSeccion;
        var nombre = parts.Length switch
        {
            1 => parts[0].Trim().ToUpperInvariant(),
            2 => parts[1].Trim().ToUpperInvariant(),
            _ => string.Empty
        };

        if (string.IsNullOrWhiteSpace(nombre)) return null;

        var matched = await db.Secciones
            .AsNoTracking()
            .Where(x => x.Proceso != null && x.Nombre != null
                && x.Proceso.ToUpper() == proceso
                && x.Nombre.ToUpper() == nombre)
            .Select(x => (int?)x.Id)
            .FirstOrDefaultAsync(ct);

        return matched;
    }

    private static DateTime ParseFecha(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException("El campo fecha es obligatorio");

        if (DateTime.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var exact))
            return DateTime.SpecifyKind(exact, DateTimeKind.Utc);
        if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var parsed))
            return parsed.ToUniversalTime();

        throw new InvalidOperationException($"Fecha inválida: {value}");
    }

    private static TimeOnly? ParseHoraNullable(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return null;
        return TimeOnly.TryParse(value, CultureInfo.InvariantCulture, out var time)
            ? time
            : null;
    }

    private static TimeOnly ParseHoraRequired(string value, string fieldName)
    {
        if (TimeOnly.TryParse(value, CultureInfo.InvariantCulture, out var time))
            return time;
        throw new InvalidOperationException($"Hora inválida en {fieldName}: {value}");
    }

    private static List<OperacionCarguioHorometro> BuildHorometros(OperacionCarguioHorometrosRequest? request)
    {
        var detalle = request?.Horometro;
        if (detalle is null) return [];

        return
        [
            new OperacionCarguioHorometro
            {
                Tipo = "horometro",
                Inicio = detalle.Inicio,
                Final = detalle.Final,
                Op = detalle.Op,
                Inop = detalle.Inop,
            }
        ];
    }

    private static OperacionCarguioCondicionEquipo? BuildCondicionEquipo(OperacionCarguioCondicionEquipoRequest? request)
    {
        if (request is null) return null;
        return new OperacionCarguioCondicionEquipo
        {
            Op = request.Op,
            NoOp = request.NoOp,
            Lugar = request.Lugar,
            Descripcion = request.Descripcion,
            AceiteMotor = request.AceiteMotor,
            AceiteHidraulico = request.AceiteHidraulico,
            AceiteTransmision = request.AceiteTransmision,
            Combustible = request.Combustible,
            HoraLlenado = ParseHoraNullable(request.HoraLlenado),
        };
    }

    private static OperacionCarguioProgramaTrabajo? BuildProgramaTrabajo(OperacionCarguioProgramaTrabajoRequest? request)
    {
        if (request is null) return null;

        return new OperacionCarguioProgramaTrabajo
        {
            NCucharasProgramado = request.NCucharasProgramado,
            NCucharasRealizado = request.NCucharasRealizado,
        };
    }

    private static List<OperacionCarguioControlLlanta> BuildControlLlantas(OperacionCarguioControlLlantasRequest? request)
    {
        if (request is null) return [];
        return
        [
            new() { Posicion = 1, Estado = request.Numero1 ?? false },
            new() { Posicion = 2, Estado = request.Numero2 ?? false },
            new() { Posicion = 3, Estado = request.Numero3 ?? false },
            new() { Posicion = 4, Estado = request.Numero4 ?? false },
        ];
    }

    private async Task<List<OperacionCarguioChecklistRespuesta>> BuildChecklistAsync(List<OperacionCarguioChecklistRespuestaRequest>? request, CancellationToken ct)
    {
        if (request is null || request.Count == 0) return [];

        var catalog = await db.CheckListItems.AsNoTracking()
            .Where(x => x.Proceso == ProcesoChecklist)
            .ToListAsync(ct);

        return request.Select(item =>
        {
            var matched = catalog.FirstOrDefault(c =>
                string.Equals(c.Categoria?.Trim(), item.Categoria.Trim(), StringComparison.OrdinalIgnoreCase) &&
                string.Equals(c.Nombre?.Trim(), item.Descripcion.Trim(), StringComparison.OrdinalIgnoreCase));

            return new OperacionCarguioChecklistRespuesta
            {
                ChecklistItemId = matched?.Id,
                CategoriaSnapshot = item.Categoria,
                DescripcionSnapshot = item.Descripcion,
                Decision = item.Decision,
                Observacion = item.Observacion,
            };
        }).ToList();
    }

    private async Task<List<OperacionCarguioRegistro>> BuildRegistrosAsync(List<OperacionCarguioRegistroRequest>? request, CancellationToken ct)
    {
        if (request is null || request.Count == 0) return [];

        var estados = await db.Estados.AsNoTracking()
            .Where(x => x.Proceso == ProcesoEstado)
            .ToListAsync(ct);
        var now = DateTime.UtcNow;

        return request.Select(item =>
        {
            var estado = estados.FirstOrDefault(x =>
                string.Equals(x.Codigo, item.Codigo, StringComparison.OrdinalIgnoreCase));
            var detalle = item.Operacion ?? EmptyRegistroOperacion();
            var esOperativo = string.Equals(item.Estado?.Trim(), "OPERATIVO", StringComparison.OrdinalIgnoreCase);

            return new OperacionCarguioRegistro
            {
                ExternalId = item.Id,
                Numero = item.Numero,
                EstadoPrincipal = item.Estado ?? string.Empty,
                CodigoEstado = item.Codigo ?? string.Empty,
                EstadoCatalogoId = estado?.Id,
                HoraInicio = ParseHoraRequired(item.HoraInicio, nameof(item.HoraInicio)),
                HoraFinal = ParseHoraRequired(item.HoraFinal, nameof(item.HoraFinal)),
                PayloadOperacion = JsonSerializer.Serialize(detalle, JsonOptions),
                CreatedAt = now,
                UpdatedAt = now,
                Detalle = esOperativo
                    ? new OperacionCarguioRegistroDetalle
                    {
                        NivelInicio = detalle.NivelInicio,
                        TipoLaborInicio = detalle.TipoLaborInicio,
                        LaborInicio = detalle.LaborInicio,
                        AlaInicio = detalle.AlaInicio,
                        UbicacionDestino = detalle.UbicacionDestino,
                        NCucharas = detalle.NCucharas,
                        Observaciones = detalle.Observaciones,
                    }
                    : null
            };
        }).ToList();
    }
}
