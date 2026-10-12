## ADDED Requirements

### Requirement: Local PostgreSQL service
The project SHALL provide a Docker Compose PostgreSQL service for local development with persistent data storage and a readiness health check.

#### Scenario: Start local database
- **WHEN** a developer starts the PostgreSQL Compose service
- **THEN** PostgreSQL becomes healthy and its data remains available after the container is stopped and restarted

### Requirement: App PostgreSQL connection
The app SHALL register a PostgreSQL EF Core context using the `ConnectionStrings:DefaultConnection` configuration value, with environment configuration able to override that value.

#### Scenario: Configure database connection
- **WHEN** the app starts with a valid default connection string supplied by configuration
- **THEN** the registered EF Core context uses that PostgreSQL connection

### Requirement: Local setup instructions
The project SHALL document how to start PostgreSQL, configure and run the app, and create database migrations when application entities are introduced.

#### Scenario: Follow local setup
- **WHEN** a developer follows the documented database setup steps
- **THEN** they can start PostgreSQL and run the app connected to it without relying on committed production credentials
