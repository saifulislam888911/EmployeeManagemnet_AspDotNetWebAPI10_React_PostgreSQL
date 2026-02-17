using Application.DTOs;

namespace Application.Interfaces;

public interface IEmployeeService
{
    Task<List<EmployeeDto>> GetAllEmployeesAsync();
    Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
    Task<List<EmployeeDto>> SearchEmployeesAsync(string query);
    Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto);
    Task<EmployeeDto?> UpdateEmployeeAsync(int id, UpdateEmployeeDto dto);
    Task<bool> DeleteEmployeeAsync(int id);
}
