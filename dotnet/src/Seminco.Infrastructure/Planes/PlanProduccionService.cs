using System.Text.Json;
using Seminco.Application.Planes;
using Seminco.Domain.Planes;
using Seminco.Infrastructure.Persistence;

namespace Seminco.Infrastructure.Planes;

public sealed class PlanProduccionService(SemincoDbContext db)
    : PlanServiceBase<PlanProduccion, PlanProduccionDto>(db, "columna_")
{
    protected override PlanProduccionDto ToDto(PlanProduccion e)
    {
        var dto = new PlanProduccionDto(
            e.Id, e.Anio, e.Mes,
            e.Semana, e.Mina, e.Zona, e.Area, e.Fase,
            e.MinadoTipo, e.TipoLabor, e.TipoMineral, e.EstructuraVeta,
            e.Nivel, e.Block, e.Labor, e.Ala,
            e.AnchoVeta, e.AnchoMinadoSem, e.AnchoMinadoMes,
            e.AgGr, e.PorcentajeCu, e.PorcentajePb, e.PorcentajeZn,
            e.VptAct, e.VptFinal, e.CutOff1, e.CutOff2,
            e.Programado, e.CreatedAt, e.UpdatedAt
        );
        dto.Columnas = new Dictionary<string, JsonElement>();
        SetDtoColumns(e, dto.Columnas);
        return dto;
    }

    protected override PlanProduccion ToEntity(PlanProduccionDto dto)
    {
        var e = new PlanProduccion
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
            AgGr = dto.AgGr,
            PorcentajeCu = dto.PorcentajeCu,
            PorcentajePb = dto.PorcentajePb,
            PorcentajeZn = dto.PorcentajeZn,
            VptAct = dto.VptAct,
            VptFinal = dto.VptFinal,
            CutOff1 = dto.CutOff1,
            CutOff2 = dto.CutOff2,
            Programado = dto.Programado,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
        SetEntityColumns(e, dto.Columnas);
        return e;
    }

    protected override void ApplyUpdate(PlanProduccion e, PlanProduccionDto dto)
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
        e.AgGr = dto.AgGr;
        e.PorcentajeCu = dto.PorcentajeCu;
        e.PorcentajePb = dto.PorcentajePb;
        e.PorcentajeZn = dto.PorcentajeZn;
        e.VptAct = dto.VptAct;
        e.VptFinal = dto.VptFinal;
        e.CutOff1 = dto.CutOff1;
        e.CutOff2 = dto.CutOff2;
        e.Programado = dto.Programado;
        e.UpdatedAt = DateTime.UtcNow;
        SetEntityColumns(e, dto.Columnas);
    }
}
