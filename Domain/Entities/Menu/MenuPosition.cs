using Domain.Entities.Products;

namespace Domain.Entities.Menu;

public class MenuPosition
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsVegan { get; set; }
    public bool IsAvailable { get; set; }
    public List<Allergens> Allergens { get; set; } = [];
    public MenuCategory MenuCategory { get; set; }
    public int MenuCategoryId { get; set; }
    public List<Product> Products { get; set; } = [];
}