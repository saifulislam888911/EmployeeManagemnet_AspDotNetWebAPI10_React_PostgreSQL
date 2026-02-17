namespace Domain.Entities;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NID { get; set; } = string.Empty; // 10 or 17 digits
    public string Phone { get; set; } = string.Empty; // BD format
    public string Department { get; set; } = string.Empty;
    public decimal BasicSalary { get; set; }
    
    // Relationships
    public Spouse? Spouse { get; set; }
    public List<Child> Children { get; set; } = new();
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
