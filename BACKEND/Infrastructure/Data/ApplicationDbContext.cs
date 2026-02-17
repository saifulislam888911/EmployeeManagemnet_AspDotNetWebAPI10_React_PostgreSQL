using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

using Application.Interfaces;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Spouse> Spouses { get; set; }
    public DbSet<Child> Children { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.NID).IsUnique();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.NID).IsRequired().HasMaxLength(17);
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(14);
            entity.Property(e => e.Department).IsRequired().HasMaxLength(100);
            entity.Property(e => e.BasicSalary).HasColumnType("decimal(18,2)");
            entity.HasOne(e => e.Spouse).WithOne(s => s.Employee).HasForeignKey<Spouse>(s => s.EmployeeId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(e => e.Children).WithOne(c => c.Employee).HasForeignKey(c => c.EmployeeId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Spouse>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.HasIndex(s => s.NID).IsUnique();
            entity.Property(s => s.Name).IsRequired().HasMaxLength(200);
            entity.Property(s => s.NID).IsRequired().HasMaxLength(17);
        });

        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(200);
            entity.Property(c => c.DateOfBirth).IsRequired();
        });

        DataSeeder.SeedData(modelBuilder);
    }
}
