namespace Shared.Dtos.Menu;

public class AllergensDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CreateAllergensDto
{
    public string Name { get; set; } = string.Empty;
}

public class UpdateAllergensDto
{
    public string Name { get; set; } = string.Empty;
} 