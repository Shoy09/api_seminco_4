using Microsoft.EntityFrameworkCore;
using Seminco.Application.Auth;
using Seminco.Application.Users;
using Seminco.Domain.Users;
using Seminco.Infrastructure.Persistence;

namespace Seminco.Infrastructure.Users;

public sealed class UserRepository(SemincoDbContext db) : IUserAuthRepository, IUserProfileRepository
{
    public Task<User?> FindByCodigoDniAsync(string codigoDni, CancellationToken cancellationToken) =>
        db.Users.AsNoTracking().FirstOrDefaultAsync(user => user.CodigoDni == codigoDni, cancellationToken);

    public Task<User?> FindByIdAsync(int id, CancellationToken cancellationToken) =>
        db.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
}
