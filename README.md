# Library Management System API

A RESTful Web API built with **.NET 10** following **Clean Architecture** principles and the **Repository Pattern**. Built to demonstrate layered architecture, JWT authentication, role-based authorization, and EF Core with SQL Server.

---

## Table of Contents

- [Architecture](#architecture)
- [Project Structure](#project-structure)
- [Tech Stack](#tech-stack)
- [Getting Started](#getting-started)
- [Authentication](#authentication)
- [Roles](#roles)
- [API Endpoints](#api-endpoints)
- [Domain Model](#domain-model)

---

## Architecture

This project follows **Clean Architecture** with a strict dependency rule — dependencies only point inward. The Domain layer has zero external dependencies.

```
LibraryManagementSystem.Api
        │
        ▼
LibraryManagementSystem.Application
        │
        ▼
LibraryManagementSystem.Domain
        ▲
        │
LibraryManagementSystem.Persistence
```

### Dependency Graph

```
Api          →  Application + Persistence
Application  →  Domain
Persistence  →  Domain
Domain       →  nothing
```

---

## Project Structure

```
LibraryManagementSystem.sln
│
├── LibraryManagementSystem.Api
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── BooksController.cs
│   │   ├── BorrowsController.cs
│   │   └── MembersController.cs
│   └── Extensions/
│       ├── DataSeederExtensions.cs
│       └── IdentityExtensions.cs
│
├── LibraryManagementSystem.Application
│   ├── DTOs/
│   │   ├── Auth/
│   │   ├── Books/
│   │   ├── Borrows/
│   │   └── Members/
│   ├── Interfaces/
│   │   ├── IAuthService.cs
│   │   ├── IBookService.cs
│   │   ├── IBorrowService.cs
│   │   ├── IMemberService.cs
│   │   └── ITokenService.cs
│   └── Services/
│       ├── AuthService.cs
│       ├── BookService.cs
│       ├── BorrowService.cs
│       └── MemberService.cs
        └── TokenService.cs
│
├── LibraryManagementSystem.Domain
│   ├── Entities/
│   │   ├── Book.cs
│   │   ├── Borrow.cs
│   │   └── Member.cs
│   └── Interfaces/
│       ├── IBookRepository.cs
│       ├── IBorrowRepository.cs
│       ├── IMemberRepository.cs
│       └── IUnitOfWork.cs
│
└── LibraryManagementSystem.Persistence
    ├── Identity/
    │   └── ApplicationUser.cs
    ├── Repositories/
    │   ├── BookRepository.cs
    │   ├── BorrowRepository.cs
    │   └── MemberRepository.cs
        ├── UnitOfWork.cs
    ├── LibraryDbContext.cs
    └── PersistenceServiceRegistration.cs
```

---

## Tech Stack

| Technology            | Purpose                 |
| --------------------- | ----------------------- |
| .NET 10               | Runtime                 |
| ASP.NET Core Web API  | API framework           |
| Entity Framework Core | ORM                     |
| SQL Server            | Database                |
| ASP.NET Core Identity | User management         |
| JWT Bearer            | Authentication          |
| User Secrets          | Secret management (dev) |

---

## Getting Started

### Prerequisites

- .NET 10 SDK
- SQL Server
- Postman (for testing)

### Setup

**1. Clone the repository**

```bash
git clone https://github.com/yourusername/LibraryManagementSystem.git
cd LibraryManagementSystem
```

**2. Configure the database connection string in `appsettings.json`**

```json
{
	"ConnectionStrings": {
		"LibraryDBCon": "Server=localhost;Database=LibraryDB;Trusted_Connection=True;TrustServerCertificate=True;"
	},
	"Jwt": {
		"Issuer": "LibraryManagementSystem",
		"Audience": "LibraryManagementSystemUsers"
	},
	"AdminSeed": {
		"Email": "admin@example.com",
		"Password": "Admin@123"
	}
}
```

**3. Set the JWT key via User Secrets**

```bash
dotnet user-secrets set "Jwt:Key" "your-secret-key-at-least-32-characters" --project LibraryManagementSystem.Api
```

**4. Apply migrations**

```bash
dotnet ef database update --project LibraryManagementSystem.Persistence --startup-project LibraryManagementSystem.Api
```

**5. Run the project**

```bash
dotnet run --project LibraryManagementSystem.Api
```

On startup, the application automatically seeds:

- Roles: `Admin`, `Librarian`, `Member`
- Default admin user (`admin@example.com`)

---

## Authentication

This API uses **JWT Bearer authentication**. To access protected endpoints:

1. Register or login via the Auth endpoints
2. Copy the `token` from the response
3. Include it in the `Authorization` header of subsequent requests:

```
Authorization: Bearer <your_token>
```

Tokens expire after **1 hour**.

---

## Roles

| Role        | Description                                                      |
| ----------- | ---------------------------------------------------------------- |
| `Admin`     | Full access — manages books, members, roles, and system settings |
| `Librarian` | Manages books and processes borrows/returns                      |
| `Member`    | Can view books and borrow/return for themselves only             |

---

## API Endpoints

### Auth

| Method | Endpoint                | Auth   | Description                   |
| ------ | ----------------------- | ------ | ----------------------------- |
| POST   | `/api/auth/register`    | Public | Register a new member account |
| POST   | `/api/auth/login`       | Public | Login and receive JWT token   |
| PUT    | `/api/auth/update-role` | Admin  | Assign a role to a user       |

#### Register

```json
POST /api/auth/register
{
  "firstName": "James",
  "lastName": "Anderson",
  "email": "james@email.com",
  "password": "Abc@123",
  "phoneNumber": "01711234567"
}
```

#### Login

```json
POST /api/auth/login
{
  "email": "james@email.com",
  "password": "Abc@123"
}
```

#### Assign Role

```json
PUT /api/auth/update-role
{
  "userId": "identity-user-id",
  "role": "Librarian"
}
```

---

### Books

| Method | Endpoint               | Auth                     | Description                     |
| ------ | ---------------------- | ------------------------ | ------------------------------- |
| GET    | `/api/books`           | Admin, Librarian, Member | Get all books                   |
| GET    | `/api/books/available` | All authenticated        | Get books with available copies |
| POST   | `/api/books`           | Admin, Librarian         | Add a new book                  |
| PUT    | `/api/books/{id}`      | Admin, Librarian         | Update a book                   |
| DELETE | `/api/books/{id}`      | Admin                    | Delete a book                   |

#### Add / Update Book

```json
{
	"title": "Clean Code",
	"author": "Robert C. Martin",
	"totalCopies": 4
}
```

---

### Borrows

| Method | Endpoint                   | Auth              | Description              |
| ------ | -------------------------- | ----------------- | ------------------------ |
| GET    | `/api/borrows/overdue`     | Admin, Librarian  | Get all overdue borrows  |
| POST   | `/api/borrows`             | All authenticated | Borrow a book            |
| POST   | `/api/borrows/return`      | All authenticated | Return a book            |
| PUT    | `/api/borrows/{id}/extend` | Admin, Librarian  | Extend a borrow due date |

#### Borrow a Book

```json
POST /api/borrows
{
  "bookId": 1,
  "memberId": 3
}
```

> Members can only borrow for themselves. `memberId` is ignored for Member role and taken from the JWT token instead.

#### Return a Book

```json
POST /api/borrows/return
{
  "bookId": 1,
  "memberId": 3
}
```

#### Extend Due Date

```json
PUT /api/borrows/1/extend
{
  "newDueDate": "2025-03-01T00:00:00"
}
```

---

### Members

| Method | Endpoint                       | Auth             | Description           |
| ------ | ------------------------------ | ---------------- | --------------------- |
| GET    | `/api/members`                 | Admin, Librarian | Get all members       |
| GET    | `/api/members/{id}`            | Admin, Librarian | Get member by ID      |
| PUT    | `/api/members/{id}`            | Admin, Librarian | Update member details |
| PUT    | `/api/members/{id}/deactivate` | Admin            | Deactivate a member   |
| PUT    | `/api/members/{id}/activate`   | Admin            | Activate a member     |

#### Update Member

```json
PUT /api/members/1
{
  "firstName": "James",
  "lastName": "Anderson",
  "email": "james.updated@email.com",
  "phoneNumber": "01711111111"
}
```

---

## Domain Model

### Book

| Field             | Type   | Description               |
| ----------------- | ------ | ------------------------- |
| `Id`              | int    | Primary key               |
| `Title`           | string | Book title                |
| `Author`          | string | Author name               |
| `TotalCopies`     | int    | Total copies owned        |
| `AvailableCopies` | int    | Copies currently on shelf |

**Domain behavior:** `Borrow()` decrements available copies and enforces availability rules. `Return()` increments available copies.

---

### Member

| Field         | Type     | Description       |
| ------------- | -------- | ----------------- |
| `Id`          | int      | Primary key       |
| `FirstName`   | string   | First name        |
| `LastName`    | string   | Last name         |
| `Email`       | string   | Email address     |
| `PhoneNumber` | string   | Phone number      |
| `MemberSince` | DateTime | Registration date |
| `IsActive`    | bool     | Active status     |

**Domain behavior:** Members are soft-deactivated rather than deleted to preserve borrow history.

---

### Borrow

| Field        | Type      | Description                           |
| ------------ | --------- | ------------------------------------- |
| `Id`         | int       | Primary key                           |
| `BookId`     | int       | FK to Book                            |
| `MemberId`   | int       | FK to Member                          |
| `BorrowedAt` | DateTime  | Date borrowed                         |
| `DueDate`    | DateTime  | Return due date (14 days default)     |
| `ReturnedAt` | DateTime? | Date returned (null = still borrowed) |

**Domain behavior:** `Return()` sets `ReturnedAt`. `IsOverdue()` returns true if past due date and not yet returned. `ExtendDueDate()` extends the due date with validation.

---

## Key Design Decisions

**Repository interfaces in Domain** — Domain defines the contract (`IBookRepository`), Persistence implements it (`BookRepository`). This means the domain never depends on EF Core.

**Private setters on entities** — All state changes go through behavioral methods (`Borrow()`, `Return()`, `Deactivate()`), enforcing domain rules and preventing invalid state from outside the entity.

**Unit of Work** — All repository operations within a single use case are committed together via `IUnitOfWork.SaveChangesAsync()`, ensuring atomicity.

**Identity separated from domain** — `ApplicationUser` (Identity) and `Member` (domain entity) are separate classes linked by `MemberId`. This keeps ASP.NET Core Identity out of the Domain layer.
