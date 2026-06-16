using System.Text.Json;
using Seminco.Application.Planes;
using Seminco.Domain.Planes;
using Seminco.Infrastructure.Persistence;

namespace Seminco.Infrastructure.Planes;

public sealed class PlanMetrajeService(SemincoDbContext db)
    : PlanServiceBase<PlanMetraje, PlanMetrajeDto>(db, "columna_")
{
    protected override PlanMetrajeDto ToDto(PlanMetraje e)
    {
        var dto = new PlanMetrajeDto(
            e.Id, e.Anio, e.Mes,
            e.Semana, e.Mina, e.Zona, e.Area, e.Fase,
            e.MinadoTipo, e.TipoLabor, e.TipoMineral, e.EstructuraVeta,
            e.Nivel, e.Block, e.Labor, e.Ala,
            e.AnchoVeta, e.AnchoMinadoSem, e.AnchoMinadoMes,
            e.Burden, e.Espaciamiento, e.LongitudPerforacion,
            e.Programado, e.CreatedAt, e.UpdatedAt
        );
        dto.Columnas = new Dictionary<string, JsonElement>();
        SetDtoColumns(e, dto.Columnas);
        return dto;
    }

    protected override PlanMetraje ToEntity(PlanMetrajeDto dto)
    {
        var e = new PlanMetraje
        {
            Id = dto.Id,
            Anio = dto.Anio,
            Mes = dto.Mes,
            Semana = dto.Semana,
            Mina = dto.Mina,
            Zona = dto.Zona,
            Area = dto.Area,
            Fase = dto.Fase,
            MinadoTipo = dto.MinadoTipo,
            TipoLabor = dto.TipoLabor,
            TipoMineral = dto.TipoMineral,
            EstructuraVeta = dto.EstructuraVeta,
            Nivel = dto.Nivel,
            Block = dto.Block,
            Labor = dto.Labor,
            Ala = dto.Ala,
            AnchoVeta = dto.AnchoVeta,
            AnchoMinadoSem = dto.AnchoMinadoSem,
            AnchoMinadoMes = dto.AnchoMinadoMes,
            Burden = dto.Burden,
            Espaciamiento = dto.Espaciamiento,
            LongitudPerforacion = dto.LongitudPerforacion,
            Programado = dto.Programado,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
        SetEntityColumns(e, dto.Columnas);
        return e;
    }

    protected override void ApplyUpdate(PlanMetraje e, PlanMetrajeDto dto)
    {
        e.Anio = dto.Anio;
        e.Mes = dto.Mes;
        e.Semana = dto.Semana;
        e.Mina = dto.Mina;
        e.Zona = dto.Zona;
        e.Area = dto.Area;
        e.Fase = dto.Fase;
        e.MinadoTipo = dto.MinadoTipo;
        e.TipoLabor = dto.TipoLabor;
        e.TipoMineral = dto.TipoMineral;
        e.EstructuraVeta = dto.EstructuraVeta;
        e.Nivel = dto.Nivel;
        e.Block = dto.Block;
        e.Labor = dto.Labor;
        e.Ala = dto.Ala;
        e.AnchoVeta = dto.AnchoVeta;
        e.AnchoMinadoSem = dto.AnchoMinadoSem;
        e.AnchoMinadoMes = dto.AnchoMinadoMes;
        e.Burden = dto.Burden;
        e.Espaciamiento = dto.Espaciamiento;
        e.LongitudPerforacion = dto.LongitudPerforacion;
        e.Programado = dto.Programado;
        e.UpdatedAt = DateTime.UtcNow;
        SetEntityColumns(e, dto.Columnas);
    }
}
