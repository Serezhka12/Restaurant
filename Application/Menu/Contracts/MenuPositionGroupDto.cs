namespace Application.Menu.Contracts;

public class MenuPositionGroupDto
{
    public string CategoryName { get; set; } = string.Empty;
    public List<MenuPositionLimitedDto> Positions { get; set; } = [];
}

public class MenuPositionLimitedDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsVegan { get; set; }
    public decimal Price { get; set; }
    public List<string> Allergens { get; set; } = [];
    public List<string> Products { get; set; } = [];
}