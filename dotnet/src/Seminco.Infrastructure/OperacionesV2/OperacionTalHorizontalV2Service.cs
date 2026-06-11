using System.Globalization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Seminco.Application.OperacionesV2;
using Seminco.Domain.Catalogs;
using Seminco.Domain.OperacionesV2;
using Seminco.Infrastructure.Persistence;

namespace Seminco.Infrastructure.OperacionesV2;

public sealed class OperacionTalHorizontalV2Service(SemincoDbContext db) : IOperacionTalHorizontalV2Service
{
    private const string ProcesoChecklist = "PERFORACIÓN HORIZONTAL";
    private const string ProcesoEstado = "PERFORACIÓN HORIZONTAL";
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public async Task<OperacionTalHorizontalV2ResponseDto> CreateAsync(JsonElement body, CancellationToken ct)
    {
        var request = Deserialize(body);
        var now = DateTime.UtcNow;

        var entity = new OperacionTalHorizontal
        {
            CreatedAt = now,
            UpdatedAt = now,
        };

        await ApplyRequestAsync(entity, request, body.GetRawText(), ct, isUpdate: false);

        db.OperacionesTalHorizontalV2.Add(entity);
        await db.SaveChangesAsync(ct);

        return new OperacionTalHorizontalV2ResponseDto(entity.Id, "Operación tal_horizontal V2 creada correctamente");
    }

    public async Task<OperacionTalHorizontalV2ResponseDto?> UpdateAsync(int id, JsonElement body, CancellationToken ct)
    {
        var entity = await db.OperacionesTalHorizontalV2
            .Include(x => x.CondicionEquipo)
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

        return new OperacionTalHorizontalV2ResponseDto(entity.Id, "Operación tal_horizontal V2 actualizada correctamente");
    }

    private async Task ApplyRequestAsync(OperacionTalHorizontal entity, OperacionTalHorizontalUpsertRequest request, string rawPayload, CancellationToken ct, bool isUpdate)
    {
        entity.Fecha = ParseFecha(request.Fecha);
        entity.Turno = request.Turno;
        entity.Operador = request.Operador;
        entity.JefeGuardia = request.JefeGuardia;
        entity.EquipoNombre = request.Equipo;
        entity.NEquipo = request.NEquipo;
        entity.Seccion = request.Seccion;
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
        entity.ControlLlantas = BuildControlLlantas(request.ControlLlantas);
        entity.ChecklistRespuestas = await BuildChecklistAsync(request.CheckList, ct);
        entity.Registros = await BuildRegistrosAsync(request.Registros, ct);
    }

    private static OperacionTalHorizontalUpsertRequest Deserialize(JsonElement body) =>
        JsonSerializer.Deserialize<OperacionTalHorizontalUpsertRequest>(body.GetRawText(), JsonOptions)
        ?? throw new InvalidOperationException("No se pudo deserializar la operación tal_horizontal V2");

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

    private static decimal? ParseDecimal(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return null;
        return decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsed)
            ? parsed
            : null;
    }

    private static List<OperacionTalHorizontalHorometro> BuildHorometros(HorometrosRequest? request)
    {
        var result = new List<OperacionTalHorizontalHorometro>();
        AddIfPresent("diesel", request?.Diesel);
        AddIfPresent("electrico", request?.Electrico);
        AddIfPresent("percusion", request?.Percusion);
        return result;

        void AddIfPresent(string tipo, HorometroDetalleRequest? detalle)
        {
            if (detalle is null) return;
            result.Add(new OperacionTalHorizontalHorometro
            {
                Tipo = tipo,
                Inicio = detalle.Inicio,
                Final = detalle.Final,
                Op = detalle.Op,
                Inop = detalle.Inop,
            });
        }
    }

    private static OperacionTalHorizontalCondicionEquipo? BuildCondicionEquipo(CondicionEquipoRequest? request)
    {
        if (request is null) return null;
        return new OperacionTalHorizontalCondicionEquipo
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

    private static List<OperacionTalHorizontalControlLlanta> BuildControlLlantas(ControlLlantasRequest? request)
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

    private async Task<List<OperacionTalHorizontalChecklistRespuesta>> BuildChecklistAsync(List<ChecklistRespuestaRequest>? request, CancellationToken ct)
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

            return new OperacionTalHorizontalChecklistRespuesta
            {
                ChecklistItemId = matched?.Id,
                CategoriaSnapshot = item.Categoria,
                DescripcionSnapshot = item.Descripcion,
                Decision = item.Decision,
                Observacion = item.Observacion,
            };
        }).ToList();
    }

    private async Task<List<OperacionTalHorizontalRegistro>> BuildRegistrosAsync(List<RegistroRequest>? request, CancellationToken ct)
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
            var esOperativo = string.Equals(item.Estado?.Trim(), "OPERATIVO", StringComparison.OrdinalIgnoreCase);

            return new OperacionTalHorizontalRegistro
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
                Detalle = esOperativo
                    ? new OperacionTalHorizontalRegistroDetalle
                    {
                        Nivel = item.Operacion.Nivel,
                        TipoLabor = item.Operacion.TipoLabor,
                        Labor = item.Operacion.Labor,
                        Ala = item.Operacion.Ala,
                        TalProd = ParseDecimal(item.Operacion.TalProd),
                        TalRimados = ParseDecimal(item.Operacion.TalRimados),
                        TalAlivio = ParseDecimal(item.Operacion.TalAlivio),
                        TalRepaso = ParseDecimal(item.Operacion.TalRepaso),
                        LongBarras = ParseDecimal(item.Operacion.LongBarras),
                        NumBarras = ParseDecimal(item.Operacion.NumBarras),
                        TipoPerforacion = item.Operacion.TipoPerforacion,
                        TipoPerforacionId = item.Operacion.TipoPerforacionId,
                        Observaciones = item.Operacion.Observaciones,
                    }
                    : null
            };
        }).ToList();
    }
}
