# Proposal: Node to .NET 10 Migration

## Intent

Replace the Node.js/Express mining API with an enterprise-grade .NET 10 backend that improves maintainability, security, and OpenAPI while protecting critical flows.

## Scope

### In Scope
- Full Node replacement through reviewable vertical slices, not one oversized PR.
- Hardened authentication and normalized API errors/status codes.
- Specs for users/auth, operations, measurements, reports, API contract/error normalization, and platform foundation.
- Existing legacy MySQL data/schema used as the migration source/reference, with PostgreSQL as the .NET target database.

### Out of Scope
- Long-running side-by-side Node/.NET architecture.
- Upfront schema redesign or operation payload normalization beyond approved cleanup.
- Frontend rewrite.

## Capabilities

### New Capabilities
- `platform-foundation`: .NET 10 solution, configuration, PostgreSQL access via EF Core/Npgsql, OpenAPI, health readiness.
- `users-auth`: login, JWT validation, user profile/access behavior, password compatibility, auth hardening.
- `operations`: dynamic mining operation flows and operation type behavior.
- `measurements`: measurement endpoints and persisted measurement behavior.
- `reports`: reporting/query outputs for operational workflows.
- `api-contract-error-normalization`: route naming cleanup, response/status consistency, compatibility decisions.

### Modified Capabilities
- None; no main OpenSpec specs exist yet.

## Approach

Build a layered ASP.NET Core 10 solution (`Api`, `Application`, `Domain`, `Infrastructure`) and migrate by slices: foundation/auth first, then operations, measurements, and reports. Use characterization checks against Node behavior before cutover. Correct route/API naming and normalize errors with documented compatibility impact.

## Affected Areas

| Area | Impact | Description |
|------|--------|-------------|
| `src/`, `routes/`, `controllers/`, `models/` | Replaced | Behavior source for parity. |
| `.NET solution root` | New | ASP.NET Core backend, services, data access, OpenAPI. |
| `migrations/`, legacy MySQL schema, PostgreSQL target schema | Modified | Legacy data contract informs the PostgreSQL target; schema redesign remains deferred. |
| deployment config | Modified | Replace Vercel Node hosting. |

## Risks

| Risk | Likelihood | Mitigation |
|------|------------|------------|
| Parity regressions without tests | High | Add characterization/API checks per slice. |
| Auth hardening breaks clients | Med | Document changed protection rules. |
| Route cleanup breaks consumers | Med | Spec compatibility and redirects/aliases if needed. |
| Hosting/secret migration issues | Med | Validate config before cutover. |

## Rollback Plan

Keep Node deployable until each replacement slice is accepted. If cutover fails, route traffic back to Node, keep the legacy MySQL source unchanged initially, restore prior auth/deployment settings, and retain Cloudinary folders/URLs.

## Dependencies

- .NET 10 hosting target and secret-management decision.
- Representative legacy MySQL data migrated or staged in PostgreSQL, bcrypt hashes, JWT policy, and critical client workflows.
- Agreement on corrected route names/error semantics.

## Success Criteria

- [ ] Users/auth, operations, measurements, and reports run in .NET 10.
- [ ] Auth is hardened without undocumented breakage.
- [ ] API route/error normalization is specified and verified.
- [ ] Node can be retired after phased acceptance and rollback readiness.
