using Seminco.Domain.Users;

namespace Seminco.Application.Auth;

public interface IUserAuthRepository
{
    Task<User?> FindByCodigoDniAsync(string codigoDni, CancellationToken cancellationToken);
    Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User> CreateAsync(User user, CancellationToken cancellationToken);
}

public interface IPasswordVerifier
{
    bool Verify(string password, string passwordHash);
    string Hash(string password);
}

public interface IJwtTokenIssuer
{
    string Issue(User user);
}

public sealed class LoginService(IUserAuthRepository users, IPasswordVerifier passwords, IJwtTokenIssuer tokens)
{
    public async Task<LoginResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await users.FindByCodigoDniAsync(request.CodigoDni.Trim(), cancellationToken);
        if (user is null || !passwords.Verify(request.Password, user.PasswordHash)) return null;

        return new LoginResponse(tokens.Issue(user), user.Id, user.CodigoDni, user.Apellidos, user.Nombres);
    }
}

public sealed class RegisterService(IUserAuthRepository users, IPasswordVerifier passwords)
{
    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request, CancellationToken ct)
    {
        if (await users.FindByCodigoDniAsync(request.CodigoDni.Trim(), ct) is not null)
            throw new DuplicateCodigoDniException(request.CodigoDni);

        if (request.Correo?.Trim() is { Length: > 0 } email
            && await users.FindByEmailAsync(email, ct) is not null)
            throw new DuplicateEmailException(email);

        var user = new User
        {
            CodigoDni = request.CodigoDni.Trim(),
            Nombres = request.Nombres.Trim(),
            Apellidos = request.Apellidos.Trim(),
            PasswordHash = passwords.Hash(request.Password),
            Rol = request.Rol?.Trim(),
            Correo = request.Correo?.Trim(),
            Cargo = request.Cargo?.Trim(),
            Area = request.Area?.Trim(),
            Clasificacion = request.Clasificacion?.Trim(),
            Empresa = request.Empresa?.Trim(),
            Guardia = request.Guardia?.Trim(),
            AutorizadoEquipo = request.AutorizadoEquipo?.Trim(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await users.CreateAsync(user, ct);

        return new RegisterResponse(
            created.Id, created.CodigoDni, created.Apellidos, created.Nombres,
            created.Cargo, created.Empresa, created.Guardia,
            created.AutorizadoEquipo, created.Correo, created.Rol, null);
    }
}
