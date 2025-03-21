using Domain.Entities.Reservation;

namespace Application.Common.Interfaces;

public interface ITableRepository
{
    Task<Table?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Table>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<int> AddAsync(Table table, CancellationToken cancellationToken = default);
    Task UpdateAsync(Table table, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Table>> GetAvailableTablesAsync(int seats, DateTime reservationDate, CancellationToken cancellationToken = default);
    Task<int> AddReservationAsync(TableReservation reservation, CancellationToken cancellationToken = default);
    Task CancelReservationAsync(int reservationId, CancellationToken cancellationToken = default);
    Task<TableReservation?> GetReservationByIdAsync(int reservationId, CancellationToken cancellationToken = default);
    Task<List<TableReservation>> GetReservationsByDateAsync(DateTime date, CancellationToken cancellationToken = default);
}