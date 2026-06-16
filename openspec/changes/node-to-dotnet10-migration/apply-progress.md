# Apply Progress: Node to .NET 10 Migration

## Mode

- Apply mode: Standard
- Artifact store: OpenSpec
- Chain strategy: stacked-to-main
- Current PR slice: PR2/Auth-users

## Completed Tasks / Evidence

- [x] 1.1 Created the `dotnet/` solution, API/Application/Domain/Infrastructure projects, and test project shell targeting `net10.0`.
- [x] 1.2 Implemented the API host foundation with DI, JSON, CORS, JWT bearer plumbing, authorization, ProblemDetails, `/docs`, `/api/diagnostico`, and health/readiness endpoints.
- [x] 1.3 Added `SemincoDbContext`, PostgreSQL readiness health check, and Database/JWT/Cloudinary/Hosting option binding from .NET configuration sections plus existing Node environment variable names, without committed plaintext secrets.
- [x] PR1 correction Updated the foundation database provider from Pomelo/MySQL to Npgsql/PostgreSQL after the user chose PostgreSQL as the .NET target database; MySQL remains legacy/source only.
- [x] 2.1 Implemented `POST /api/auth/login` in .NET with the approved `{ codigo_dni, password }` request, BCrypt verification against the legacy `usuarios.password` hash, and JWT claims `id`, `codigo_dni`, `apellidos`, `nombres` using the configured three-hour expiry.
- [x] PR2 remediation Aligned the login success response with the users-auth spec by returning the JWT plus identity fields (`id`, `codigo_dni`, `apellidos`, `nombres`) and hardened BCrypt verification so corrupt/non-BCrypt stored hashes are treated as invalid credentials instead of server errors.
- [ ] 2.2 Profile endpoint implementation started: `/api/usuarios/perfil` now requires Bearer JWT and reads the authenticated user's profile from PostgreSQL-compatible `usuarios` mappings. Signature URL upload/persistence remains pending, so task 2.2 is not marked complete.

## Verification

| Command | Result | Notes |
|---|---|---|
| Target-provider text search in `dotnet/` | Passed | No `Pomelo`, `UseMySql`, `ServerType`, `MySql`, `MySQL`, `mysql`, `TreatTinyAsBoolean`, or `HasCharSet` references remain in PR1 foundation code after correction. |
| `dotnet --info` | Passed | Host has .NET SDK `10.0.100` and .NET runtime `10.0.0` installed. |
| `dotnet build Seminco.sln` from `dotnet/` | Passed | Build succeeded with 0 warnings and 0 errors after moving the local `DocsHtml` constant before first use in `Program.cs`. |
| `dotnet build Seminco.sln` from `dotnet/` | Passed | PR2/Auth-users build succeeded with 0 warnings and 0 errors after adding auth/users application, domain, infrastructure, and controllers. |
| `dotnet build Seminco.sln` from `dotnet/` | Passed | PR2 remediation build succeeded with 0 warnings and 0 errors after adding login response identity fields and BCrypt invalid-hash handling. |

## Deviations / Risks

- None from the PR1 foundation design; operations, measurements, reports, uploads, and business controllers remain out of scope.
- .NET SDK `10.0.100` is now available locally and PR1/Foundation builds successfully.
- Package versions (`Microsoft.*` and `Npgsql.EntityFrameworkCore.PostgreSQL` `10.0.0`) restored successfully during `dotnet build`.
- PR2 intentionally stops at login plus protected profile read to stay below the 400-line review budget; signature upload/persistence and catalog CRUD remain separate follow-up work.
- The profile response parses `operaciones_autorizadas` as JSON when valid and returns the stored value unchanged when parsing fails, matching the broader migration pattern for legacy JSON/text fields.

## Next Steps

1. Run a fresh review of PR2/Auth-users because it adds authentication token issuance and the first protected migrated endpoint.
2. Complete task 2.2 in a follow-up slice by adding signature upload/storage URL persistence through the planned storage port.
3. Keep task 2.3 catalog CRUD out of this PR unless the maintainer explicitly accepts a larger review boundary.
