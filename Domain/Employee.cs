namespace EfcoreEmployees.Domain;

public class Employee
{
    public Guid Id { get; set; }
    public string Identification { get; set; } = null!;
    public string Name { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public bool ReceiveOffers { get; set; }

    public Guid DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
}