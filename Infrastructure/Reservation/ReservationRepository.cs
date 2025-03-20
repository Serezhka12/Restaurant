using Application.Common.Interfaces;
using Domain.Entities.Reservation;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Reservation;

public class ReservationRepository(AppDbContext context) : RepositoryBase(context), IReservationRepository
{
    private readonly AppDbContext _context = context;

    public async Task<TableReservation?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.TableReservations
            .Include(tr => tr.Table)
            .FirstOrDefaultAsync(tr => tr.Id == id, cancellationToken);
    }

    public async Task<List<TableReservation>> GetByTableIdAsync(int tableId, CancellationToken cancellationToken = default)
    {
        return await _context.TableReservations
            .Where(tr => tr.TableId == tableId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TableReservation>> GetByDateAsync(DateTime date, CancellationToken cancellationToken = default)
    {
        return await _context.TableReservations
            .Include(tr => tr.Table)
            .Where(tr => tr.ReservationDate.Date == date.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> AddAsync(TableReservation reservation, CancellationToken cancellationToken = default)
    {
        await _context.TableReservations.AddAsync(reservation, cancellationToken);
        await SaveChangesAsync(cancellationToken);
        return reservation.Id;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var reservation = await _context.TableReservations.FindAsync([id], cancellationToken);
        if (reservation != null)
        {
            _context.TableReservations.Remove(reservation);
            await SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<List<Table>> GetAvailableTablesAsync(int seats, DateTime reservationDate, CancellationToken cancellationToken = default)
    {
        // Get all tables with enough seats
        var tables = await _context.Tables
            .Where(t => t.IsFree && t.Seats >= seats)
            .Include(t => t.Reservations)
            .ToListAsync(cancellationToken);

        // Filter tables that are not reserved on the specified date
        return tables
            .Where(t => t.Reservations.All(r => r.ReservationDate.Date != reservationDate.Date))
            .ToList();
    }
}