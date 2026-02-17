# Employee & Family Registry

Full-stack Employee Management System for Bangladesh. Manages employee profiles and family details (spouse, children) with search, PDF export, and role-based access.

## Prerequisites

- .NET 10 SDK ([Download](https://dotnet.microsoft.com/download))
- Node.js 18+ ([Download](https://nodejs.org/))
- PostgreSQL 14+ ([Download](https://www.postgresql.org/download/))

## Quick Start Guide

### Step 1: Setup Database

**Option A: Using Docker (Recommended)**
```bash
docker run -d --name postgres-employee -e POSTGRES_PASSWORD=1234 -e POSTGRES_DB=EmployeeManagementDB -p 5432:5432 postgres:16
```

**Option B: Using Local PostgreSQL**
1. Open PostgreSQL and create a database named `EmployeeManagementDB`
2. Update the connection string in `BACKEND/API/appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=EmployeeManagementDB;Username=postgres;Password=YOUR_PASSWORD"
}
```

### Step 2: Run Backend

```bash
cd BACKEND/API
dotnet restore
dotnet run
```

The API will start at **http://localhost:5164**  
Swagger UI: **http://localhost:5164/swagger**

Migrations will run automatically on startup and seed 10 employees.

### Step 3: Run Frontend

Open a new terminal:

```bash
cd FRONTEND
npm install
npm run dev
```

The frontend will start at **http://localhost:5173**

## Login Credentials

| Role   | Username | Password   |
|--------|----------|------------|
| Admin  | admin    | admin123   |
| Viewer | viewer   | viewer123  |

**Admin** can create, update, and delete employees.  
**Viewer** has read-only access.

## Features

- Employee management with family details
- Global search by name, NID, or department
- PDF export (employee list and individual CV)
- Role-based authentication (Admin/Viewer)
- Input validation (NID: 10 or 17 digits, Phone: BD format)
- Automatic database seeding

## Project Structure

```
EmployeeManagement/
├── BACKEND/
│   ├── API/              Web API Controllers
│   ├── Application/      DTOs, Validators, Interfaces
│   ├── Domain/           Entities (Employee, Spouse, Child)
│   └── Infrastructure/   DbContext, Migrations, Services
├── FRONTEND/             React + Vite + Tailwind CSS
└── SRS DOCUMENT/         Software Requirements Specification
```

## Technology Stack

**Backend:** .NET 10, EF Core, PostgreSQL, FluentValidation, QuestPDF  
**Frontend:** React, Vite, Tailwind CSS, Axios
