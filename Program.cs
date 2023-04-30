using EfCoreEmployees.Data;
using Microsoft.EntityFrameworkCore;

namespace EfCoreEmployees
{
    class Program
    {
        // Useful commands:        
        // dotnet add package Microsoft.EntityFrameworkCore.Design
        // dotnet tool install --global dotnet-ef
        // dotnet tool uninstall --global dotnet-ef
        // dotnet ef migrations add InitialCreate

        static void Main(string[] args)
        {
            HealthCheckCanConnect();
        }

        // Creates Database, can use migration, but does not need it
        static void EnsureCreatedDatabase()
        {
            using var db = new EmployeesContext();
            db.Database.EnsureCreated();
        }

        // Deletes the Database
        static void EnsureDeletedDatabase()
        {
            // dotnet ef database drop
            using var db = new EmployeesContext();
            db.Database.EnsureDeleted();
        }

        static void ListsMigrations()
        {
            // Commands:
            // dotnet ef migrations list --context EmployeesContext
            using var db = new EmployeesContext();
            var migrations = db.Database.GetMigrations();
            Console.WriteLine($"Total {migrations.Count()}");
            foreach(var mig in migrations)
            {
                Console.WriteLine(mig);
            }
        }

        static void ApplyPendingMigrations()
        {
            using var db = new EmployeesContext();
            db.Database.Migrate();
        }

        static void HealthCheckCanConnect()
        {
            using var db = new EmployeesContext();
            var canConnect = db.Database.CanConnect();
            Console.WriteLine(canConnect);
        }
    }
}