# Reports Specification

## Purpose

Define reporting and query outputs used by operational workflows during the migration.

## Requirements

### Requirement: Period-based plan reports

The system MUST provide year-and-month query outputs for monthly plan, metraje plan, and production plan workflows.

#### Scenario: Existing period data

- GIVEN plan records exist for a requested year and month
- WHEN the client requests that period
- THEN the system MUST return the matching report data

#### Scenario: Empty period

- GIVEN no records exist for a requested year and month
- WHEN the client requests that period
- THEN the system MUST return a documented empty result or not-found response

### Requirement: Operational summary reports

The system MUST provide operational query outputs required by migrated clients, including approval and jefe-filtered operation views.

#### Scenario: Approval report

- GIVEN operations with approval state
- WHEN the client requests approval data for an operation type
- THEN the system MUST return records matching the approval query

#### Scenario: Invalid report filter

- GIVEN an invalid operation type or report filter
- WHEN the client requests the report
- THEN the system MUST return a normalized client error

### Requirement: Report contract consistency

The system MUST preserve approved response fields and ordering semantics where clients depend on them.

#### Scenario: Compatible report shape

- GIVEN a migrated report endpoint
- WHEN the report is requested with valid filters
- THEN the response MUST include the documented fields for that report

#### Scenario: Backend query failure

- GIVEN the report data source fails
- WHEN a report is requested
- THEN the system MUST return a normalized server error without partial misleading data
