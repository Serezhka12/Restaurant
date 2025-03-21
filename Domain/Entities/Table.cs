namespace Domain.Entities.Reservation;

public class Table
{
    public int Id { get; set; }
    public int Seats { get; set; }
    public bool IsFree { get; set; }
    public ICollection<TableReservation> Reservations { get; set; } = new List<TableReservation>();
}