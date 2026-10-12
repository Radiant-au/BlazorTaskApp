# BlazorTaskApp

A modern Blazor Web application built with **.NET 10** and **PostgreSQL** (Entity Framework Core) featuring a full task management interface.

---

## 🚀 Features

- **Blazor Web Components**: Interactive UI for task tracking (create, update, complete, and delete tasks).
- **PostgreSQL Persistence**: Managed with Entity Framework Core and Npgsql.
- **Docker Compose**: Pre-configured containerized PostgreSQL setup with health checks.
- **Auto-migration / Schema creation**: Database schema is automatically verified and applied on startup.

---

## 🛠️ Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/)
- [Docker](https://www.docker.com/) & Docker Compose

---

## 🏃 Getting Started

### 1. Start the PostgreSQL Container

Run the local PostgreSQL instance via Docker Compose:

```bash
docker compose up -d --wait
```

> **Note**: The default development credentials are user `myblazorapp` with password `local_dev_password`. The database is persisted in the Docker volume `postgres_data`.

### 2. Run the Blazor App

Launch the application:

```bash
dotnet run
```

Navigate to `http://localhost:5297` (or the port specified in the console output).

### 3. Use the task REST API on this device or Wi-Fi

Run the HTTP development profile with `dotnet run --launch-profile http`. The task collection is available at:

```text
http://<host-ip>:5297/api/tasks
```

Replace `<host-ip>` with the computer's local network IPv4 address (on Linux, run `hostname -I`). Devices must be on the same Wi-Fi or local network, and the host firewall must allow inbound TCP port `5297`. This development API has no authentication: anyone who can reach it can read and change tasks. Use it only on a trusted local network; do not expose it to the public internet.

| Method | Path | Action |
| --- | --- | --- |
| `GET` | `/api/tasks` | List tasks |
| `GET` | `/api/tasks/{id}` | Read a task |
| `POST` | `/api/tasks` | Create a task with `{"title":"Example"}` |
| `PUT` | `/api/tasks/{id}` | Replace title and completion state with `{"title":"Example","isComplete":true}` |
| `DELETE` | `/api/tasks/{id}` | Delete a task |

---

## ⚙️ Configuration

- Local database connection settings are configured in [`appsettings.Development.json`](./appsettings.Development.json).
- Base settings reside in [`appsettings.json`](./appsettings.json).
- For non-development environments, use environment variables or secret managers.

---

## 🛑 Stopping the Database

To stop the database container:

```bash
docker compose down
```

To stop and remove database volumes (reset data):

```bash
docker compose down -v
```
