# Employee Management System - Backend

A full-stack Employee & Family Registry system built with .NET 10 and PostgreSQL for managing employee profiles and family relationships in Bangladesh context.

## 🚀 Features

- ✅ Employee CRUD operations
- ✅ Family management (Spouse & Children)
- ✅ Global search (Name, NID, Department)
- ✅ PDF export (Employee list & Individual CV)
- ✅ Role-based authentication (Admin/Viewer)
- ✅ BD-specific validation (NID, Phone format)
- ✅ Auto-migration and seed data

## 📋 Prerequisites

- .NET 10 SDK
- PostgreSQL 14+
- Node.js 18+ (for frontend)

## 🛠️ Backend Setup

### 1. Install PostgreSQL

Make sure PostgreSQL is running on `localhost:5432` with:
- Username: `postgres`
- Password: `postgres`

Or update the connection string in `API/appsettings.json`

### 2. Restore Dependencies

```powershell
cd BACKEND
dotnet restore
```

### 3. Run Migrations

```powershell
dotnet ef database update --project Infrastructure --startup-project API
```

This will:
- Create the database `EmployeeManagementDB`
- Create all tables
- Seed 10 initial employees with families

### 4. Run the Backend

```powershell
cd API
dotnet run
```

Backend will run on: `http://localhost:5000` or `https://localhost:5001`

Swagger UI: `https://localhost:5001/swagger`

## 🔐 Authentication

### Test Accounts

**Admin Account:**
- Username: `admin`
- Password: `admin123`
- Permissions: Full CRUD access

**Viewer Account:**
- Username: `viewer`
- Password: `viewer123`  
- Permissions: Read-only access

### Login

POST `/api/auth/login`
```json
{
  "username": "admin",
  "password": "admin123"
}
```

Returns JWT token - use in `Authorization: Bearer <token>` header

## 📚 API Endpoints

### Employee Management

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| GET | `/api/employees` | Get all employees | - |
| GET | `/api/employees/{id}` | Get employee by ID | - |
| GET | `/api/employees/search?q={query}` | Search employees | - |
| POST | `/api/employees` | Create employee | Admin |
| PUT | `/api/employees/{id}` | Update employee | Admin |
| DELETE | `/api/employees/{id}` | Delete employee | Admin |

### PDF Export

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/employees/export/pdf` | Export all as PDF |
| GET | `/api/employees/export/pdf?q={query}` | Export filtered as PDF |
| GET | `/api/employees/{id}/cv/pdf` | Export employee CV |

## 🏗️ Project Structure

```
BACKEND/
├── Domain/                 # Entities and enums
│   ├── Entities/
│   │   ├── Employee.cs
│   │   ├── Spouse.cs
│   │   └── Child.cs
│   └── Enums/
│       └── UserRole.cs
├── Application/            # DTOs, interfaces, validators
│   ├── DTOs/
│   ├── Interfaces/
│   └── Validators/
├── Infrastructure/         # Data access, services
│   ├── Data/
│   │   ├── ApplicationDbContext.cs
│   │   └── DataSeeder.cs
│   └── Services/
│       ├── EmployeeService.cs
│       └── PdfService.cs
└── API/                    # Controllers, startup
    ├── Controllers/
    │   ├── EmployeesController.cs
    │   └── AuthController.cs
    └── Program.cs
```

## ✅ Validation Rules

### NID (National ID)
- Must be unique (for both employees and spouses)
- Must be exactly 10 OR 17 digits
- Only numeric characters

### Phone Number
- Must be in Bangladesh format:
  - `+8801XXXXXXXXX` (14 characters) OR
  - `01XXXXXXXXX` (11 characters)

### Salary
- Must be greater than 0

## 🗄️ Database Schema

```
Employee (1:1) ← Spouse
Employee (1:n) ← Children
```

### Seed Data

10 employees with Bangladeshi names:
- Md. Hasan Ahmed, Moushumi Begum, Tanvir Rahman, etc.
- Departments: Engineering, HR, Finance, Marketing, Operations, IT Support
- Some have spouses and children

## 🔧 Tech Stack

- **Framework**: .NET 10
- **Database**: PostgreSQL + Entity Framework Core
- **Validation**: FluentValidation
- **Authentication**: JWT Bearer
- **PDF Generation**: QuestPDF
- **API Documentation**: Swagger/OpenAPI

## 📝 Sample Request

### Create Employee

```json
POST /api/employees
Authorization: Bearer <admin-token>

{
  "name": "Rahim Uddin",
  "nid": "9988776655",
  "phone": "+8801712341234",
  "department": "Engineering",
  "basicSalary": 55000,
  "spouse": {
    "name": "Sultana Begum",
    "nid": "1122334455"
  },
  "children": [
    {
      "name": "Arafat Rahim",
      "dateOfBirth": "2018-05-15"
    }
  ]
}
```

## 🚧 Troubleshooting

### Port already in use
Change port in `API/Properties/launchSettings.json`

### Database connection failed
- Ensure PostgreSQL is running
- Check connection string in `appsettings.json`

### Migration failed
```powershell
dotnet ef database drop --project Infrastructure --startup-project API
dotnet ef migrations remove --project Infrastructure --startup-project API
dotnet ef migrations add InitialCreate --project Infrastructure --startup-project API
dotnet ef database update --project Infrastructure --startup-project API
```

## 📧 Contact

For issues or questions, contact: hello@fionetix.com

---

**Note**: This is a technical assessment project demonstrating clean architecture, EF Core, authentication, and PDF generation in .NET.
