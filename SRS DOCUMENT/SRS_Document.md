# Software Requirements Specification
## Employee & Family Registry

---

## 1. System Scope

**What it does:**
- Manages employee profiles: Name, NID (10 or 17 digits), Phone (BD format), Department, Basic Salary
- Manages family: Spouse (Name, NID), Children (Name, Date of Birth)
- Global search by Name, NID, or Department
- PDF export: filtered list and individual employee CV
- Roles: Viewer (read-only), Admin (Create, Update, Delete)

**What it does not do:**
- No integration with external HR or payroll systems
- No audit trails or change history
- No NID verification against government database
- No bulk import/export

---

## 2. Entity Relationship

```
Employee (1:1) Spouse
Employee (1:N) Children

Employees: Id, Name, NID*, Phone, Department, BasicSalary
Spouses: Id, Name, NID*, EmployeeId (FK)
Children: Id, Name, DateOfBirth, EmployeeId (FK)
* Unique
```

---

## 3. Edge Cases

| Scenario | Handling |
|----------|----------|
| NID duplicate | Returns 400 Bad Request with message |
| Invalid phone | FluentValidation rejects (must be +8801X... or 01X...) |
| Invalid NID | Must be 10 or 17 digits only |
| Empty search | Returns 400 for /search endpoint |
| Child DoB in future | Validation rejects |
| Spouse NID duplicate | Same as employee NID |

---

## 4. Assumptions

1. One spouse per employee
2. NID: 10 digits (old) or 17 digits (new), no verification
3. Phone: BD mobile 013-019
4. Predefined Admin and Viewer accounts in config
5. Case-insensitive search
