namespace Api.Dtos.Tables;

public class CreateTableDto
{
    public int Seats { get; set; }
    public bool IsFree { get; set; } = true;
}