namespace Domain.Entities.Staff;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Roles Role { get; set; }
    public int Salary { get; set; }

    public ICollection<EmployeeWorkDay> EmployeeWorkDays { get; set; } = new List<EmployeeWorkDay>();
}