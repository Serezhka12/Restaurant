namespace Domain.Entities.Staff;

public class EmployeeWorkDay
{
    public int EmployeeId { get; set; }

    public Employee Employee { get; set; } = null!;
    public DayOfWeek WorkDay { get; set; }
}