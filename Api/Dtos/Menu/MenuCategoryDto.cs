namespace Api.Dtos.Menu;


public class CreateMenuCategoryDto
{
    public string Name { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
}

public class UpdateMenuCategoryDto
{
    public string Name { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
}