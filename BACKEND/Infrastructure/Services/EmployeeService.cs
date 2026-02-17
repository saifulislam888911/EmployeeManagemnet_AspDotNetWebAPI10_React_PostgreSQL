using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class EmployeeService : IEmployeeService
{
    private readonly ApplicationDbContext _context;

    public EmployeeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
    {
        var employees = await _context.Employees
            .Include(e => e.Spouse)
            .Include(e => e.Children)
            .ToListAsync();

        return employees.Select(MapToDto).ToList();
    }

    public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
    {
        var employee = await _context.Employees
            .Include(e => e.Spouse)
            .Include(e => e.Children)
            .FirstOrDefaultAsync(e => e.Id == id);

        return employee == null ? null : MapToDto(employee);
    }

    public async Task<List<EmployeeDto>> SearchEmployeesAsync(string query)
    {
        var lowerQuery = query.ToLower();
        
        var employees = await _context.Employees
            .Include(e => e.Spouse)
            .Include(e => e.Children)
            .Where(e => e.Name.ToLower().Contains(lowerQuery) ||
                       e.NID.Contains(lowerQuery) ||
                       e.Department.ToLower().Contains(lowerQuery))
            .ToListAsync();

        return employees.Select(MapToDto).ToList();
    }

    public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto)
    {
        // Check NID uniqueness
        var existingNid = await _context.Employees.AnyAsync(e => e.NID == dto.NID);
        if (existingNid)
            throw new InvalidOperationException("NID already exists");

        if (dto.Spouse != null)
        {
            var existingSpouseNid = await _context.Spouses.AnyAsync(s => s.NID == dto.Spouse.NID);
            if (existingSpouseNid)
                throw new InvalidOperationException("Spouse NID already exists");
        }

        var employee = new Employee
        {
            Name = dto.Name,
            NID = dto.NID,
            Phone = dto.Phone,
            Department = dto.Department,
            BasicSalary = dto.BasicSalary,
            Spouse = dto.Spouse != null ? new Spouse
            {
                Name = dto.Spouse.Name,
                NID = dto.Spouse.NID
            } : null,
            Children = dto.Children.Select(c => new Child
            {
                Name = c.Name,
                DateOfBirth = c.DateOfBirth
            }).ToList()
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return await GetEmployeeByIdAsync(employee.Id) 
            ?? throw new InvalidOperationException("Failed to retrieve created employee");
    }

    public async Task<EmployeeDto?> UpdateEmployeeAsync(int id, UpdateEmployeeDto dto)
    {
        var employee = await _context.Employees
            .Include(e => e.Spouse)
            .Include(e => e.Children)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (employee == null) return null;

        // Check NID uniqueness (excluding current employee)
        var existingNid = await _context.Employees.AnyAsync(e => e.NID == dto.NID && e.Id != id);
        if (existingNid)
            throw new InvalidOperationException("NID already exists");

        employee.Name = dto.Name;
        employee.NID = dto.NID;
        employee.Phone = dto.Phone;
        employee.Department = dto.Department;
        employee.BasicSalary = dto.BasicSalary;
        employee.UpdatedAt = DateTime.UtcNow;

        // Update Spouse
        if (dto.Spouse != null)
        {
            if (employee.Spouse == null)
            {
                employee.Spouse = new Spouse
                {
                    Name = dto.Spouse.Name,
                    NID = dto.Spouse.NID,
                    EmployeeId = employee.Id
                };
            }
            else
            {
                employee.Spouse.Name = dto.Spouse.Name;
                employee.Spouse.NID = dto.Spouse.NID;
            }
        }
        else if (employee.Spouse != null)
        {
            _context.Spouses.Remove(employee.Spouse);
        }

        // Update Children - simple approach: remove all and re-add
        _context.Children.RemoveRange(employee.Children);
        employee.Children = dto.Children.Select(c => new Child
        {
            Name = c.Name,
            DateOfBirth = c.DateOfBirth,
            EmployeeId = employee.Id
        }).ToList();

        await _context.SaveChangesAsync();

        return MapToDto(employee);
    }

    public async Task<bool> DeleteEmployeeAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return false;

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return true;
    }

    private static EmployeeDto MapToDto(Employee employee)
    {
        return new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name,
            NID = employee.NID,
            Phone = employee.Phone,
            Department = employee.Department,
            BasicSalary = employee.BasicSalary,
            Spouse = employee.Spouse != null ? new SpouseDto
            {
                Name = employee.Spouse.Name,
                NID = employee.Spouse.NID
            } : null,
            Children = employee.Children.Select(c => new ChildDto
            {
                Name = c.Name,
                DateOfBirth = c.DateOfBirth
            }).ToList()
        };
    }
}
