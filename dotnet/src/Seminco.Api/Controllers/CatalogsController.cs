using Microsoft.AspNetCore.Mvc;
using Seminco.Application.Catalogs;

namespace Seminco.Api.Controllers;

[Route("api/TipoPerfpo")]
public sealed class TipoPerforacionesController(ICatalogService<TipoPerforacionDto> service) : CatalogController<TipoPerforacionDto>(service);

[Route("api/tipo-equipos")]
public sealed class TipoEquiposController(ICatalogService<TipoEquipoDto> service) : CatalogController<TipoEquipoDto>(service);

[Route("api/check-list")]
public sealed class CheckListItemsController(ICatalogService<CheckListItemDto> service) : CatalogController<CheckListItemDto>(service);

[Route("api/checklists-telemando")]
public sealed class ChecklistsTelemandoController(ICatalogService<ChecklistTelemandoDto> service) : CatalogController<ChecklistTelemandoDto>(service);

[Route("api/longitud-barras")]
public sealed class LongitudBarrasController(ICatalogService<LongitudBarraDto> service) : CatalogController<LongitudBarraDto>(service);

[Route("api/pernos")]
public sealed class PernosController(ICatalogService<PernoDto> service) : CatalogController<PernoDto>(service);

[Route("api/mallas")]
public sealed class MallasController(ICatalogService<MallaDto> service) : CatalogController<MallaDto>(service);

[Route("api/origen-destino")]
public sealed class OrigenDestinosController(ICatalogService<OrigenDestinoDto> service) : CatalogController<OrigenDestinoDto>(service);

[Route("api/Accesorios")]
public sealed class AccesoriosController(ICatalogService<AccesorioDto> service) : CatalogController<AccesorioDto>(service);

[Route("api/Explosivos")]
public sealed class ExplosivosController(ICatalogService<ExplosivoDto> service) : CatalogController<ExplosivoDto>(service);

[Route("api/Explo-uni")]
public sealed class ExplosivosUniController(ICatalogService<ExplosivoUniDto> service) : CatalogController<ExplosivoUniDto>(service);

[Route("api/n-retardos")]
public sealed class NumeroRetardosController(ICatalogService<NumeroRetardoDto> service) : CatalogController<NumeroRetardoDto>(service);
