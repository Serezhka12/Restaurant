using Application.Common.Interfaces;
using Domain.Entities.Reservation;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Reservation;

public class ReservationRepository(AppDbContext context) :  IReservationRepository
{


    public async Task<TableReservation?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.TableReservations
            .Include(tr => tr.Table)
            .FirstOrDefaultAsync(tr => tr.Id == id, cancellationToken);
    }

    public async Task<List<TableReservation>> GetByTableIdAsync(int tableId, CancellationToken cancellationToken = default)
    {
        return await context.TableReservations
            .Where(tr => tr.TableId == tableId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TableReservation>> GetByDateAsync(DateTime date, CancellationToken cancellationToken = default)
    {
        return await context.TableReservations
            .Include(tr => tr.Table)
            .Where(tr => tr.ReservationDate.Date == date.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> AddAsync(TableReservation reservation, CancellationToken cancellationToken = default)
    {
        await context.TableReservations.AddAsync(reservation, cancellationToken);
        return reservation.Id;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var reservation = await context.TableReservations.FindAsync([id], cancellationToken);
        if (reservation != null)
        {
            context.TableReservations.Remove(reservation);
        }
    }

    public async Task<List<Table>> GetAvailableTablesAsync(int seats, DateTime reservationDate, CancellationToken cancellationToken = default)
    {
        // Get all tables with enough seats
        var tables = await context.Tables
            .Where(t => t.IsFree && t.Seats >= seats)
            .Include(t => t.Reservations)
            .ToListAsync(cancellationToken);

        // Filter tables that are not reserved on the specified date
        return tables
            .Where(t => t.Reservations.All(r => r.ReservationDate.Date != reservationDate.Date))
            .ToList();
    }
}