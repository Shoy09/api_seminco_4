## Exploration: Migrate the Node.js/Express mining operation management API to .NET 10

### Current State

The current backend is an Express 4 CommonJS API mounted from `src/app.js`. It enables CORS, parses JSON payloads up to `10mb`, mounts all API routes under `/api`, and serves the generated Swagger document at `/docs`. Route aggregation happens in `src/routes/index.js`, which wires 24 route modules to mixed-case and Spanish path segments such as `/api/Equipo`, `/api/TipoPerfpo`, `/api/usuarios/usuarios`, `/api/operaciones`, and `/api/NubeDatosExploraciones`.

The architecture is route/controller/model/config based, without service or application layers. Controllers directly perform HTTP response formatting, input parsing, data access, password hashing, Cloudinary upload orchestration, and transaction handling. The codebase uses two database access styles:

- Raw MySQL through `src/config/db.js` (`mysql2` promise pool), used by auth, users, estados, and checklist items.
- Sequelize 6 models through `src/config/sequelize.js`, used by most catalog, plan, operation, measurement, and exploration aggregate flows.

The migration should therefore be treated as a compatibility migration first, not a domain optimization. A .NET 10 backend can become enterprise-grade through a better solution structure, dependency injection, configuration, typed boundaries, OpenAPI generation, and characterization tests, while initially preserving route paths, response shapes, table names, payload quirks, and existing storage behavior.

### Current Architecture and Module Boundaries

- **HTTP entrypoint**: `src/app.js` starts the server, mounts `/api`, and serves Swagger UI from `swagger.json`.
- **Route composition**: `src/routes/index.js` maps route modules to public API prefixes. This file defines the migration route surface and must be treated as a contract until the frontend/client usage is verified.
- **Controllers**: `src/controllers/*.js` contain request handling, validation, persistence, JSON transformation, and response formatting. There is no separate service layer.
- **Models**: `src/models/*.js` define Sequelize table mappings. Many table names are explicit and case-sensitive, especially operation tables such as `Operacion_tal_largo`.
- **Database config**: `src/config/db.js` creates a raw MySQL pool; `src/config/sequelize.js` creates a Sequelize connection and runs connection diagnostics on import.
- **Auth boundary**: `src/controllers/authController.js` validates `codigo_dni` and bcrypt password, then issues a JWT valid for 3 hours. `src/middleware/auth.js` accepts either raw token or `Bearer <token>` in `Authorization`.
- **Upload boundary**: `src/config/upload.js`, `uploadPdfOperaciones.js`, and `uploadSostenimiento.js` configure Cloudinary-backed multer storage. User signature uploads are used in `UsuarioController`.
- **Operations boundary**: `src/routes/api/operaciones.js` dispatches by `tipo` to `src/models/indexOperaciones.js`, which maps 9 operation keys to Sequelize models.
- **Exploration aggregate boundary**: `src/controllers/NubeDatosTrabajoExploracionesConstroller.js` handles a multi-table aggregate with Sequelize transactions and nested associations.

### APIs, Routes, Controllers, and Models That Define the Migration Surface

The route scan found 121 route verb registrations. Public compatibility should initially preserve the following prefixes and controller boundaries:

| Prefix under `/api` | Route module | Controller | Main model/table surface | Notes |
|---|---|---|---|---|
| `/auth` | `authRoutes.js` | `authController.js` | raw `usuarios` query | `POST /login`, bcrypt + JWT, unprotected by design. |
| `/usuarios` | `UsuarioRoutes.js` | `UsuarioController.js` | raw `usuarios` + Sequelize `Usuario` | Nested paths like `/usuarios/usuarios`, `/usuarios/perfil`, `/usuarios/:id/firma`; protected inside controller arrays, not route declarations. |
| `/Equipo` | `equipoRoutes.js` | `equipoController.js` | `Equipo` | Protected CRUD plus `/proceso/:proceso`. Mixed-case route prefix. |
| `/estado` | `estadoRoutes.js` | `estadoController.js` | raw `estados` | Protected CRUD plus `/proceso/:proceso`. |
| `/TipoPerfpo` | `tipoPerforacionRoutes.js` | `tipoPerforacionController.js` | `TipoPerforacion` | Protected CRUD. Typo-like public prefix must be preserved initially. |
| `/PlanMensual` | `planMensualRoutes.js` | `planMensualController.js` | `PlanMensual` | Protected CRUD plus `/anio/:anio/mes/:mes`. |
| `/PlanMetraje` | `planMetrajeRoutes.js` | `planMetrajeController.js` | `PlanMetraje` | Protected CRUD plus `/anio/:anio/mes/:mes`. |
| `/PlanProduccion` | `planProduccionRoutes.js` | `planProduccionController.js` | `PlanProduccion` | Unprotected CRUD plus `/anio/:anio/mes/:mes`. |
| `/fechas-plan-mensual` | `fechasPlanMensualroutes.js` | `fechasPlanMensualcontroller.js` | `FechasPlanMensual` | Unprotected get/create/latest. |
| `/check-list` | `checklistItemRoutes.js` | `checklistItemController.js` | raw `checklist_items` | Unprotected CRUD. |
| `/tipo-equipos` | `tipoEquipoRoutes.js` | `tipoEquipoController.js` | `TipoEquipo` | Unprotected CRUD. |
| `/checklists-telemando` | `checklistTelemandoRoutes.js` | `checklistTelemandoController.js` | `ChecklistTelemando` | Protected CRUD. |
| `/secciones` | `SeccionRuta.js` | `SeccionController.js` | `Seccion` | Protected CRUD plus `/proceso/:proceso`. |
| `/operaciones` | `operaciones.js` | `operaciones.js` controller | 9 operation models | Dynamic by `tipo`; create/update are currently unprotected. |
| `/longitud-barras` | `longitudBarrasRoutes.js` | `longitudBarrasController.js` | `LongitudBarras` | Protected CRUD. |
| `/pernos` | `pernosRoutes.js` | `pernosController.js` | `Pernos` | Protected CRUD. |
| `/mallas` | `mallasRoutes.js` | `mallasController.js` | `Mallas` | Protected CRUD. |
| `/origen-destino` | `origenDestinoRoutes.js` | `origenDestinoController.js` | `OrigenDestino` | Protected CRUD. |
| `/Accesorios` | `accesorioRoutes.js` | `accesorioController.js` | `Accesorio` | Protected CRUD. Mixed-case route prefix. |
| `/Explo-uni` | `explisivosUniRouter.js` | `explisivosUniController.js` | `Explosivo_uni` / `explisivos_uni` | Protected CRUD; spelling is part of the current public surface. |
| `/Explosivos` | `explosivoRoutes.js` | `explosivoController.js` | `Explosivo` | `GET /` is unprotected; other operations protected. |
| `/NubeDatosExploraciones` | `NubeDatosTrabajoExploracionesRoutes.js` | `NubeDatosTrabajoExploracionesConstroller.js` | 7-table aggregate | Create and update-like actions are currently unprotected; reads protected. |
| `/medicion-tal-horizontal` | `medicionesHorizontalRoutes.js` | `medicionesHorizontalController.js` | `MedicionesHorizontal` | Duplicate `GET /` registrations; the second handler is shadowed. Create is unprotected. |
| `/n-retardos` | `numero_retardosRoutes.js` | `numero_retardosController.js` | `NumeroRetardos` | Protected list/create/update/delete. |

### Database Access Patterns and .NET 10 Migration Implications

- **Existing schema is the contract**: Sequelize migrations define tables for users, catalogs, plans, operation tables, exploration aggregate tables, measurements, explosives, accessories, and retardos. The .NET migration should not redesign schema upfront.
- **Dual data access should become explicit**: In .NET 10, use a database-first mapping strategy. EF Core is appropriate for Sequelize-style CRUD and aggregate relationships; Dapper or carefully mapped EF queries can mirror raw SQL controllers where response parity matters.
- **Table naming must be exact**: Operation table names use case-sensitive mixed names like `Operacion_tal_largo`; many catalog tables use snake_case. This matters on Linux/MySQL deployments.
- **JSON behavior is mixed**: Some fields are Sequelize `JSON`, while operation models store structured payloads in `TEXT` (`registros`, `horometros`, `check_list`, etc.) and parse strings back to objects on read. Initial .NET DTOs should preserve current output conversion before any normalization.
- **Transactions matter in exploration aggregate**: `NubeDatosTrabajoExploracionesConstroller.js` creates parent/child records in a Sequelize transaction. This should be a later, focused migration slice with explicit transaction tests.
- **No automated test safety net exists**: Before replacing endpoints, create characterization checks against Node responses for selected routes and use them to validate .NET parity.
- **Migration ownership must be decided**: Current schema migrations are Sequelize CLI files. Proposal should decide whether .NET will initially consume the existing schema only, then introduce EF migrations later, or whether schema evolution remains in Sequelize until cutover.

### Authentication, Uploads, Swagger/OpenAPI, and Deployment Considerations

#### Authentication

- Preserve `POST /api/auth/login` request body: `codigo_dni`, `password`.
- Preserve JWT payload fields initially: `id`, `codigo_dni`, `apellidos`, `nombres`.
- Preserve 3-hour token expiry unless product/security approves a change.
- Preserve support for `Authorization: Bearer <token>`; decide whether to also preserve raw-token Authorization header compatibility.
- Validate bcrypt compatibility with existing hashes before rollout.
- Route protection is inconsistent today. Tightening unprotected endpoints is a security improvement, but it changes behavior and needs explicit product approval.

#### Uploads

- Preserve Cloudinary folders and URL storage for user signatures: `firmas` and stored `firma` URL.
- If migrating upload endpoints early, use a typed Cloudinary abstraction in Infrastructure and ASP.NET Core multipart handling.
- Current `UsuarioController` references `cloudinary.uploader.destroy(...)` without importing `cloudinary`; the migration can safely fix the internal dependency, but response behavior should remain compatible.

#### Swagger/OpenAPI

- Current `swagger.json` is generated from `swagger.js` and served at `/docs`. It is Swagger 2.0-style output and contains broad inferred schemas.
- The .NET backend should generate OpenAPI from controllers/minimal endpoints and serve Swagger UI. Keep `/docs` or provide a redirect if existing consumers expect it.
- Do not treat generated Swagger as the sole source of truth; route files and controllers are more reliable for behavior.

#### Deployment

- Current deployment is Vercel `@vercel/node` targeting `src/app.js`. A .NET 10 ASP.NET Core app needs a new hosting decision: container, Azure App Service, IIS, Render/Fly, or another platform.
- Secrets must move to environment/secret management. Do not carry plaintext DB credentials forward.
- Decide whether Node and .NET run side-by-side during migration via a strangler/proxy approach or whether cutover happens by environment.

### Approaches

1. **Big-bang replacement** — Build the full .NET 10 API, then switch clients from Node to .NET in one cutover.
   - Pros: One final architecture, no long-lived parallel runtime.
   - Cons: High parity risk, no automated test net, difficult rollback, large review workload, likely exceeds 400-line review budget many times.
   - Effort: High

2. **Compatibility-first vertical slices** — Build a .NET 10 solution and migrate route groups one slice at a time, preserving public contracts while improving internal structure.
   - Pros: Lower risk, supports characterization checks, can keep Node as fallback, allows reviewable work units, matches the instruction to avoid optimizing critical internals upfront.
   - Cons: Requires temporary coexistence/routing plan and careful contract tracking.
   - Effort: Medium

3. **Schema/domain redesign first** — Normalize operation payloads, unify auth/authorization, redesign tables, then expose new .NET APIs.
   - Pros: Best long-term architecture if product can absorb breaking changes.
   - Cons: Violates compatibility-first migration goal, high product risk, requires frontend and data migration work before value is delivered.
   - Effort: High

### Recommendation

Use **Approach 2: Compatibility-first vertical slices**.

Create an enterprise-grade .NET 10 solution structure, but migrate behavior incrementally:

- `Api` layer: ASP.NET Core endpoints/controllers, OpenAPI, auth middleware, request/response compatibility.
- `Application` layer: use cases per route group, validation, response orchestration.
- `Domain` layer: minimal domain models only where useful; do not force deep domain modeling for JSON/text operation internals yet.
- `Infrastructure` layer: PostgreSQL target access via EF Core/Npgsql, legacy MySQL source/reference mapping, Cloudinary adapter, password/JWT services, repository/query implementations.

Preserve route paths, payload names, status codes where known, response envelopes, table names, JSON/text conversion, and Cloudinary URL storage. Improve only the migration scaffolding and low-risk flow issues: centralized configuration, secret handling, dependency injection, OpenAPI generation, typed DTOs, error-handling consistency, and characterization tests.

### Recommended First Migration Slice

Start with **Foundation + Auth/Profile + one simple protected catalog read/write**:

1. Build the .NET 10 solution skeleton with configuration, PostgreSQL connectivity, Swagger UI, health endpoint, JWT validation, and deployment-ready environment binding.
2. Implement `POST /api/auth/login` with existing `usuarios` table, bcrypt verification, same JWT payload, and 3-hour expiry.
3. Implement `GET /api/usuarios/perfil` to prove authenticated user context and raw/EF database mapping compatibility.
4. Implement a small protected catalog route such as `/api/tipo-equipos` or `/api/longitud-barras` to validate CRUD patterns without Cloudinary or nested aggregate complexity.
5. Add characterization checks comparing Node vs .NET responses for these endpoints.

This slice proves the highest-leverage cross-cutting requirements: PostgreSQL target connectivity, legacy MySQL migration/reference awareness, JWT, bcrypt compatibility, route preservation, OpenAPI, and deployability, while avoiding the complex dynamic operations and exploration aggregate until the foundation is trustworthy.

### What to Preserve Initially

- Existing `/api` route prefixes, including casing and typo-like names (`/Equipo`, `/TipoPerfpo`, `/Explo-uni`, `/usuarios/usuarios`).
- Existing request/response payload field names in Spanish.
- Existing auth contract: `codigo_dni` login, bcrypt password verification, JWT payload, 3-hour expiry.
- Existing MySQL schema, table names, and data as legacy source/reference material while PostgreSQL becomes the .NET target database.
- Current operation `tipo` keys: `tal_largo`, `tal_horizontal`, `empernador`, `carguio`, `rompebanco`, `scissor`, `anfochanger`, `scalamin`, `dumper`.
- Current operation JSON/text behavior and response parsing.
- Cloudinary folder names and stored URLs.
- Swagger/docs availability, preferably under `/docs`.
- Current protection/unprotection behavior unless product approves security hardening.

### What to Improve During Migration When Justified

- Use .NET dependency injection, options/configuration binding, and secret management.
- Add a clear solution boundary: API/Application/Domain/Infrastructure.
- Add typed DTOs and validation at the API boundary without changing payload names.
- Add centralized error handling while preserving response compatibility for migrated endpoints.
- Add characterization tests/manual contract checks before each slice cutover.
- Fix internal dependency issues such as the missing Cloudinary import equivalent during upload migration.
- Document and optionally correct unreachable/ambiguous routes only with explicit approval, especially the duplicate `GET /` in mediciones horizontal.
- Move from generated Swagger 2.0 artifacts to source-generated OpenAPI, while maintaining `/docs` access.

### Risks

- **No automated tests**: parity regressions are likely without characterization checks.
- **Inconsistent auth protection**: securing currently unprotected endpoints may break clients; preserving them carries security risk.
- **Route compatibility quirks**: mixed casing, typos, nested paths, and duplicate routes may be client dependencies.
- **Dual data access**: raw SQL and Sequelize behaviors must be mapped intentionally in .NET.
- **JSON/text payloads**: operation records store structured data inconsistently; premature normalization could break clients.
- **Cloudinary upload behavior**: user signature storage/deletion has fragile existing behavior and an existing missing import issue.
- **Deployment change**: Vercel Node deployment does not directly translate to ASP.NET Core hosting.
- **Migration ownership**: Sequelize migrations vs .NET migrations must be decided before schema changes.
- **Bcrypt/JWT compatibility**: existing hashes and token expectations must be validated with real data and clients.

### Open Questions Before Proposal

1. Should the .NET API preserve every existing route path exactly, including casing and typo-like names, or may proposal include compatibility redirects/renames?
2. Should currently unprotected endpoints remain unprotected for compatibility, or should the migration include security hardening?
3. What hosting target should replace Vercel Node for .NET 10: container, Azure App Service, IIS, or another platform?
4. Will Node and .NET run side-by-side during migration, or is a single cutover expected?
5. Who owns future database migrations after cutover: .NET EF migrations, continued Sequelize CLI, or SQL migration scripts?
6. Will migration validation use a cloned/staged legacy MySQL source, a PostgreSQL dataset already loaded from MySQL, or both?
7. Which frontend/client flows are most critical and must be migrated first?
8. Are current response status codes and error message texts considered strict contracts?
9. Should the duplicate `GET /api/medicion-tal-horizontal/` behavior be preserved as-is or fixed with a distinct route/filter?
10. Should operation JSON/text internals remain as-is for the first release, or is there appetite for gradual typed normalization after parity?
11. Are Cloudinary uploads still required in the .NET version, and should folders/public IDs remain exactly the same?
12. What acceptance evidence is required for proposal: manual API checks, Postman collection, generated OpenAPI diff, or automated integration tests?

### Ready for Proposal

**Yes, with questions.** The codebase has enough explored structure to create an SDD proposal for a compatibility-first .NET 10 migration. The orchestrator should ask the open questions above before proposal, especially around route compatibility, auth hardening, deployment target, coexistence/cutover strategy, and database migration ownership.
