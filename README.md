# ASP.NET Core MVC Project

## Overview
This project is built using ASP.NET Core MVC, targeting .NET 8. The application follows the Onion Architecture, as described in [Code Maze](https://code-maze.com/onion-architecture-in-aspnetcore/).

## Technologies Used

- **ASP.NET Core MVC**: For building the main application framework and handling HTTP requests. [Learn more](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-8.0)
- **Entity Framework Core**: For database access and ORM (Object-Relational Mapping). [Learn more](https://learn.microsoft.com/en-us/ef/core/)
- **Identity Framework** (not yet implemented): For managing user authentication and authorization. [Learn more](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-8.0)
- **FluentAPI**: For configuring domain models in Entity Framework Core. [Learn more](https://learn.microsoft.com/en-us/ef/core/modeling/)
- **FluentValidation**: For validating incoming data to ensure it meets defined rules. [Learn more](https://fluentvalidation.net/)
- **Mapster**: For mapping objects between different layers (e.g., DTOs to ViewModels). [Learn more](https://github.com/MapsterMapper/Mapster)

## Architecture
The project is designed based on the Onion Architecture principles which provide a clear and maintainable structure for your application. For more details, visit the [Code Maze article](https://code-maze.com/onion-architecture-in-aspnetcore/