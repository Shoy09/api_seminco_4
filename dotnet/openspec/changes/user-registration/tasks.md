# Tasks: User Registration Endpoint

## Review Workload Forecast

| Field | Value |
|-------|-------|
| Estimated changed lines | ~210 implementation + ~20 minor (migration auto-gen ~70 more) |
| 400-line budget risk | Low |
| Chained PRs recommended | No |
| Suggested split | Single PR |
| Delivery strategy | single-pr |
| Chain strategy | size-exception |

Decision needed before apply: Yes
Chained PRs recommended: No
Chain strategy: size-exception
400-line budget risk: Low

## Phase 1: Foundation — Interfaces, DTOs, Exceptions

- [ ] **T-001 (Step 0) — Add `rol` claim to JWT**. Insert `new Claim(ClaimTypes.Role, user.Rol ?? "trabajador")` in `JwtTokenIssuer.Issue()` claims array. Deps: none. Effort: Small. AC: JWT contains `ClaimTypes.Role` with user's `Rol` value.
- [ ] **T-002 (Step 1) — Extend auth interfaces**. Add `Hash(string)` to `IPasswordVerifier`; add `FindByEmailAsync(string, CancellationToken)` and `CreateAsync(User, CancellationToken)` to `IUserAuthRepository`. File: `AuthServices.cs`. Deps: none. Effort: Small. AC: Interfaces compile with new method signatures.
- [ ] **T-003 (Step 1) — Create auth exceptions**. New file `Application/Auth/AuthExceptions.cs` with `DuplicateCodigoDniException` and `DuplicateEmailException`. Deps: none. Effort: Small. AC: Both exceptions carry their identifier property and are throwable.
- [ ] **T-004 (Step 2) — Add Register DTOs**. Add `RegisterRequest` (required: `codigo_dni`, `nombres`, `apellidos`, `password`; optional rest) and `RegisterResponse` (mirrors `UserProfileResponse` shape, no password hash) records. File: `AuthDtos.cs`. Deps: none. Effort: Small. AC: DTOs serialize/deserialize with snake_case JSON property names.

## Phase 2: Implementation — Hash, Persistence, RegisterService

- [ ] **T-005 (Step 3) — Implement `Hash()` in BCryptPasswordVerifier**. Add `Hash(string)` → `BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12)`. File: `BCryptPasswordVerifier.cs`. Deps: T-002. Effort: Small. AC: `Hash("secret")` returns `$2a$12$...` string verifiable by `Verify()`.
- [ ] **T-006 (Step 4) — Implement `CreateAsync` / `FindByEmailAsync` in UserRepository**. `FindByEmailAsync`: query `db.Users` by `Correo == email`. `CreateAsync`: `db.Users.AddAsync` + `db.SaveChangesAsync`, return created User. File: `UserRepository.cs`. Deps: T-002. Effort: Small. AC: New user is persisted; query by email returns matching user.
- [ ] **T-007 (Step 5) — Add `RegisterService`**. New class in `AuthServices.cs`: validate uniqueness (409 if duplicate `codigo_dni` or `correo`), map request to `User` entity, hash password via `IPasswordVerifier.Hash()`, default `Rol = "trabajador"`, persist via `IUserAuthRepository.CreateAsync()`, return `RegisterResponse`. Deps: T-002, T-003, T-004, T-005, T-006. Effort: Medium. AC: `RegisterAsync` returns `RegisterResponse` on success; throws correct exception on duplicate fields; password is BCrypt hash; `rol` defaults to `"trabajador"`.

## Phase 3: Integration — Controller, DI, Migration

- [ ] **T-008 (Step 6) — Add `Register` action in AuthController**. `[HttpPost("register")] [Authorize(Roles = "admin")]` action that injects `RegisterService`, calls `RegisterAsync`, catches exceptions → 409. File: `AuthController.cs`. Deps: T-003, T-004, T-007. Effort: Small. AC: Admin POST returns 200; non-admin returns 403; unauthenticated returns 401; duplicate returns 409.
- [ ] **T-009 (Step 7) — Register `RegisterService` in DI**. Add `services.AddScoped<RegisterService>()` in `DependencyInjection.cs`. Deps: T-007. Effort: Small. AC: `RegisterService` resolvable from DI container.
- [ ] **T-010 (Step 8) — Add unique indexes + generate migration**. Add `HasIndex(user => user.CodigoDni).IsUnique()` and `HasIndex(user => user.Correo).IsUnique().HasFilter(...)` in `SemincoDbContext.OnModelCreating`. Run `dotnet ef migrations add InitialCreate`. File: `SemincoDbContext.cs` + `Migrations/` new. Deps: all previous. Effort: Medium. AC: Migration `Up()` creates `usuarios` table with unique indexes; `Down()` drops it.

