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
            .UseSqlServer(cn, options => options
                // How many operations in a single command when SaveChanges is called (default is 42)
                .MaxBatchSize(50)
                // EF's Retry on failure, with many options. Enabling this should, however, be paired with an execution strategy, to avoid problems of data (see AddWithExecutionStrategy())
                .EnableRetryOnFailure(4, TimeSpan.FromSeconds(5), null)
                )
            // To Log the parameter values of the sql commands
            .EnableSensitiveDataLogging()            
            .LogTo(Console.WriteLine, LogLevel.Information);
    }
}
