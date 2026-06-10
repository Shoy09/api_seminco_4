# Proposal: User Registration Endpoint

## Intent

Admins cannot register users via API — accounts require direct DB inserts. This endpoint enables admin-only user provisioning, removing the manual step.

## Scope

### In Scope
- `POST /api/auth/register` (admin-only)
- `IPasswordVerifier.Hash()` for BCrypt hashing
- `IUserAuthRepository.CreateAsync()` + `IUserProfileRepository.FindByEmailAsync()`
- `rol` claim added to JWT tokens
- EF Core `InitialCreate` migration for `usuarios` table
- `RegisterRequest`/`RegisterResponse` DTOs (snake_case)
- Validation: `codigo_dni` unique, `correo` unique if provided
- `rol` defaults to `"trabajador"`

### Out of Scope
Email verification, password strength rules, user update/delete, bulk import, public registration.

## Capabilities

### New Capabilities
- `user-registration`: Admin-only `POST /api/auth/register` covering uniqueness validation, BCrypt hashing, role defaulting, and response matching `UserProfileResponse`.

### Modified Capabilities
None.

## Approach

1. Add `Hash()` to `IPasswordVerifier`, implement in `BCryptPasswordVerifier`
2. Add `CreateAsync`/`FindByEmailAsync` to repo interfaces, implement in `UserRepository`
3. Add `rol` claim to `JwtTokenIssuer.Issue()`
4. Create `RegisterRequest` (required: `codigo_dni`, `nombres`, `apellidos`, `password`; optional: `correo`, `cargo`, `area`, `clasificacion`, `empresa`, `guardia`, `autorizado_equipo`, `rol`)
5. Create `RegisterResponse` matching `UserProfileResponse` shape
6. Add `RegisterService` (validate uniqueness → hash → persist → return)
7. Add `Register` action in `AuthController` with `[Authorize(Roles = "admin")]`
8. Generate `InitialCreate` migration
9. Register `RegisterService` in DI

## Affected Areas

| Area | Impact |
|------|--------|
| `Application/Auth/AuthServices.cs` | Modified |
| `Application/Auth/AuthDtos.cs` | New |
| `Application/Users/UserProfileServices.cs` | Modified |
| `Infrastructure/Auth/BCryptPasswordVerifier.cs` | Modified |
| `Infrastructure/Auth/JwtTokenIssuer.cs` | Modified |
| `Infrastructure/Users/UserRepository.cs` | Modified |
| `Infrastructure/DependencyInjection.cs` | Modified |
| `Infrastructure/Persistence/SemincoDbContext.cs` | Modified |
| `Api/Controllers/AuthController.cs` | Modified |
| `Migrations/` | New |

## Risks

| Risk | Likelihood | Mitigation |
|------|------------|------------|
| Admin token lacks `rol` claim → 403 | High | Add claim to `Issue()` first, register second |
| Race condition on duplicate fields | Med | DB unique indexes as final guard |
| Accidental public exposure | Low | `[Authorize(Roles = "admin")]` + global `RequireAuthorization()` |

## Rollback Plan

`dotnet ef migrations remove`. Revert controller, DTOs, service, and interface changes. Migration is additive — no data loss.

## Dependencies

None. `BCrypt.Net-Next 4.0.3` already referenced.

## Success Criteria

- [ ] Valid admin request → `200 OK` + user data (not JWT)
- [ ] Non-admin user → `403 Forbidden`
- [ ] Unauthenticated request → `401 Unauthorized`
- [ ] Duplicate `codigo_dni` or `correo` → `409 Conflict`
- [ ] Missing required fields → `400 Bad Request`
- [ ] Password stored as BCrypt hash, not plaintext
- [ ] `rol` defaults to `"trabajador"` when omitted
