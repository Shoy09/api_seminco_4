using Microsoft.EntityFrameworkCore;
using Seminco.Application.Catalogs;
using Seminco.Domain.Catalogs;
using Seminco.Infrastructure.Persistence;

namespace Seminco.Infrastructure.Catalogs.Services;

public sealed class EquipoService(SemincoDbContext db) : CatalogService<Equipo, EquipoDto>(db)
{
    protected override EquipoDto ToDto(Equipo e) => new(e.Id, e.Nombre, e.Proceso, e.Codigo, e.Marca, e.Modelo, e.Serie, e.AnioFabricacion, e.FechaIngreso, e.CapacidadYd3, e.CapacidadM3);
    protected override Equipo ToEntity(EquipoDto dto) => new() { Nombre = dto.Nombre, Proceso = dto.Proceso, Codigo = dto.Codigo, Marca = dto.Marca, Modelo = dto.Modelo, Serie = dto.Serie, AnioFabricacion = dto.AnioFabricacion, FechaIngreso = dto.FechaIngreso, CapacidadYd3 = dto.CapacidadYd3, CapacidadM3 = dto.CapacidadM3 };
    protected override void ApplyUpdate(Equipo entity, EquipoDto dto) { entity.Nombre = dto.Nombre; entity.Proceso = dto.Proceso; entity.Codigo = dto.Codigo; entity.Marca = dto.Marca; entity.Modelo = dto.Modelo; entity.Serie = dto.Serie; entity.AnioFabricacion = dto.AnioFabricacion; entity.FechaIngreso = dto.FechaIngreso; entity.CapacidadYd3 = dto.CapacidadYd3; entity.CapacidadM3 = dto.CapacidadM3; }
}

public sealed class EstadoService(SemincoDbContext db) : CatalogService<Estado, EstadoDto>(db)
{
    protected override EstadoDto ToDto(Estado e) => new(e.Id, e.EstadoPrincipal, e.Codigo, e.TipoEstado, e.Categoria, e.Proceso);
    protected override Estado ToEntity(EstadoDto dto) => new() { EstadoPrincipal = dto.EstadoPrincipal, Codigo = dto.Codigo, TipoEstado = dto.TipoEstado, Categoria = dto.Categoria, Proceso = dto.Proceso };
    protected override void ApplyUpdate(Estado entity, EstadoDto dto) { entity.EstadoPrincipal = dto.EstadoPrincipal; entity.Codigo = dto.Codigo; entity.TipoEstado = dto.TipoEstado; entity.Categoria = dto.Categoria; entity.Proceso = dto.Proceso; }
}

public sealed class TipoPerforacionService(SemincoDbContext db) : CatalogService<TipoPerforacion, TipoPerforacionDto>(db)
{
    protected override TipoPerforacionDto ToDto(TipoPerforacion e) => new(e.Id, e.Nombre, e.Proceso, e.PermitidoMedicion);
    protected override TipoPerforacion ToEntity(TipoPerforacionDto dto) => new() { Nombre = dto.Nombre, Proceso = dto.Proceso, PermitidoMedicion = dto.PermitidoMedicion };
    protected override void ApplyUpdate(TipoPerforacion entity, TipoPerforacionDto dto) { entity.Nombre = dto.Nombre; entity.Proceso = dto.Proceso; entity.PermitidoMedicion = dto.PermitidoMedicion; }
    public override async Task<List<TipoPerforacionDto>> GetByProcesoAsync(string proceso, CancellationToken ct)
    {
        var entities = await Db.Set<TipoPerforacion>().AsNoTracking()
            .Where(e => e.Proceso == proceso)
            .ToListAsync(ct);
        return entities.Select(ToDto).ToList();
    }
}

public sealed class TipoEquipoService(SemincoDbContext db) : CatalogService<TipoEquipo, TipoEquipoDto>(db)
{
    protected override TipoEquipoDto ToDto(TipoEquipo e) => new(e.Id, e.Nombre);
    protected override TipoEquipo ToEntity(TipoEquipoDto dto) => new() { Nombre = dto.Nombre };
    protected override void ApplyUpdate(TipoEquipo entity, TipoEquipoDto dto) { entity.Nombre = dto.Nombre; }
}

public sealed class CheckListItemService(SemincoDbContext db) : CatalogService<CheckListItem, CheckListItemDto>(db)
{
    protected override CheckListItemDto ToDto(CheckListItem e) => new(e.Id, e.Proceso, e.Categoria, e.Nombre);
    protected override CheckListItem ToEntity(CheckListItemDto dto) => new() { Proceso = dto.Proceso, Categoria = dto.Categoria, Nombre = dto.Nombre };
    protected override void ApplyUpdate(CheckListItem entity, CheckListItemDto dto) { entity.Proceso = dto.Proceso; entity.Categoria = dto.Categoria; entity.Nombre = dto.Nombre; }
}

public sealed class ChecklistTelemandoService(SemincoDbContext db) : CatalogService<ChecklistTelemando, ChecklistTelemandoDto>(db)
{
    protected override ChecklistTelemandoDto ToDto(ChecklistTelemando e) => new(e.Id, e.Nombre);
    protected override ChecklistTelemando ToEntity(ChecklistTelemandoDto dto) => new() { Nombre = dto.Nombre };
    protected override void ApplyUpdate(ChecklistTelemando entity, ChecklistTelemandoDto dto) { entity.Nombre = dto.Nombre; }
}

public sealed class SeccionService(SemincoDbContext db) : CatalogService<Seccion, SeccionDto>(db)
{
    protected override SeccionDto ToDto(Seccion e) => new(e.Id, e.Proceso, e.Nombre);
    protected override Seccion ToEntity(SeccionDto dto) => new() { Proceso = dto.Proceso, Nombre = dto.Nombre };
    protected override void ApplyUpdate(Seccion entity, SeccionDto dto) { entity.Proceso = dto.Proceso; entity.Nombre = dto.Nombre; }
}

public sealed class LongitudBarraService(SemincoDbContext db) : CatalogService<LongitudBarra, LongitudBarraDto>(db)
{
    protected override LongitudBarraDto ToDto(LongitudBarra e) => new(e.Id, e.Proceso, e.LongitudPies);
    protected override LongitudBarra ToEntity(LongitudBarraDto dto) => new() { Proceso = dto.Proceso, LongitudPies = dto.LongitudPies };
    protected override void ApplyUpdate(LongitudBarra entity, LongitudBarraDto dto) { entity.Proceso = dto.Proceso; entity.LongitudPies = dto.LongitudPies; }
}

public sealed class PernoService(SemincoDbContext db) : CatalogService<Perno, PernoDto>(db)
{
    protected override PernoDto ToDto(Perno e) => new(e.Id, e.TipoPerno, e.Longitud);
    protected override Perno ToEntity(PernoDto dto) => new() { TipoPerno = dto.TipoPerno, Longitud = dto.Longitud };
    protected override void ApplyUpdate(Perno entity, PernoDto dto) { entity.TipoPerno = dto.TipoPerno; entity.Longitud = dto.Longitud; }
}

public sealed class MallaService(SemincoDbContext db) : CatalogService<Malla, MallaDto>(db)
{
    protected override MallaDto ToDto(Malla e) => new(e.Id, e.TipoMalla);
    protected override Malla ToEntity(MallaDto dto) => new() { TipoMalla = dto.TipoMalla };
    protected override void ApplyUpdate(Malla entity, MallaDto dto) { entity.TipoMalla = dto.TipoMalla; }
}

public sealed class OrigenDestinoService(SemincoDbContext db) : CatalogService<OrigenDestino, OrigenDestinoDto>(db)
{
    protected override OrigenDestinoDto ToDto(OrigenDestino e) => new(e.Id, e.Proceso, e.Tipo, e.Nombre);
    protected override OrigenDestino ToEntity(OrigenDestinoDto dto) => new() { Proceso = dto.Proceso, Tipo = dto.Tipo, Nombre = dto.Nombre };
    protected override void ApplyUpdate(OrigenDestino entity, OrigenDestinoDto dto) { entity.Proceso = dto.Proceso; entity.Tipo = dto.Tipo; entity.Nombre = dto.Nombre; }
}

public sealed class AccesorioService(SemincoDbContext db) : CatalogService<Accesorio, AccesorioDto>(db)
{
    protected override AccesorioDto ToDto(Accesorio e) => new(e.Id, e.Codigo, e.TipoAccesorio, e.Costo, e.UnidadMedida);
    protected override Accesorio ToEntity(AccesorioDto dto) => new() { Codigo = dto.Codigo, TipoAccesorio = dto.TipoAccesorio, Costo = dto.Costo, UnidadMedida = dto.UnidadMedida };
    protected override void ApplyUpdate(Accesorio entity, AccesorioDto dto) { entity.Codigo = dto.Codigo; entity.TipoAccesorio = dto.TipoAccesorio; entity.Costo = dto.Costo; entity.UnidadMedida = dto.UnidadMedida; }
}

public sealed class ExplosivoService(SemincoDbContext db) : CatalogService<Explosivo, ExplosivoDto>(db)
{
    protected override ExplosivoDto ToDto(Explosivo e) => new(e.Id, e.Codigo, e.TipoExplosivo, e.CantidadPorCaja, e.PesoUnitario, e.CostoPorKg, e.UnidadMedida);
    protected override Explosivo ToEntity(ExplosivoDto dto) => new() { Codigo = dto.Codigo, TipoExplosivo = dto.TipoExplosivo, CantidadPorCaja = dto.CantidadPorCaja, PesoUnitario = dto.PesoUnitario, CostoPorKg = dto.CostoPorKg, UnidadMedida = dto.UnidadMedida };
    protected override void ApplyUpdate(Explosivo entity, ExplosivoDto dto) { entity.Codigo = dto.Codigo; entity.TipoExplosivo = dto.TipoExplosivo; entity.CantidadPorCaja = dto.CantidadPorCaja; entity.PesoUnitario = dto.PesoUnitario; entity.CostoPorKg = dto.CostoPorKg; entity.UnidadMedida = dto.UnidadMedida; }
}

public sealed class ExplosivoUniService(SemincoDbContext db) : CatalogService<ExplosivoUni, ExplosivoUniDto>(db)
{
    protected override ExplosivoUniDto ToDto(ExplosivoUni e) => new(e.Id, e.Dato, e.Tipo);
    protected override ExplosivoUni ToEntity(ExplosivoUniDto dto) => new() { Dato = dto.Dato, Tipo = dto.Tipo };
    protected override void ApplyUpdate(ExplosivoUni entity, ExplosivoUniDto dto) { entity.Dato = dto.Dato; entity.Tipo = dto.Tipo; }
}

public sealed class NumeroRetardoService(SemincoDbContext db) : CatalogService<NumeroRetardo, NumeroRetardoDto>(db)
{
    protected override NumeroRetardoDto ToDto(NumeroRetardo e) => new(e.Id, e.Longitud, e.Tipo, e.Codigo);
    protected override NumeroRetardo ToEntity(NumeroRetardoDto dto) => new() { Longitud = dto.Longitud, Tipo = dto.Tipo, Codigo = dto.Codigo };
    protected override void ApplyUpdate(NumeroRetardo entity, NumeroRetardoDto dto) { entity.Longitud = dto.Longitud; entity.Tipo = dto.Tipo; entity.Codigo = dto.Codigo; }
}
