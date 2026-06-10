using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Seminco.Application.Auth;

namespace Seminco.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(LoginService login, RegisterService register) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var response = await login.LoginAsync(request, cancellationToken);
        return response is null
            ? Unauthorized(new ProblemDetails { Title = "Invalid credentials", Status = StatusCodes.Status401Unauthorized, Detail = "Credenciales incorrectas" })
            : Ok(response);
    }
    [HttpPost("register")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<RegisterResponse>> Register(
        RegisterRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await register.RegisterAsync(request, cancellationToken);
            return Ok(response);
        }
        catch (DuplicateCodigoDniException ex)
        {
            return Conflict(new ProblemDetails
            {
                Title = "Conflict",
                Status = StatusCodes.Status409Conflict,
                Detail = $"El código DNI '{ex.CodigoDni}' ya está registrado."
            });
        }
        catch (DuplicateEmailException ex)
        {
            return Conflict(new ProblemDetails
            {
                Title = "Conflict",
                Status = StatusCodes.Status409Conflict,
                Detail = $"El correo '{ex.Email}' ya está registrado."
            });
        }
    }
}
