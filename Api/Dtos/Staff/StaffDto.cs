using Domain.Entities.Staff;

namespace Shared.Dtos.Staff;

public class StaffDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Roles Role { get; set; }
    public int Salary { get; set; }
    public List<DayOfWeek> WorkDays { get; set; } = new List<DayOfWeek>();
}