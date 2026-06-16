using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Seminco.Application.Users;

namespace Seminco.Api.Controllers;

[ApiController]
[Route("api/usuarios")]
public sealed class UsuariosController(UserProfileService profiles) : ControllerBase
{
    [HttpGet("perfil")]
    public async Task<ActionResult<UserProfileResponse>> Perfil(CancellationToken cancellationToken)
    {
        var rawId = User.FindFirstValue("id") ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(rawId, out var userId))
        {
            return Unauthorized(new ProblemDetails { Title = "Unauthorized", Status = StatusCodes.Status401Unauthorized, Detail = "A valid bearer token is required." });
        }

        var profile = await profiles.GetProfileAsync(userId, cancellationToken);
        return profile is null
            ? NotFound(new ProblemDetails { Title = "User not found", Status = StatusCodes.Status404NotFound, Detail = "Usuario no encontrado" })
            : Ok(profile);
    }
}
