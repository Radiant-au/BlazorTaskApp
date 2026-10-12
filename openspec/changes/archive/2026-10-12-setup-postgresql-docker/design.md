## Context

The app is a .NET 10 Blazor Server project with no persistence packages, data model, or connection configuration. The change adds local PostgreSQL connectivity without inventing application entities or business behavior.

## Goals / Non-Goals

**Goals:**
- Start a persistent PostgreSQL instance with Docker Compose for local development.
- Register an EF Core PostgreSQL context in the app using a configurable connection string.
- Give developers a clear startup and schema migration workflow.

**Non-Goals:**
- Add domain entities, sample data, or database-backed UI.
- Containerize the Blazor app or define a production hosting architecture.
- Commit production credentials.

## Decisions

- Use the Npgsql EF Core provider so PostgreSQL connectivity and the app's migration path use the standard .NET data access stack. No other provider is needed for this app.
- Add a Compose PostgreSQL service with a named data volume and health check. The app remains a host process, so its local connection uses `localhost` and the published database port.
- Read the connection string from the `ConnectionStrings:DefaultConnection` configuration key, allowing the normal `ConnectionStrings__DefaultConnection` environment variable override. Keep credentials out of committed app settings.
- Register a minimal DbContext as the app's persistence entry point, but add no entity or initial migration until there is an application data model to migrate.

## Risks / Trade-offs

- The database container and app can start at different times → document waiting for the Compose health check before starting the app; database operations still need to handle a stopped database.
- Local Compose credentials are for development only → document that deployments must provide their own secret-managed connection string.
- No entity exists yet, so no schema can be provisioned → document migration generation once a model is added.

## Migration Plan

Start the database with Compose, then run the Blazor app with its connection string supplied through development configuration or environment. Stop the database with Compose while retaining its named volume. Removing the volume is a separate, destructive local reset and is not part of normal startup.

## Open Questions

None for local development. Production deployment and credentials remain deployment-specific.
