namespace Domain.Entities.Menu;

public class Allergens
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<MenuPosition> Positions { get; set; }
}