using System.Text.Json;
using Seminco.Application.Planes;
using Seminco.Domain.Planes;
using Seminco.Infrastructure.Persistence;

namespace Seminco.Infrastructure.Planes;

public sealed class PlanMensualService(SemincoDbContext db)
    : PlanServiceBase<PlanMensual, PlanMensualDto>(db, "col_")
{
    protected override PlanMensualDto ToDto(PlanMensual e)
    {
        var dto = new PlanMensualDto(
            e.Id, e.Anio, e.Mes,
            e.MinadoTipo, e.Empresa, e.Zona, e.Area, e.TipoMineral,
            e.Fase, e.EstructuraVeta, e.Nivel, e.TipoLabor, e.Labor, e.Ala,
            e.AvanceM, e.AnchoM, e.AltoM, e.Tms,
            e.Programado, e.CreatedAt, e.UpdatedAt
        );
        dto.Columnas = new Dictionary<string, JsonElement>();
        SetDtoColumns(e, dto.Columnas);
        return dto;
    }

    protected override PlanMensual ToEntity(PlanMensualDto dto)
    {
        var e = new PlanMensual
        {
            Id = dto.Id,
            Anio = dto.Anio,
            Mes = dto.Mes,
            MinadoTipo = dto.MinadoTipo,
            Empresa = dto.Empresa,
            Zona = dto.Zona,
            Area = dto.Area,
            TipoMineral = dto.TipoMineral,
            Fase = dto.Fase,
            EstructuraVeta = dto.EstructuraVeta,
            Nivel = dto.Nivel,
            TipoLabor = dto.TipoLabor,
            Labor = dto.Labor,
            Ala = dto.Ala,
            AvanceM = dto.AvanceM,
            AnchoM = dto.AnchoM,
            AltoM = dto.AltoM,
            Tms = dto.Tms,
            Programado = dto.Programado,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
        SetEntityColumns(e, dto.Columnas);
        return e;
    }

    protected override void ApplyUpdate(PlanMensual e, PlanMensualDto dto)
    {
        e.Anio = dto.Anio;
        e.Mes = dto.Mes;
        e.MinadoTipo = dto.MinadoTipo;
        e.Empresa = dto.Empresa;
        e.Zona = dto.Zona;
        e.Area = dto.Area;
        e.TipoMineral = dto.TipoMineral;
        e.Fase = dto.Fase;
        e.EstructuraVeta = dto.EstructuraVeta;
        e.Nivel = dto.Nivel;
        e.TipoLabor = dto.TipoLabor;
        e.Labor = dto.Labor;
        e.Ala = dto.Ala;
        e.AvanceM = dto.AvanceM;
        e.AnchoM = dto.AnchoM;
        e.AltoM = dto.AltoM;
        e.Tms = dto.Tms;
        e.Programado = dto.Programado;
        e.UpdatedAt = DateTime.UtcNow;
        SetEntityColumns(e, dto.Columnas);
    }
}
