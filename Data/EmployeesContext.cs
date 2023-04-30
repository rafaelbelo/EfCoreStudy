using EfcoreEmployees.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EfCoreEmployees.Data;

public class EmployeesContext : DbContext
{
    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string cn = "Data source=localhost,1433; Initial Catalog=EmpDb; User ID=sa; Password=SQLp@ssw0rd; Encrypt=false; TrustServerCertificate=false";
        optionsBuilder
            .UseSqlServer(cn)
            // To Log the parameter values of the sql commands
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, LogLevel.Information);
    }
}
