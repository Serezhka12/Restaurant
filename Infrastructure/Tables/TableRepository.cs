using Application.Common.Interfaces;
using Domain.Entities.Reservation;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Reservation;

public class TableRepository(AppDbContext context) : RepositoryBase(context), ITableRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Table?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Tables
            .Include(t => t.Reservations)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<List<Table>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Tables
            .Include(t => t.Reservations)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> AddAsync(Table table, CancellationToken cancellationToken = default)
    {
        await _context.Tables.AddAsync(table, cancellationToken);
        await SaveChangesAsync(cancellationToken);
        return table.Id;
    }

    public async Task UpdateAsync(Table table, CancellationToken cancellationToken = default)
    {
        _context.Tables.Update(table);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var table = await _context.Tables.FindAsync([id], cancellationToken);
        if (table != null)
        {
            _context.Tables.Remove(table);
            await SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Tables.AnyAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<List<Table>> GetAvailableTablesAsync(int seats, DateTime reservationDate, CancellationToken cancellationToken = default)
    {

        var tables = await _context.Tables
            .Where(t => t.IsFree && t.Seats >= seats)
            .Include(t => t.Reservations)
            .ToListAsync(cancellationToken);

        return tables
            .Where(t => t.Reservations.All(r => r.ReservationDate.Date != reservationDate.Date))
            .ToList();
    }

    public async Task<int> AddReservationAsync(TableReservation reservation, CancellationToken cancellationToken = default)
    {
        await _context.TableReservations.AddAsync(reservation, cancellationToken);
        await SaveChangesAsync(cancellationToken);
        return reservation.Id;
    }

    public async Task CancelReservationAsync(int reservationId, CancellationToken cancellationToken = default)
    {
        var reservation = await _context.TableReservations.FindAsync([reservationId], cancellationToken);
        if (reservation != null)
        {
            _context.TableReservations.Remove(reservation);
            await SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<TableReservation?> GetReservationByIdAsync(int reservationId, CancellationToken cancellationToken = default)
    {
        return await _context.TableReservations
            .Include(tr => tr.Table)
            .FirstOrDefaultAsync(tr => tr.Id == reservationId, cancellationToken);
    }

    public async Task<List<TableReservation>> GetReservationsByDateAsync(DateTime date, CancellationToken cancellationToken = default)
    {
        return await _context.TableReservations
            .Include(tr => tr.Table)
            .Where(tr => tr.ReservationDate.Date == date.Date)
            .ToListAsync(cancellationToken);
    }
}