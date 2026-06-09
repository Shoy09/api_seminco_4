using System.Text.Json;
using Seminco.Domain.Users;

namespace Seminco.Application.Users;

public interface IUserProfileRepository
{
    Task<User?> FindByIdAsync(int id, CancellationToken cancellationToken);
}

public sealed class UserProfileService(IUserProfileRepository users)
{
    public async Task<UserProfileResponse?> GetProfileAsync(int id, CancellationToken cancellationToken)
    {
        var user = await users.FindByIdAsync(id, cancellationToken);
        return user is null ? null : new UserProfileResponse(
            user.Id,
            user.CodigoDni,
            user.Apellidos,
            user.Nombres,
            user.Cargo,
            user.Empresa,
            user.Guardia,
            user.AutorizadoEquipo,
            user.Correo,
            user.Firma,
            user.Rol,
            ParseOperations(user.OperacionesAutorizadas));
    }

    private static object? ParseOperations(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return null;
        try { return JsonSerializer.Deserialize<object>(value); }
        catch (JsonException) { return value; }
    }
}
