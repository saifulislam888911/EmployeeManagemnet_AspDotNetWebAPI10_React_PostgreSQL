namespace Domain.Entities;

public class Child
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    
    // Foreign Key
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
}
