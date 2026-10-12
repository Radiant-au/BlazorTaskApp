## ADDED Requirements

### Requirement: REST task collection
The app SHALL expose task collection and item operations as JSON REST endpoints under `/api/tasks`, using the existing PostgreSQL-backed task data.

#### Scenario: List tasks
- **WHEN** a client sends `GET /api/tasks`
- **THEN** the app returns the persisted tasks as JSON with their IDs, titles, and completion states

#### Scenario: Read a task
- **WHEN** a client sends `GET /api/tasks/{id}` for an existing task
- **THEN** the app returns that task as JSON

#### Scenario: Read a missing task
- **WHEN** a client sends `GET /api/tasks/{id}` for an unknown ID
- **THEN** the app returns `404 Not Found`

### Requirement: Create and update tasks
The app SHALL allow clients to create and update tasks through the REST API and persist successful changes in the shared database.

#### Scenario: Create a task
- **WHEN** a client sends `POST /api/tasks` with a non-empty title of at most 120 characters
- **THEN** the app persists the task and returns `201 Created` with the created task and its item location

#### Scenario: Reject an invalid task title
- **WHEN** a client sends `POST /api/tasks` or `PUT /api/tasks/{id}` with a missing, empty, whitespace-only, or overlong title
- **THEN** the app returns `400 Bad Request` and does not change stored tasks

#### Scenario: Update a task
- **WHEN** a client sends `PUT /api/tasks/{id}` with a valid title and completion state for an existing task
- **THEN** the app persists both values and returns `204 No Content`

#### Scenario: Update a missing task
- **WHEN** a client sends `PUT /api/tasks/{id}` for an unknown ID
- **THEN** the app returns `404 Not Found`

### Requirement: Delete tasks
The app SHALL allow clients to delete tasks through the REST API.

#### Scenario: Delete an existing task
- **WHEN** a client sends `DELETE /api/tasks/{id}` for an existing task
- **THEN** the app removes it from the database and returns `204 No Content`

#### Scenario: Delete a missing task
- **WHEN** a client sends `DELETE /api/tasks/{id}` for an unknown ID
- **THEN** the app returns `404 Not Found`
