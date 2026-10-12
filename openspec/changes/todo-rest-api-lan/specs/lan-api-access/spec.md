## ADDED Requirements

### Requirement: Development API access over the local network
The development app SHALL listen for HTTP connections on local network interfaces so a device on the same Wi-Fi can reach the task API using the host computer's IP address.

#### Scenario: Access tasks from a same-network device
- **WHEN** the app runs with its documented HTTP development profile and a same-network device requests `http://<host-ip>:5297/api/tasks`
- **THEN** the app returns the task collection, provided the host firewall and network permit the connection

#### Scenario: Explain local API access
- **WHEN** a developer follows the README's API instructions
- **THEN** the README explains how to find the host IP, shows the task endpoint URL format, and notes that the port must be allowed through the host firewall

#### Scenario: Warn about unauthenticated LAN access
- **WHEN** a developer configures or uses the local-network API
- **THEN** the documentation states that reachable devices can read and modify tasks and that this development API is not intended for public internet exposure
