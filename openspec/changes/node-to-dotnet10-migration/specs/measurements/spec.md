# Measurements Specification

## Purpose

Define migrated measurement endpoint behavior and persisted measurement contract.

## Requirements

### Requirement: Measurement CRUD

The system MUST provide list, detail, create, update, and delete behavior for horizontal drilling measurements.

#### Scenario: List measurements

- GIVEN persisted measurement records
- WHEN the client requests the measurements collection
- THEN the system MUST return the available records

#### Scenario: Missing measurement

- GIVEN no measurement exists for the requested id
- WHEN the client requests, updates, or deletes it
- THEN the system MUST return a normalized not-found response

### Requirement: Measurement persistence

The system MUST preserve existing measurement fields and persisted values when migrating reads and writes.

#### Scenario: Create measurement

- GIVEN a valid measurement payload
- WHEN the client creates a measurement
- THEN the system MUST persist the measurement and return the saved record

#### Scenario: Invalid measurement payload

- GIVEN a payload with missing or invalid measurement data
- WHEN the client submits it
- THEN the system MUST return a normalized validation error

### Requirement: Ambiguous measurement reads

The system MUST document and resolve approved behavior for previously duplicated measurement collection routes.

#### Scenario: Approved collection behavior

- GIVEN the approved measurement collection contract
- WHEN the collection route is requested
- THEN the system MUST return the documented collection shape

#### Scenario: Deprecated ambiguous behavior

- GIVEN a client depends on an old ambiguous route behavior
- WHEN that behavior is no longer supported
- THEN the system MUST provide a documented compatibility or error response
