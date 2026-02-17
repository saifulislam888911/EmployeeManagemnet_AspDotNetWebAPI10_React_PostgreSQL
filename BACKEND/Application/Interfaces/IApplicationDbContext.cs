using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Employee> Employees { get; }
    DbSet<Spouse> Spouses { get; }
    DbSet<Child> Children { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
