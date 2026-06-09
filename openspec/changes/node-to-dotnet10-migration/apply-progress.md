# Apply Progress: Node to .NET 10 Migration

## Mode

- Apply mode: Standard
- Artifact store: OpenSpec
- Chain strategy: stacked-to-main
- Current PR slice: PR1/Foundation

## Completed Tasks / Evidence

- [x] 1.1 Created the `dotnet/` solution, API/Application/Domain/Infrastructure projects, and test project shell targeting `net10.0`.
- [x] 1.2 Implemented the API host foundation with DI, JSON, CORS, JWT bearer plumbing, authorization, ProblemDetails, `/docs`, `/api/diagnostico`, and health/readiness endpoints.
- [x] 1.3 Added `SemincoDbContext`, PostgreSQL readiness health check, and Database/JWT/Cloudinary/Hosting option binding from .NET configuration sections plus existing Node environment variable names, without committed plaintext secrets.
- [x] PR1 correction Updated the foundation database provider from Pomelo/MySQL to Npgsql/PostgreSQL after the user chose PostgreSQL as the .NET target database; MySQL remains legacy/source only.

## Verification

| Command | Result | Notes |
|---|---|---|
| Target-provider text search in `dotnet/` | Passed | No `Pomelo`, `UseMySql`, `ServerType`, `MySql`, `MySQL`, `mysql`, `TreatTinyAsBoolean`, or `HasCharSet` references remain in PR1 foundation code after correction. |
| `dotnet --info` | Passed | Host has .NET SDK `10.0.100` and .NET runtime `10.0.0` installed. |
| `dotnet build Seminco.sln` from `dotnet/` | Passed | Build succeeded with 0 warnings and 0 errors after moving the local `DocsHtml` constant before first use in `Program.cs`. |

## Deviations / Risks

- None from the PR1 foundation design; auth/users, operations, measurements, reports, uploads, and business controllers remain out of scope.
- .NET SDK `10.0.100` is now available locally and PR1/Foundation builds successfully.
- Package versions (`Microsoft.*` and `Npgsql.EntityFrameworkCore.PostgreSQL` `10.0.0`) restored successfully during `dotnet build`.

## Next Steps

1. Run a fresh review of PR1/Foundation because the target database provider changed and the previously blocked build now passes.
2. Open/review PR1/Foundation before starting PR2 business behavior.
3. Start PR2 only after PR1/Foundation is accepted.
