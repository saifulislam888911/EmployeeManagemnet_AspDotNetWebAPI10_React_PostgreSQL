using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IPdfService _pdfService;

    public EmployeesController(IEmployeeService employeeService, IPdfService pdfService)
    {
        _employeeService = employeeService;
        _pdfService = pdfService;
    }

    [HttpGet]
    public async Task<ActionResult<List<EmployeeDto>>> GetAll()
    {
        var employees = await _employeeService.GetAllEmployeesAsync();
        return Ok(employees);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EmployeeDto>> GetById(int id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        if (employee == null)
            return NotFound();
        return Ok(employee);
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<EmployeeDto>>> Search([FromQuery] string q)
    {
        if (string.IsNullOrWhiteSpace(q))
            return BadRequest("Search query is required");

        var employees = await _employeeService.SearchEmployeesAsync(q);
        return Ok(employees);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> Create([FromBody] CreateEmployeeDto dto)
    {
        try
        {
            var employee = await _employeeService.CreateEmployeeAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }
        catch (DuplicateNidException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<EmployeeDto>> Update(int id, [FromBody] UpdateEmployeeDto dto)
    {
        try
        {
            var employee = await _employeeService.UpdateEmployeeAsync(id, dto);
            if (employee == null)
                return NotFound();
            return Ok(employee);
        }
        catch (DuplicateNidException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _employeeService.DeleteEmployeeAsync(id);
        if (!result)
            return NotFound();
        return NoContent();
    }

    [HttpGet("export/pdf")]
    public async Task<IActionResult> ExportPdf([FromQuery] string? q)
    {
        var employees = string.IsNullOrWhiteSpace(q)
            ? await _employeeService.GetAllEmployeesAsync()
            : await _employeeService.SearchEmployeesAsync(q);

        var pdf = _pdfService.GenerateEmployeeListPdf(employees);
        return File(pdf, "application/pdf", "employees.pdf");
    }

    [HttpGet("{id:int}/cv/pdf")]
    public async Task<IActionResult> ExportCvPdf(int id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        if (employee == null)
            return NotFound();

        var pdf = _pdfService.GenerateEmployeeCvPdf(employee);
        return File(pdf, "application/pdf", $"{employee.Name}_CV.pdf");
    }
}
