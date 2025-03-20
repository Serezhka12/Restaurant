namespace Domain.Entities.Reservation;

public class TableReservation
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public Table Table { get; set; } = null!;
    public DateTime ReservationDate { get; set; }
    public int NumberOfPeople { get; set; }
} 