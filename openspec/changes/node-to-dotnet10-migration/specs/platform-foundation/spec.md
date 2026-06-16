# Platform Foundation Specification

## Purpose

Define the baseline platform behavior required for the .NET 10 replacement API before business flows are migrated.

## Requirements

### Requirement: Runtime and API availability

The system MUST provide the mining API as a .NET 10 service under the approved deployment environment and MUST keep `/api` as the base API surface.

#### Scenario: API base is available

- GIVEN the .NET 10 service is running
- WHEN a client calls an implemented `/api` endpoint
- THEN the request is handled by the .NET service

#### Scenario: Unsupported endpoint

- GIVEN the .NET 10 service is running
- WHEN a client calls an unsupported API route
- THEN the system MUST return a normalized not-found response

### Requirement: Configuration and secrets

The system MUST load database, JWT, Cloudinary, and hosting configuration from environment or secret-managed settings and MUST NOT require committed plaintext secrets.

#### Scenario: Valid configuration

- GIVEN required settings are present
- WHEN the service starts
- THEN startup MUST succeed without exposing secret values

#### Scenario: Missing critical configuration

- GIVEN a required setting is missing
- WHEN the service starts or performs the dependent action
- THEN the system MUST fail safely with an operationally clear error

### Requirement: PostgreSQL target data access

The system MUST use PostgreSQL as the .NET target database and MUST treat the existing MySQL schema and persisted data as legacy source/reference material for migration parity.

#### Scenario: Existing data is readable

- GIVEN representative records migrated or staged from the legacy MySQL source into PostgreSQL
- WHEN a migrated endpoint reads those records
- THEN the response MUST reflect the persisted values

#### Scenario: Table or field mismatch

- GIVEN an expected table or field is unavailable
- WHEN a migrated endpoint depends on it
- THEN the system MUST return a normalized server error and log the failure

### Requirement: OpenAPI and health readiness

The system MUST expose OpenAPI documentation and health readiness suitable for migration verification.

#### Scenario: Documentation available

- GIVEN the service is running
- WHEN a client opens `/docs`
- THEN the system MUST provide API documentation for migrated endpoints

#### Scenario: Readiness check

- GIVEN the service can validate its critical dependencies
- WHEN readiness is requested
- THEN the system MUST report ready only when dependencies are usable
