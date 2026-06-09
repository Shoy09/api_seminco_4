# Users and Authentication Specification

## Purpose

Define user, login, token, profile, and authorization behavior for the .NET 10 migration.

## Requirements

### Requirement: Login compatibility and hardening

The system MUST authenticate users with `codigo_dni` and `password`, MUST verify existing password hashes, and MUST reject invalid credentials without disclosing which field failed.

#### Scenario: Valid login

- GIVEN an active user with a valid existing password hash
- WHEN the user submits valid `codigo_dni` and `password`
- THEN the system MUST return a JWT and user identity data

#### Scenario: Invalid credentials

- GIVEN a login request with unknown `codigo_dni` or wrong password
- WHEN authentication is attempted
- THEN the system MUST return a normalized unauthorized response

### Requirement: JWT contract

The system MUST issue and validate JWTs with approved identity claims and a three-hour expiration unless superseded by a documented security decision.

#### Scenario: Valid bearer token

- GIVEN a non-expired JWT
- WHEN a protected endpoint is requested with `Authorization: Bearer <token>`
- THEN the system MUST authorize the request identity

#### Scenario: Expired or malformed token

- GIVEN an expired, missing, or malformed token
- WHEN a protected endpoint is requested
- THEN the system MUST return a normalized unauthorized response

### Requirement: User profile and access

The system MUST expose authenticated profile behavior and user access data needed by migrated clients.

#### Scenario: Profile request

- GIVEN an authenticated user
- WHEN the user requests `/api/usuarios/perfil`
- THEN the system MUST return that user's profile data

#### Scenario: Profile without authentication

- GIVEN no valid authenticated identity
- WHEN `/api/usuarios/perfil` is requested
- THEN the system MUST deny access

### Requirement: User management and signatures

The system MUST preserve user management behavior, including authorized operation assignments and signature URL persistence.

#### Scenario: Signature update

- GIVEN an authorized user update request with a valid signature upload
- WHEN the signature is saved
- THEN the user record MUST store the resulting signature URL

#### Scenario: Invalid user update

- GIVEN a request for a missing user or invalid payload
- WHEN the update is submitted
- THEN the system MUST return a normalized client error
