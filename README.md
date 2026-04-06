# Project : Employee-Family-Management-System 
A full-stack application for managing employee profiles and their family details (spouse, children), with features like search, PDF export, and role-based access control. 

### Live Demo (Website) :
Host / Live Link : . 


## Summary & Project Details :
_____________________________

### Employee-Family-Management_AspDotNetCore-WebAPI_EF_React_Project 
- Type: Full-Stack Web Application  
- Architecture: Clean Architecture (Domain, Application, Infrastructure, API)  

## Technology Stack 
Backend : C#, .NET 10, ASP.NET Core, Web API, Entity Framework Core, FluentValidation, QuestPDF .
Frontend : React, Vite, Tailwind CSS, Axios . 
Database : PostgreSQL . 

---

## Version & Dependencies : 
- SDK : Microsoft.NET.Sdk (.NET 10.0) 
- NuGet Package (NuGet Dependency) : 
    i. Microsoft.AspNetCore.OpenApi (v 10.1.3) ; 
    ii. Microsoft.EntityFrameworkCore.Design (v 10.0.3) ;
    iii. FluentValidation.AspNetCore (v 11.3.1) ; 
    iv. Swashbuckle.AspNetCore (v 10.1.3) ;
    v. Microsoft.AspNetCore.Authentication.JwtBearer (v 10.0.3) ;
    vi. QuestPDF (v 2026.2.1) ;
- Node.js (v 18) 
- PostgreSQL (v 14) 

---

# Client : Amber Group : 
Assignment : Test Project According to Provided Requirements ; 
Study : Self-Practice, Self-Study ; 
Helped From : Self-Practice, Self-Study ; 

---

# Branch : ("main")
Main : Stable Release & Version Update (For publishing new updated versions). 
Development (DevWithNotes-00) : For Updating new features. Always Upto Date. Clean Codes (Deleted Comments & Notes). 
DevWithNotes : For Pushing working codes alongside with Comments & Notes. Always Upto Date.

---

# Quick Start Guide :
_____________________

### Step 1: Setup Database

**Option A: Using Local PostgreSQL**
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
