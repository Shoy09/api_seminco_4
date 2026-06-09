# Tasks: Node to .NET 10 Migration

## Review Workload Forecast

| Field | Value |
|-------|-------|
| Estimated changed lines | 6,000-10,000+ |
| 400-line budget risk | High |
| Chained PRs recommended | Yes |
| Suggested split | PR1 foundation → PR2 auth/users/catalog → PR3 reports/catalogs → PR4 operations → PR5 measurements/uploads/cutover |
| Delivery strategy | ask-on-risk |
| Chain strategy | stacked-to-main |

Decision needed before apply: No
Chained PRs recommended: Yes
Chain strategy: stacked-to-main
400-line budget risk: High

### Suggested Work Units

| Unit | Goal | Likely PR | Verification |
|------|------|-----------|--------------|
| 1 | Minimal `dotnet/` solution, health, `/docs`, ProblemDetails | PR1 | `dotnet build`; manual `/docs`, health, unsupported-route checks |
| 2 | Auth, profile, one catalog CRUD pattern | PR2 | login/profile/catalog tests and Node parity checks |
| 3 | Plans/reports and shared query patterns | PR3 | period/report contract checks |
| 4 | Dynamic operations | PR4 | supported/unsupported `tipo`, read/write, JSON/text checks |
| 5 | Measurements, uploads, deployment cutover | PR5 | measurement, Cloudinary, readiness, rollback checklist |

## Phase 1: Foundation

- [x] 1.1 Create `dotnet/Seminco.sln`, `dotnet/src/Seminco.Api/Seminco.Api.csproj`, `dotnet/src/Seminco.Application/*`, `dotnet/src/Seminco.Domain/*`, `dotnet/src/Seminco.Infrastructure/*`, and `dotnet/tests/*` projects.
- [x] 1.2 Implement `dotnet/src/Seminco.Api/Program.cs` for DI, JSON, CORS, JWT bearer, authorization, ProblemDetails, `/docs`, and health/readiness.
- [x] 1.3 Add `dotnet/src/Seminco.Infrastructure/Persistence/SemincoDbContext.cs` plus PostgreSQL/Npgsql, JWT, Cloudinary, and hosting options bound from environment.

## Phase 2: Auth, Users, First Catalog

- [x] 2.1 Implement `dotnet/src/Seminco.Api/Controllers/AuthController.cs`, `dotnet/src/Seminco.Application/Auth/*`, and `dotnet/src/Seminco.Infrastructure/Auth/*` for `POST /api/auth/login` with bcrypt and three-hour JWT.
- [ ] 2.2 Implement `dotnet/src/Seminco.Api/Controllers/UsuariosController.cs`, `dotnet/src/Seminco.Application/Users/*`, and `dotnet/src/Seminco.Infrastructure/Users/*` for `/api/usuarios/perfil` and signature URL persistence.
- [ ] 2.3 Implement `dotnet/src/Seminco.Api/Controllers/TipoEquiposController.cs`, `dotnet/src/Seminco.Application/Catalogs/*`, and `dotnet/src/Seminco.Infrastructure/Catalogs/*` as the protected CRUD pattern.

## Phase 3: Reports and Operations

- [ ] 3.1 Add `dotnet/src/Seminco.Domain/Operations/*` and `Infrastructure/Operations/*` registry for the nine approved `tipo` keys and PostgreSQL target tables mapped from the legacy MySQL source names.
- [ ] 3.2 Implement `dotnet/src/Seminco.Api/Controllers/OperacionesController.cs` for list, detail, jefe, approval, latest-horometer, create, and update with JSON/text parsing.
- [ ] 3.3 Implement `dotnet/src/Seminco.Api/Controllers/Reportes/*` and `dotnet/src/Seminco.Infrastructure/Reports/*` for monthly, metraje, production, approval, and jefe-filtered reports.

## Phase 4: Measurements, Uploads, Verification, Cutover

- [ ] 4.1 Implement `dotnet/src/Seminco.Api/Controllers/MedicionesHorizontalController.cs` and related application/infrastructure files, documenting the resolved duplicate collection behavior.
- [ ] 4.2 Implement `dotnet/src/Seminco.Application/Storage/IFileStorage.cs` and `dotnet/src/Seminco.Infrastructure/Storage/CloudinaryFileStorage.cs` preserving `firmas`, `pdf-operaciones`, and `Sostenimiento` folders.
- [ ] 4.3 Add `dotnet/tests/*` unit, integration, and characterization checks for spec scenarios; update `dotnet/README.md` with deployment, secrets, Node rollback, and retirement steps.
