using Domain.Entities.Reservation;

namespace Application.Common.Interfaces;

public interface IReservationRepository
{
    Task<TableReservation?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<TableReservation>> GetByTableIdAsync(int tableId, CancellationToken cancellationToken = default);
    Task<List<TableReservation>> GetByDateAsync(DateTime date, CancellationToken cancellationToken = default);
    Task<int> AddAsync(TableReservation reservation, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Table>> GetAvailableTablesAsync(int seats, DateTime reservationDate, CancellationToken cancellationToken = default);
}