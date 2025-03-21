namespace Shared.Dtos.Reservation;

public class ReservationDto
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public int TableSeats { get; set; }
    public DateTime ReservationDate { get; set; }
    public int NumberOfPeople { get; set; }
}