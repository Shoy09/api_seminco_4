using Microsoft.AspNetCore.Mvc;
using Seminco.Application.Planes;

namespace Seminco.Api.Controllers;

[Route("api/PlanMensual")]
public sealed class PlanMensualController(IPlanService<PlanMensualDto> service) : PlanController<PlanMensualDto>(service);

[Route("api/PlanMetraje")]
public sealed class PlanMetrajeController(IPlanService<PlanMetrajeDto> service) : PlanController<PlanMetrajeDto>(service);

[Route("api/PlanProduccion")]
public sealed class PlanProduccionController(IPlanService<PlanProduccionDto> service) : PlanController<PlanProduccionDto>(service);
