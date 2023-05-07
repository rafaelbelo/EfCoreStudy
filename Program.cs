using EfCoreEmployees.Data;
using EfcoreEmployees.Domain;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System;

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
            //EnsureDeletedDatabase();
            //EnsureCreatedDatabase();
            //BatchAddSample();
            QueryTest();
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

        static void AddWithExecutionStrategy()
        {
            using var db = new EmployeesContext();
            var strategy = db.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                using var transaction = db.Database.BeginTransaction();
                db.Departments.Add(new Department { Id = Guid.NewGuid(), Name="Engineering" });
                db.SaveChanges();
                transaction.Commit();
            });        
        }

        static void BatchAddSample()
        {
            using var db = new EmployeesContext();
            var emps = new List<Employee>();
            for (int i = 1; i < 10001; i++)
            {
                emps.Add(new Employee{Id = Guid.NewGuid(), BirthDate = DateTime.Now, Identification = i.ToString(), Name = $"Arnold {i}"});
            }

            db.Departments.Add(new Department { Id = Guid.NewGuid(), Name="Engineering", Employees = emps });
            db.SaveChanges();
        }

        static void QueryTest()
        {
            using var db = new EmployeesContext();
            var emps = new List<Employee>();

            var sw = Stopwatch.StartNew();
            var result = db.Employees.FirstOrDefault(e => e.Name.StartsWith("A"));
            //var result2 = db.Employees.Where(e => e.Name.StartsWith("A")).FirstOrDefault();
            Console.Write(sw.ElapsedMilliseconds);
            sw.Stop();
        }
    }
}