namespace Application.Menu.Contracts;

public class MenuPositionDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsVegan { get; set; }
    public bool IsAvailable { get; set; }
    public int MenuCategoryId { get; set; }
    public List<int> AllergenIds { get; set; } = [];
    public List<int> ProductIds { get; set; } = [];
} 