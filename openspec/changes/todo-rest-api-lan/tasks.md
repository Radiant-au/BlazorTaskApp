## 1. Add REST task endpoints

- [x] 1.1 Map `GET /api/tasks` and `GET /api/tasks/{id}` to the existing PostgreSQL task data with the specified JSON and missing-item responses
- [x] 1.2 Map `POST /api/tasks` and `PUT /api/tasks/{id}` with title validation, persistence, and the specified success and error responses
- [x] 1.3 Map `DELETE /api/tasks/{id}` with persistence and the specified missing-item response

## 2. Enable and document local network access

- [x] 2.1 Configure the development HTTP launch profile to listen on all interfaces at port 5297
- [x] 2.2 Document how to find the host IP, call `http://<host-ip>:5297/api/tasks`, allow the port through the host firewall, and limit use to a trusted local network

## 3. Verify the API

- [x] 3.1 Build the app and verify REST CRUD behavior, invalid-title handling, and 404 responses
- [ ] 3.2 Verify the API can be reached from another device on the same Wi-Fi using the host IP
