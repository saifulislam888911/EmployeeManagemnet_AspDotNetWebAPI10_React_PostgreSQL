using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public static class DataSeeder
{
    public static void SeedData(ModelBuilder modelBuilder)
    {
        var employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Md. Hasan Ahmed", NID = "1234567890", Phone = "+8801712345678", Department = "Engineering", BasicSalary = 50000, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Employee { Id = 2, Name = "Moushumi Begum", NID = "9876543210", Phone = "+8801812345678", Department = "Human Resources", BasicSalary = 45000, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Employee { Id = 3, Name = "Tanvir Rahman", NID = "11223344556671234", Phone = "+8801612345678", Department = "Finance", BasicSalary = 52000, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Employee { Id = 4, Name = "Fatima Khatun", NID = "22334455667781234", Phone = "+8801912345678", Department = "Marketing", BasicSalary = 48000, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Employee { Id = 5, Name = "Abdul Karim", NID = "3344556677", Phone = "+8801512345678", Department = "Operations", BasicSalary = 47000, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Employee { Id = 6, Name = "Nasrin Akter", NID = "4455667788", Phone = "+8801412345678", Department = "IT Support", BasicSalary = 46000, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Employee { Id = 7, Name = "Rafiqul Islam", NID = "55667788991121234", Phone = "+8801312345678", Department = "Engineering", BasicSalary = 55000, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Employee { Id = 8, Name = "Sultana Razia", NID = "66778899002231234", Phone = "+8801212345678", Department = "Human Resources", BasicSalary = 44000, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Employee { Id = 9, Name = "Jalal Uddin", NID = "7788990011", Phone = "+8801112345678", Department = "Finance", BasicSalary = 53000, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Employee { Id = 10, Name = "Ayesha Siddika", NID = "8899001122", Phone = "+8801012345678", Department = "Marketing", BasicSalary = 49000, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        };

        modelBuilder.Entity<Employee>().HasData(employees);

        var spouses = new List<Spouse>
        {
            new Spouse { Id = 1, Name = "Rima Ahmed", NID = "1111222233", EmployeeId = 1 },
            new Spouse { Id = 2, Name = "Shakib Rahman", NID = "2222333344", EmployeeId = 2 },
            new Spouse { Id = 3, Name = "Shapla Khatun", NID = "33334444555561234", EmployeeId = 7 }
        };

        modelBuilder.Entity<Spouse>().HasData(spouses);

        var children = new List<Child>
        {
            new Child { Id = 1, Name = "Tanvir Ahmed", DateOfBirth = new DateTime(2015, 3, 15, 0, 0, 0, DateTimeKind.Utc), EmployeeId = 1 },
            new Child { Id = 2, Name = "Ayesha Ahmed", DateOfBirth = new DateTime(2018, 7, 22, 0, 0, 0, DateTimeKind.Utc), EmployeeId = 1 },
            new Child { Id = 3, Name = "Siam Rahman", DateOfBirth = new DateTime(2016, 6, 10, 0, 0, 0, DateTimeKind.Utc), EmployeeId = 2 },
            new Child { Id = 4, Name = "Nusrat Islam", DateOfBirth = new DateTime(2019, 1, 5, 0, 0, 0, DateTimeKind.Utc), EmployeeId = 7 },
            new Child { Id = 5, Name = "Fahim Islam", DateOfBirth = new DateTime(2020, 9, 18, 0, 0, 0, DateTimeKind.Utc), EmployeeId = 7 }
        };

        modelBuilder.Entity<Child>().HasData(children);
    }
}
