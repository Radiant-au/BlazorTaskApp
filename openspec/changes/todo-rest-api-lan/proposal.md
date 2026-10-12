## Why

The app's task list can be managed in its Blazor page, but other apps and devices have no simple REST interface. Add a small API that shares the existing task data and can be reached by a device on the same Wi-Fi using the host computer's IP address.

## What Changes

- Add REST endpoints to list, read, create, update, and delete tasks.
- Bind the development server to the local network and document the API URL format and required port access.
- Document that this unauthenticated development API is intended for a trusted local network.

## Capabilities

### New Capabilities
- `todo-rest-api`: CRUD access to task items over HTTP REST endpoints.
- `lan-api-access`: Reach the development API from devices on the same local network by host IP.

### Modified Capabilities

## Impact

- ASP.NET Core endpoint registration and Kestrel development launch settings.
- Existing `TodoItem` and PostgreSQL-backed `AppDbContext`.
- README setup instructions; no new package is expected.
