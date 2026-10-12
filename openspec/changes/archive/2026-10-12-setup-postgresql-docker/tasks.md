## 1. Add local PostgreSQL

- [x] 1.1 Add a Docker Compose PostgreSQL service with a named volume and readiness health check
- [x] 1.2 Document database startup, app connection configuration, local-only credentials, and schema migration steps

## 2. Connect the app

- [x] 2.1 Add the Npgsql EF Core provider and register a minimal DbContext using `ConnectionStrings:DefaultConnection`
- [x] 2.2 Configure a development connection string path that can be overridden through environment variables without committing production secrets
- [ ] 2.3 Verify the app can resolve its database context using the local Compose PostgreSQL service
