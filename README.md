# Trekker

Trekker is a complete platform for project management, team communication, and collaborative documentation.

## Technologies Used

### Implemented
- **Keycloak**: Authentication and authorization.
- **PostgreSQL + EF Core**: Database management.
- **Clean Architecture**: Structuring the project following best practices.

### In Progress
- **Docker**: Containerization for Keycloak, PostgreSQL, Redis, and application images, managed via Docker Compose.
- **Unit Testing**: Implementing test coverage using **xUnit** and **Moq**.

### Pending
- **AWS S3**: Document, image, and audio file management.
- **Redis**: Caching mechanism for improved performance.
- **Nginx**: Load balancing and reverse proxy.
- **WebSockets & SignalR**: Real-time communication for chat and event handling.

## Setup
1. Clone the repository:
   ```sh
   git clone https://github.com/your-repo/trekker.git
   cd trekker
   ```
2. Set up environment variables in a `.env` file.
3. Start the required services using Docker Compose:
   ```sh
   docker-compose up -d
   ```
4. Run the application.
