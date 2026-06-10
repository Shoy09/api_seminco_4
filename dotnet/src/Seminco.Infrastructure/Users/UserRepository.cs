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

    public Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken) =>
        db.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Correo == email, cancellationToken);

    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken)
    {
        await db.Users.AddAsync(user, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
        return user;
    }

    public Task<User?> FindByIdAsync(int id, CancellationToken cancellationToken) =>
        db.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
}
