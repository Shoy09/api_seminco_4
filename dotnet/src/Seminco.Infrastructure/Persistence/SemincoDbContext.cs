using Microsoft.EntityFrameworkCore;

namespace Seminco.Infrastructure.Persistence;

public sealed class SemincoDbContext(DbContextOptions<SemincoDbContext> options) : DbContext(options)
{
}
