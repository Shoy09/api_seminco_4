using Seminco.Domain.Users;

namespace Seminco.Application.Auth;

public interface IUserAuthRepository
{
    Task<User?> FindByCodigoDniAsync(string codigoDni, CancellationToken cancellationToken);
}

public interface IPasswordVerifier
{
    bool Verify(string password, string passwordHash);
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
