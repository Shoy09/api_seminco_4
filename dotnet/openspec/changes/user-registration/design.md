# Design: User Registration Endpoint

## Technical Approach

Add a `RegisterService` in the Application layer following the existing `LoginService` pattern. The flow: Controller → `RegisterService` (validate uniqueness → hash password via `IPasswordVerifier.Hash()` → persist via `IUserAuthRepository.CreateAsync()`) → returns `RegisterResponse`. The `rol` claim is added to JWT **first** so `[Authorize(Roles = "admin")]` works immediately.

## Architecture Decisions

| Decision | Choice | Alternatives | Rationale |
|----------|--------|-------------|-----------|
| Where to add `Hash()` | New method on `IPasswordVerifier` | Separate `IPasswordHasher` interface | Single interface for all password ops; BCryptPasswordVerifier already owns both concerns |
| Response DTO | New `RegisterResponse` | Reuse `UserProfileResponse` | Decouples registration shape from profile shape; avoids accidental breaking changes |
| Uniqueness validation | App-level check + DB unique indexes | DB-only with exception handling | App-level gives clear 409 response before hitting DB; indexes are final guard against race conditions |
| `rol` defaulting | In `RegisterService` | In DTO or controller | Service is the single authority for business defaults; DTO stays a pure contract |

## Data Flow

```
Client ──POST /api/auth/register──→ AuthController
  [Authorize(Roles="admin")] checks JWT ClaimTypes.Role
         ↓
  RegisterService.RegisterAsync()
    ├─ IUserAuthRepository.FindByCodigoDniAsync() → 409 if exists
    ├─ IUserAuthRepository.FindByEmailAsync()     → 409 if exists (only if correo provided)
    ├─ IPasswordVerifier.Hash(password)           → BCrypt hash
    ├─ IUserAuthRepository.CreateAsync(user)      → EF Core INSERT
    └─ Returns RegisterResponse ←─────────────────┘
         ↓
Client ←── 200 OK (RegisterResponse)
```

## Prerequisite: `rol` Claim in JWT (Step 0)

**File**: `Seminco.Infrastructure/Auth/JwtTokenIssuer.cs` — **Modify**

Add to the claims array in `Issue(User user)`:

```csharp
new Claim(ClaimTypes.Role, user.Rol ?? "trabajador")
```

This MUST be done first. `[Authorize(Roles = "admin")]` checks `ClaimTypes.Role`. Without this claim every existing token or new token for admin users will return 403.

## File Changes

| File | Action | Description |
|------|--------|-------------|
| `Application/Auth/AuthDtos.cs` | Modify | Add `RegisterRequest`, `RegisterResponse` records |
| `Application/Auth/AuthServices.cs` | Modify | Add `Hash()` to `IPasswordVerifier`; add `CreateAsync`/`FindByEmailAsync` to `IUserAuthRepository`; add `RegisterService` |
| `Infrastructure/Auth/BCryptPasswordVerifier.cs` | Modify | Implement `Hash()` |
| `Infrastructure/Auth/JwtTokenIssuer.cs` | Modify | Add `ClaimTypes.Role` claim |
| `Infrastructure/Users/UserRepository.cs` | Modify | Implement `CreateAsync`, `FindByEmailAsync` |
| `Infrastructure/DependencyInjection.cs` | Modify | Register `RegisterService` |
| `Api/Controllers/AuthController.cs` | Modify | Add `Register` action |
| `Migrations/` | New | EF Core `InitialCreate` migration |

## Interfaces / Contracts

### `IPasswordVerifier` (modified interface)

```csharp
namespace Seminco.Application.Auth;

public interface IPasswordVerifier
{
    bool Verify(string password, string passwordHash);
    string Hash(string password);  // NEW: BCrypt hash generation
}
```

### `IUserAuthRepository` (modified interface)

```csharp
namespace Seminco.Application.Auth;

public interface IUserAuthRepository
{
    Task<User?> FindByCodigoDniAsync(string codigoDni, CancellationToken cancellationToken);
    Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken);  // NEW
    Task<User> CreateAsync(User user, CancellationToken cancellationToken);           // NEW
}
```

### DTOs (`AuthDtos.cs`)

```csharp
public sealed record RegisterRequest(
    [property: Required, JsonPropertyName("codigo_dni")] string CodigoDni,
    [property: Required] string Nombres,
    [property: Required] string Apellidos,
    [property: Required] string Password,
    [property: JsonPropertyName("correo")] string? Correo,
    string? Cargo,
    string? Area,
    string? Clasificacion,
    string? Empresa,
    string? Guardia,
    [property: JsonPropertyName("autorizado_equipo")] string? AutorizadoEquipo);

public sealed record RegisterResponse(
    int Id,
    [property: JsonPropertyName("codigo_dni")] string CodigoDni,
    string Apellidos,
    string Nombres,
    string? Cargo,
    string? Empresa,
    string? Guardia,
    [property: JsonPropertyName("autorizado_equipo")] string? AutorizadoEquipo,
    string? Correo,
    string? Rol,
    [property: JsonPropertyName("operaciones_autorizadas")] object? OperacionesAutorizadas);
```

Key: `RegisterRequest` omits `rol` — defaulting is the service's job. Response shape mirrors `UserProfileResponse`.

### `RegisterService` (new class in `AuthServices.cs`)

```csharp
public sealed class RegisterService(
    IUserAuthRepository users,
    IPasswordVerifier passwords)
{
    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request, CancellationToken ct)
    {
        // 1. Uniqueness checks
        if (await users.FindByCodigoDniAsync(request.CodigoDni.Trim(), ct) is not null)
            throw new DuplicateCodigoDniException(request.CodigoDni);
        if (request.Correo?.Trim() is { Length: > 0 } email
            && await users.FindByEmailAsync(email, ct) is not null)
            throw new DuplicateEmailException(email);

        // 2. Create domain entity
        var user = new User
        {
            CodigoDni = request.CodigoDni.Trim(),
            Nombres = request.Nombres.Trim(),
            Apellidos = request.Apellidos.Trim(),
            PasswordHash = passwords.Hash(request.Password),
            Rol = "trabajador",  // default — no rol field in request
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

        // 3. Persist
        var created = await users.CreateAsync(user, ct);

        // 4. Return
        return new RegisterResponse(
            created.Id, created.CodigoDni, created.Apellidos, created.Nombres,
            created.Cargo, created.Empresa, created.Guardia,
            created.AutorizadoEquipo, created.Correo, created.Rol, null);
    }
}
```

### AuthController — Register action

```csharp
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
```

## Migration Details

**Command**: `dotnet ef migrations add InitialCreate --project src/Seminco.Infrastructure --startup-project src/Seminco.Api --output-dir Migrations`

The generated `Up()` will produce:

```sql
CREATE TABLE "usuarios" (
    "id"            INTEGER GENERATED BY DEFAULT AS IDENTITY,
    "codigo_dni"    TEXT    NOT NULL,
    "apellidos"     TEXT    NOT NULL,
    "nombres"       TEXT    NOT NULL,
    "cargo"         TEXT    NULL,
    "rol"           TEXT    NULL,
    "area"          TEXT    NULL,
    "clasificacion" TEXT    NULL,
    "empresa"       TEXT    NULL,
    "guardia"       TEXT    NULL,
    "autorizado_equipo" TEXT NULL,
    "correo"        TEXT    NULL,
    "password"      TEXT    NOT NULL,
    "firma"         TEXT    NULL,
    "operaciones_autorizadas" JSONB NULL,
    "createdAt"     TIMESTAMPTZ NULL,
    "updatedAt"     TIMESTAMPTZ NULL,
    CONSTRAINT "PK_usuarios" PRIMARY KEY ("id")
);

CREATE UNIQUE INDEX "IX_usuarios_codigo_dni" ON "usuarios" ("codigo_dni");
CREATE UNIQUE INDEX "IX_usuarios_correo" ON "usuarios" ("correo") WHERE "correo" IS NOT NULL;
```

## Auth Design

Current JWT claims: `id`, `codigo_dni`, `apellidos`, `nombres`. After adding `ClaimTypes.Role`, the token carries:

```
ClaimTypes.Role = user.Rol ?? "trabajador"
```

ASP.NET Core's `[Authorize(Roles = "admin")]` maps to `ClaimTypes.Role` by default. When a user with `Rol = "admin"` logs in, their JWT includes `ClaimTypes.Role = "admin"`, and `[Authorize(Roles = "admin")]` evaluates to true. Existing users with `Rol IS NULL` get `"trabajador"` — safe default that denies admin access.

## Error Handling

| Scenario | Exception / Trigger | HTTP Status | Mechanism |
|----------|-------------------|-------------|-----------|
| Missing required fields | Model validation | 400 | `InvalidModelStateResponseFactory` (Program.cs) |
| Duplicate `codigo_dni` | `DuplicateCodigoDniException` | 409 Conflict | Caught in controller |
| Duplicate `correo` | `DuplicateEmailException` | 409 Conflict | Caught in controller |
| Unauthenticated | No/invalid JWT | 401 | JWT middleware `OnChallenge` |
| Non-admin JWT | Missing Role claim | 403 | ASP.NET Core authorization |

## DI Registration

Add to `DependencyInjection.AddSemincoInfrastructure()`:

```csharp
services.AddScoped<RegisterService>();
```

No new interface registrations needed — `IUserAuthRepository` and `IPasswordVerifier` are already registered.

## Exception Types

New file: `Application/Auth/AuthExceptions.cs`

```csharp
namespace Seminco.Application.Auth;

public sealed class DuplicateCodigoDniException(string codigoDni) : Exception
{
    public string CodigoDni { get; } = codigoDni;
}

public sealed class DuplicateEmailException(string email) : Exception
{
    public string Email { get; } = email;
}
```

## Implementation Order

| Step | File(s) | Depends On |
|------|---------|------------|
| 0 | `JwtTokenIssuer.cs` | Nothing — #1 priority |
| 1 | `AuthServices.cs` (interfaces) + `AuthExceptions.cs` | Nothing |
| 2 | `AuthDtos.cs` | Nothing |
| 3 | `BCryptPasswordVerifier.cs` | Step 1 (interface change) |
| 4 | `UserRepository.cs` | Step 1 (interface change) |
| 5 | `AuthServices.cs` (RegisterService) | Steps 1-4 |
| 6 | `AuthController.cs` | Step 5 |
| 7 | `DependencyInjection.cs` | Step 5 |
| 8 | Migration (`dotnet ef migrations add`) | Steps 0-7 |

## Testing Strategy

| Layer | What to Test | Approach |
|-------|-------------|----------|
| Domain | `User` entity property defaults | Unit — instantiate and assert defaults |
| Application | `RegisterService.RegisterAsync` — success path, duplicate `codigo_dni`, duplicate `correo`, password hashing called | Unit — mock `IUserAuthRepository` + `IPasswordVerifier` |
| Application | `RegisterRequest` validation — missing required fields | Unit — `ValidationContext` assertions |
| Infrastructure | `BCryptPasswordVerifier.Hash()` — produces verifiable hash | Unit — hash + verify roundtrip |
| Infrastructure | `UserRepository.CreateAsync` — persisted user has correct properties | Integration — in-memory Postgres or `DbContext` with real DB |
| Infrastructure | `JwtTokenIssuer.Issue()` — token contains `ClaimTypes.Role` | Unit — decode token and assert claim presence |
| API | `POST /api/auth/register` — admin token → 200, non-admin → 403 | Integration — `WebApplicationFactory` with TestAuthHandler |
| Migration | `Up()` / `Down()` idempotency | `dotnet ef migrations script` + review |

## Open Questions

None.
