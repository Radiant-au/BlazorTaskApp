## Context

The app already has a PostgreSQL-backed `TodoItem` with `Id`, `Title`, and `IsComplete`, and the Blazor page performs CRUD through `AppDbContext`. The app currently has no HTTP API, and its HTTP launch profile only listens on localhost.

## Goals / Non-Goals

**Goals:**
- Expose the existing task data through a small REST API.
- Let trusted devices on the same Wi-Fi call it using the development computer's IP.
- Keep persistence in the existing EF Core context and PostgreSQL database.

**Non-Goals:**
- Authentication, internet hosting, API versioning, Swagger UI, or a separate API service.
- Changing the task model or existing Blazor page.

## Decisions

- Add ASP.NET Core Minimal API routes under `/api/tasks`: `GET` collection and item, `POST` item, `PUT` item, and `DELETE` item. Return JSON task fields (`id`, `title`, `isComplete`), `404` for missing IDs, `201` with a location for successful creation, and `204` for successful update or deletion.
- Use small request records and validate `title` as non-empty and at most the existing 120-character limit. This prevents clients from changing IDs and keeps the REST contract aligned with `TodoItem`.
- Inject the existing `AppDbContext` into handlers so API writes and the Blazor UI share the same PostgreSQL rows.
- Set the development HTTP launch profile to listen on all interfaces (`0.0.0.0`) at its current port. Document requests as `http://<host-ip>:5297/api/tasks`, using the HTTP profile so local HTTPS redirection does not send clients to a development certificate they cannot trust.
- Keep the API unauthenticated for the requested trusted-LAN development use. Document that anyone who can reach the port can read, add, change, or delete tasks; do not expose it to the public internet.

## Risks / Trade-offs

- [Any device on the reachable LAN can modify tasks] → Document the trusted-network assumption and keep this development-only; add authentication before broader deployment.
- [Host firewall or Wi-Fi client isolation may block access] → Document that the host firewall must allow the selected port and that both devices must be on a network that permits peer connections.
- [HTTP traffic is not encrypted] → Limit the documented use to a trusted local network; use HTTPS with a trusted certificate before use on untrusted networks.

## Migration Plan

No database migration is needed because the API uses the existing `TodoItem` table. Start the app with the HTTP launch profile, find the host computer's local IP, and call `http://<host-ip>:5297/api/tasks`. Rollback by removing the route mappings and LAN binding; persisted tasks remain unchanged.

## Open Questions

None.
