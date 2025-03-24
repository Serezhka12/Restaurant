namespace Domain.Entities.Menu;

public class MenuCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsAvailable { get; set; }
    public List<MenuPosition> Positions { get; set; }
}