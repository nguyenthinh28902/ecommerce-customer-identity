Customer Identity Service

Customer Identity Service là microservice chịu trách nhiệm xác thực và quản lý khách hàng
trong hệ thống thương mại điện tử.

Responsibilities
- Đăng ký / đăng nhập khách hàng
- Phát hành JWT / Refresh Token
- Quản lý trạng thái tài khoản khách hàng
- Cung cấp API xác thực cho các service khác

Architecture
- Microservice
- Clean Architecture

Architecture flow:
API -> Application -> Domain
             ^
      Infrastructure

Solution Structure

CustomerIdentityService/
├─ README.md
├─ .gitignore
├─ Directory.Build.props
├─ CustomerIdentityService.sln
│
├─ src/
│   ├─ CustomerIdentityService.API
│   ├─ CustomerIdentityService.Application
│   ├─ CustomerIdentityService.Domain
│   └─ CustomerIdentityService.Infrastructure
│
└─ test/
    ├─ CustomerIdentityService.Domain.Tests
    └─ CustomerIdentityService.Application.Tests

Tech Stack
- .NET 8 (LTS)
- ASP.NET Core Web API
- Entity Framework Core
- JWT Authentication
- Docker

Run locally
dotnet restore
dotnet build
dotnet run --project src/CustomerIdentityService.API

Run tests
dotnet test

Notes
- Service này chỉ xử lý xác thực khách hàng
- Các service khác không truy cập database, chỉ verify token
