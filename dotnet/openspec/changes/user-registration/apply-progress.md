# Apply Progress: User Registration Endpoint

| Task | Description | Status | Notes |
|------|-------------|--------|-------|
| T-001 | Add `rol` claim to JWT | ✅ Done | `JwtTokenIssuer.cs` — added `ClaimTypes.Role` with fallback `"trabajador"` |
| T-002 | Extend auth interfaces | ✅ Done | `AuthServices.cs` — added `Hash()` to `IPasswordVerifier`, `FindByEmailAsync`/`CreateAsync` to `IUserAuthRepository` |
| T-003 | Create auth exceptions | ✅ Done | `AuthExceptions.cs` — `DuplicateCodigoDniException`, `DuplicateEmailException` |
| T-004 | Add Register DTOs | ✅ Done | `AuthDtos.cs` — `RegisterRequest` (snake_case, no `rol`), `RegisterResponse` (mirrors `UserProfileResponse` sans `Firma`) |
| T-005 | Implement `Hash()` in BCryptPasswordVerifier | ✅ Done | `BCryptPasswordVerifier.cs` — `Hash(password)` with work factor 12 |
| T-006 | Implement `CreateAsync`/`FindByEmailAsync` in UserRepository | ✅ Done | `UserRepository.cs` — EF Core AddAsync + SaveChangesAsync; query by `Correo` |
| T-007 | Add `RegisterService` | ✅ Done | `AuthServices.cs` — validates uniqueness, auto-defaults `rol = "trabajador"`, BCrypt hashes, persists, returns `RegisterResponse` |
| T-008 | Add `Register` action in AuthController | ✅ Done | `AuthController.cs` — `[HttpPost("register")] [Authorize(Roles = "admin")]`, catches duplicates → 409 |
| T-009 | Register `RegisterService` in DI | ✅ Done | `DependencyInjection.cs` — `services.AddScoped<RegisterService>()` |
| T-010 | Add unique indexes + generate migration | ✅ Done | `SemincoDbContext.cs` — unique indexes on `CodigoDni` and `Correo` (filtered); `dotnet ef migrations add InitialCreate` ran successfully |
