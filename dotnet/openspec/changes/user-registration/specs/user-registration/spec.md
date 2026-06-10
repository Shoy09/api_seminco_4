# User Registration Specification

## Purpose

Admin-only `POST /api/auth/register` for provisioning users. Replaces manual DB inserts with: validate uniqueness → BCrypt hash → persist → return `UserProfileResponse`.

## Requirements

### REQ-1: Admin Authorization

Only `rol = "admin"` SHALL access this endpoint.

- GIVEN an authenticated admin user WHEN POST with valid body THEN `200 OK` + created profile
- GIVEN authenticated non-admin (`rol = "trabajador"`) WHEN POST THEN `403 Forbidden`
- GIVEN no bearer token WHEN POST THEN `401 Unauthorized`

### REQ-2: Input Validation

Missing/empty required fields MUST return `400 Bad Request`.

- GIVEN a request with missing `codigo_dni`, `nombres`, `apellidos`, or `password` WHEN POST THEN `400` + `ValidationProblemDetails`

### REQ-3: Uniqueness Constraints

`codigo_dni` MUST be globally unique. `correo` MUST be unique when provided; null correo does not conflict.

- GIVEN existing `codigo_dni = "12345678"` WHEN POST with that same value THEN `409 Conflict`
- GIVEN existing `correo = "a@b.com"` WHEN POST with that same email THEN `409 Conflict`

### REQ-4: Password Hashing

Passwords MUST be BCrypt-hashed (work factor 12) before storage.

- GIVEN password `"secret123"` WHEN persisted THEN column contains `$2a$12$...` hash, not plaintext

### REQ-5: Role Defaulting

Omitted `rol` MUST default to `"trabajador"`.

- GIVEN a request without `rol` WHEN saved THEN stored value is `"trabajador"`

### REQ-6: JWT rol Claim

JWT tokens MUST include the `rol` claim for `[Authorize(Roles = "...")]`.

- GIVEN a user with `rol = "admin"` WHEN `IJwtTokenIssuer.Issue()` is called THEN JWT contains `rol: "admin"`

### REQ-7: Response Shape

Response MUST match `UserProfileResponse` (password hash excluded).

- GIVEN successful registration WHEN endpoint returns THEN `200 OK` + body with `id`, `codigo_dni`, `apellidos`, `nombres`, `cargo`, `empresa`, `guardia`, `autorizado_equipo`, `correo`, `firma`, `rol`, `operaciones_autorizadas`

## API Contract

### POST /api/auth/register

**Request:** `{ codigo_dni, nombres, apellidos, password }` (required) + `{ correo, cargo, area, clasificacion, empresa, guardia, autorizado_equipo, rol }` (optional, snake_case JSON)

**Response 200:** Matches [UserProfileResponse](src/Seminco.Application/Users/UserProfileDtos.cs) shape with all profile fields, no password.

| Code | When |
|------|------|
| 200 | Created successfully |
| 400 | Missing/validation failure |
| 401 | No valid bearer token |
| 403 | Token valid but user is not admin |
| 409 | Duplicate `codigo_dni` or `correo` |

## Validation Rules

| Field | Required | Constraints |
|-------|----------|-------------|
| `codigo_dni` | Yes | Unique, max 50 |
| `nombres` | Yes | Max 200 |
| `apellidos` | Yes | Max 200 |
| `password` | Yes | Min 6 |
| `correo` | No | Unique if non-null, max 200 |
| `cargo`, `area`, `empresa`, `autorizado_equipo` | No | Max 200 each |
| `clasificacion`, `guardia` | No | Max 50 each |
| `rol` | No | Max 20, defaults to `"trabajador"` |

## Database Changes

### Migration: InitialCreate

First EF Core migration. Creates `usuarios` table with columns from `SemincoDbContext` mapping.

**Unique constraints:**

```sql
CREATE UNIQUE INDEX ix_usuarios_codigo_dni ON usuarios (codigo_dni);
CREATE UNIQUE INDEX ix_usuarios_correo ON usuarios (correo) WHERE correo IS NOT NULL;
```

PostgreSQL partial index on `correo` allows multiple NULLs (explicit via EF Core `HasFilter()`).

**BCrypt work factor:** 12, configured at call site in `RegisterService`.
