using Microsoft.EntityFrameworkCore;
using EmployeesCh12.Models;

namespace EmployeesCh12.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }
        public DbSet<Benefits> Benefits { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentLocation> DepartmentLocations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<DepartmentLocation>().HasKey(d => new { d.DepartmentID, d.LocationID });
            mb.Entity<DepartmentLocation>().HasOne(d1 => d1.Department).WithMany(d => d.DepartmentLocations)
                .HasForeignKey(d1 => d1.DepartmentID);
            mb.Entity<DepartmentLocation>().HasOne(d1 => d1.Location).WithMany(d => d.DepartmentLocations)
                .HasForeignKey(d1 => d1.LocationID);

            mb.Entity<Benefits>().HasOne<Employee>(p => p.Employee).WithOne(s => s.Benefits)
                .HasForeignKey<Employee>(b => b.BenefitID);
            mb.Entity<Employee>().HasOne<Benefits>(p => p.Benefits).WithOne(s => s.Employee)
                .HasForeignKey<Benefits>(e => e.EmployeeID);
        }
    }
}
