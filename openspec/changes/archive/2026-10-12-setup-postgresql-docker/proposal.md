## Why

The app has no persistent database connection, so it cannot store or retrieve application data from PostgreSQL. A local Docker setup and configured app connection provide a repeatable starting point for database-backed features.

## What Changes

- Add a Docker Compose service for PostgreSQL with configurable credentials and persistent storage.
- Configure the Blazor app to connect through a PostgreSQL provider using settings that can be overridden by environment variables.
- Document how to start the database and apply schema changes locally.

## Capabilities

### New Capabilities
- `postgresql-connection`: Run PostgreSQL locally in Docker and connect the app to it with persistent storage and configurable settings.

### Modified Capabilities

## Impact

- `Program.cs`, application configuration, and `MyBlazorApp.csproj` for database registration and provider dependencies.
- New Docker Compose configuration and local setup documentation.
- No existing application data model or database-backed feature is present; this change establishes connectivity and the migration path only.
