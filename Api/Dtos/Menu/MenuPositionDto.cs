namespace Api.Dtos.Menu;

public class CreateMenuPositionDto
{
    public string Name { get; set; } = string.Empty;
    public bool IsVegan { get; set; }
    public bool IsAvailable { get; set; }
    public decimal Price { get; set; }
    public List<int> AllergenIds { get; set; } = [];
    public int MenuCategoryId { get; set; }
    public List<int> ProductIds { get; set; } = [];
}

public class UpdateMenuPositionDto
{
    public string Name { get; set; } = string.Empty;
    public bool IsVegan { get; set; }
    public bool IsAvailable { get; set; }
    public decimal Price { get; set; }
    public List<int> AllergenIds { get; set; } = [];
    public int MenuCategoryId { get; set; }
    public List<int> ProductIds { get; set; } = [];
}