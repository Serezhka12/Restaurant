namespace Shared.Dtos.Reservation;

public class CreateTableDto
{
    public int Seats { get; set; }
    public bool IsFree { get; set; } = true;
}