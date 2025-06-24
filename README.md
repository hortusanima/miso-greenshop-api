# Miso Greenshop Web API
Miso Greenshop is a mock online plant shop application. This repository contains the backend web service (Web APIs) powering the shop, built with .NET C# using Clean/Onion Architecture, the Mediator pattern, and SOLID principles. The APIs are designed to be hosted on Render and connect to a PostgreSQL database hosted on CockroachDB via Entity Framework Core.

## Features
- **Browse Plants:** Search by name, filter by multiple paramteres, and sort plants available in the shop.
- **View Plant Details:** Access detailed information for each plant, including size, category, tags, multiple description types, related plants and user reviews.
- **Shopping Cart:** Add plants to the cart (stored in local storage for guests, synced with the backend for authenticated users).
- **Newsletter Subscription:** Subscribe to be notified when new plants are added (handled via System.Net.Mail and SMTP).
- **User Registration & Authentication:** Register with email, name, and password, with optional newsletter subscription. Secure login returns a JWT token.
- **Purchase Flow:** Authenticated users can "purchase" their cart (mocked; cart is cleared and a confirmation email is sent).
- **Role-based Features:** Logged-in users can leave reviews and access improved sale prices.

## Tech Stack
- **Language:** .NET C# (Core application logic)
- **Architecture:** Clean/Onion architecture (Separation of concerns, scalability, testability)
- **Patterns:** Mediator (MediatR), SOLID (Decoupling, maintainability)
- **ORM:** Entity Framework Core (Data access and mapping)
- **Database:** PostgreSQL (Persistent data storage, hosted on **CockroachDB**)
- **Authentication:** JWT (Secure API access)
- **Email:** System.Net.Mail, SMTP (Sending notifications and confirmations)
- **Hosting:** Render (Cloud deployment)
- **Client App:** React + TypeScript (available [here](https://github.com/Cavrak396/Miso-greenshop-web))

## Solution Structure
- **Domain:** Core entities, model validations and interfaces
- **Application:** Application specific models, queries, commands, handlers and mapping profiles
- **Infrastructure:** Data access (Entity Framework Core), database configuration, migrations and interface implementations
- **Web API**: API endpoints (controllers), data transfer objects (DTOs), action and exception filters and properties

This structure follows Clean Architecture principles, ensuring separation of concerns, testability, and independence from frameworks and databases.

## API Overview
- **Plants:** GET /plants, GET /plants/total-number, GET /plants/category-number, GET /plants/size-number, GET /plants/{plantId}, GET /plants/{plantId}/related, POST /plants (admin key required), PUT /plants (admin key required), DELETE /plants (admin key required)
- **Subscribers:** GET /subscribers (admin key required), POST /subscribers, DELETE /subscribers (admin key required)
- **Users:** GET /users/all (admin key required), GET /users (authentication required) POST /users/register, POST /users/login, POST \users/logout, PUT /users/{isSubscribed} (authentication required), DELETE /users (authentication required)
- **Reviews:** GET /reviews/{plantId}, GET /reviews/{plantId}/user (authentication required), GET /reviews/{plantId}/total-number, GET /reviews/{plantId}/rating-number, POST /reviews (authentication required), PUT /reviews (authentication required), DELETE /reviews (authentication required)
- **Carts:** POST /carts (authentication required), PUT /carts (authentication required)
- **CartItems:** POST /cartitems (authentication required), DELETE /cartitems (authentication required)

Full API documentation is available via Swagger/OpenAPI at /swagger/index.html when running the service. Endpoints that do not require admin key still require application key (security reasons).

## Getting started
### Prerequisites
- .NET 8 SDK
- PostgreSQL database (CockroachDB recommended)
- SMTP credentials for email notifications
- JWT secret, application key and admin key
### Setup
1. Clone the repository
```bash
gh repo clone hortusanima/miso-greenshop-api
```
3. Configure environment variables (e.g. through appsettings.json)
    - Database connection string (_ConnectionStrings:MisoGreenshopManagement_)
    - JWT secret (_Jwt:SecurityKey_)
    - SMTP credentials (_Smtp:Server_, _Smtp:Username_, _Smtp:Password_, _Smtp:Port_, _Smtp:EnableSsl_)
    - Application Key (_PermissionControl:ApplicationKey_)
    - Admin Key (_PermissionControl:AdminKey_)
4. Apply database migrations
```bash
dotnet ef database update
```
4. Run the API
```bash
dotnet run
```
5. Test via Postman, Swagger UI or other testing tool

## Full application
- Explore the live application [here](https://amazing-biscuit-7704bb.netlify.app/).
### Important Note
For optimal cookie setup functionality on mobile devices, please disable the 'Prevent Cross-Site Tracking' option (Safari) or third-party cookie blocking (Chrome and other browsers) in your browser settings.

## Licence
MIT Licence

## Contributing
Contributions are welcome! Please open issues or submit pull requests for improvements or bug fixes.

## Contact
For questions or feedback, please open an issue in this repository.
