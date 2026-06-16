# Design: Node to .NET 10 Migration

## Technical Approach

Build a new .NET 10 API under `dotnet/` while using the current Node code only as the behavior reference. The final runtime is ASP.NET Core only; Node remains deployable during development/rollback, but there is no long-running side-by-side production architecture. Migration proceeds by reviewable vertical slices: foundation/auth, users, catalog/report patterns, operations, measurements, exploration/uploads.

## Architecture Decisions

| Area | Choice | Alternatives considered | Rationale |
|---|---|---|---|
| Root | `dotnet/Seminco.sln` with `src/` and `tests/` | Replace repository `src/`; separate repo | Keeps Node reference intact during migration and makes cutover explicit. |
| API style | Controllers grouped by slice under `/api` | Minimal APIs everywhere | Current route surface is controller-like and compatibility-heavy; controllers simplify auth, ProblemDetails, OpenAPI grouping. |
| Layers | `Seminco.Api`, `Application`, `Domain`, `Infrastructure` | Flat API project; heavy DDD | Enterprise boundaries without over-modeling JSON/text operation payloads. |
| Database | PostgreSQL target database with EF Core and Npgsql; legacy MySQL is source/reference data only, with Dapper available for parity/report SQL where useful | Keep MySQL/Pomelo as target; immediate schema redesign | The .NET runtime must own the PostgreSQL target while preserving behavior learned from Sequelize/raw mysql2 and legacy table/payload contracts. |
| Migrations | Freeze Sequelize migrations as historical source; no schema changes in first slices; future changes owned by .NET EF migrations or SQL scripts after cutover | Continue Sequelize CLI indefinitely | Avoids dual migration ownership once Node is retired. |
| Auth | `POST /api/auth/login` remains public; all migrated critical/mutating routes require Bearer JWT unless explicitly documented public | Preserve inconsistent unprotected writes | User approved auth hardening; fixes operations create/update, measurements create, production plan, and exploration write exposure. |
| Errors | ASP.NET Core `ProblemDetails`/validation problem envelope | Preserve ad-hoc `{error}`/`{message}` everywhere | User approved status/error normalization; compatibility exceptions must be documented per route. |

## Data Flow

    Request → Controller → Validator → Application Use Case
       → Repository/Query/Cloudinary Port → PostgreSQL or Cloudinary
       → DTO mapper → ProblemDetails or success response

Complex exploration writes stay transactional in `Infrastructure` to mirror `NubeDatosTrabajoExploracionesConstroller.js` parent/child inserts.

## File Changes

| File | Action | Description |
|---|---|---|
| `dotnet/Seminco.sln` | Create | Solution root for replacement API. |
| `dotnet/src/Seminco.Api/Seminco.Api.csproj` | Create | ASP.NET Core 10 host, controllers, auth, OpenAPI, health. |
| `dotnet/src/Seminco.Api/Program.cs` | Create | DI, CORS, JSON options, JWT bearer, authorization, ProblemDetails, `/docs`. |
| `dotnet/src/Seminco.Api/Controllers/*` | Create | `AuthController`, `UsuariosController`, `OperacionesController`, `MedicionesHorizontalController`, `Reportes/*`. |
| `dotnet/src/Seminco.Application/*` | Create | Slice use cases, DTOs, validators, ports. |
| `dotnet/src/Seminco.Domain/*` | Create | Minimal entities/value objects for users, operations, measurements, reports. |
| `dotnet/src/Seminco.Infrastructure/*` | Create | PostgreSQL/Npgsql DbContext, Dapper queries, bcrypt/JWT services, Cloudinary adapter. |
| `dotnet/tests/*` | Create | Unit, integration, and characterization tests. |
| `src/**`, `migrations/**` | Reference only | Do not modify during design/apply except eventual retirement. |

## Interfaces / Contracts

- Routes keep `/api`; corrected route names may become primary, with in-app legacy aliases only when needed (`/TipoPerfpo`, `/Equipo`, `/usuarios/usuarios`, etc.).
- Login request remains `{ codigo_dni, password }`; JWT keeps `id`, `codigo_dni`, `apellidos`, `nombres`, three-hour expiry.
- Operations support exactly: `tal_largo`, `tal_horizontal`, `empernador`, `carguio`, `rompebanco`, `scissor`, `anfochanger`, `scalamin`, `dumper`.
- Operation text fields (`registros`, `horometros`, `check_list`, etc.) are returned parsed when valid JSON, unchanged when not parseable.
- Cloudinary folders remain `firmas`, `pdf-operaciones`, and `Sostenimiento`; uploads use an `IFileStorage` port.

## Testing Strategy

| Layer | What to Test | Approach |
|---|---|---|
| Unit | Validators, JSON/text parsing, auth claim mapping | xUnit + focused service tests. |
| Integration | PostgreSQL mappings, transactions, auth middleware | WebApplicationFactory + PostgreSQL container or staged DB. |
| Characterization | Node vs .NET critical responses | Snapshot/API checks for auth, profile, operations, measurements, reports before cutover. |
| Manual | Cloudinary uploads and deployment secrets | Staged environment checklist. |

## Migration / Rollout

1. Foundation PR: solution, config, health, OpenAPI, ProblemDetails, JWT plumbing.
2. Auth/users PR: login, profile, user signature upload.
3. Reports/catalog PRs: period plan queries and simple CRUD patterns.
4. Operations PRs: dynamic dispatch, approvals, jefe filters, horometers, writes.
5. Measurements/exploration PRs: fix duplicate collection behavior, transactional aggregates.
6. Cutover: point clients to .NET, keep Node rollback available briefly, then retire Node runtime/config.

Each PR should stay reviewable; if forecast exceeds 400 changed lines, split by slice before apply.

## Open Questions

- [ ] Final .NET hosting target and secret store.
- [ ] Exact list of legacy route aliases to keep vs remove during route cleanup.
