using Application.Reservation.Contracts;

namespace Application.Tables.Contracts;

public class TableDto
{
    public int Id { get; set; }
    public int Seats { get; set; }
    public bool IsFree { get; set; }
    public List<ReservationDto> Reservations { get; set; } = new List<ReservationDto>();
}