namespace Application.Interfaces;

public interface IPdfService
{
    byte[] GenerateEmployeeListPdf(List<DTOs.EmployeeDto> employees);
    byte[] GenerateEmployeeCvPdf(DTOs.EmployeeDto employee);
}
