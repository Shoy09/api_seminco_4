# Operations Specification

## Purpose

Define migrated behavior for dynamic mining operation flows and operation type handling.

## Requirements

### Requirement: Operation type dispatch

The system MUST support the approved operation `tipo` keys: `tal_largo`, `tal_horizontal`, `empernador`, `carguio`, `rompebanco`, `scissor`, `anfochanger`, `scalamin`, and `dumper`.

#### Scenario: Supported operation type

- GIVEN a valid operation `tipo`
- WHEN a client requests operations for that type
- THEN the system MUST query and return records for that operation type

#### Scenario: Unsupported operation type

- GIVEN an unknown operation `tipo`
- WHEN a client requests or writes operations for that type
- THEN the system MUST return a normalized client error

### Requirement: Operation reads

The system MUST provide operation list, by-id, by-jefe, approval, and latest-horometer reads needed by clients.

#### Scenario: Read by id

- GIVEN an existing operation record
- WHEN the client requests it by `tipo` and `id`
- THEN the system MUST return the matching operation

#### Scenario: Missing operation

- GIVEN no operation matches the request
- WHEN the client requests it
- THEN the system MUST return a normalized not-found response

### Requirement: Operation writes

The system MUST create and update operation records while preserving persisted fields and approved auth hardening.

#### Scenario: Create operation

- GIVEN a valid operation payload for a supported `tipo`
- WHEN the client submits the create request
- THEN the system MUST persist and return the created operation

#### Scenario: Invalid operation payload

- GIVEN a payload missing required operation data
- WHEN the client submits the write request
- THEN the system MUST return a normalized validation error

### Requirement: Structured operation payloads

The system MUST preserve current structured payload behavior for operation details stored as JSON or text-backed values.

#### Scenario: Parsed operation details

- GIVEN an operation with stored structured details
- WHEN the record is returned
- THEN the response MUST expose those details in client-consumable structure

#### Scenario: Unparseable stored details

- GIVEN a record with malformed stored detail data
- WHEN the record is read
- THEN the system MUST fail that record safely without corrupting persisted data
