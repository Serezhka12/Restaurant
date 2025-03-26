using Application.Common.Interfaces;
using Domain.Entities.Reservation;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tables;

public class TableRepository(AppDbContext context) :  ITableRepository
{
    public async Task<Table?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Tables
            .Include(t => t.Reservations)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<List<Table>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Tables
            .Include(t => t.Reservations)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> AddAsync(Table table, CancellationToken cancellationToken = default)
    {
        await context.Tables.AddAsync(table, cancellationToken);
        return table.Id;
    }

    public async Task UpdateAsync(Table table, CancellationToken cancellationToken = default)
    {
        context.Tables.Update(table);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var table = await context.Tables.FindAsync([id], cancellationToken);
        if (table != null)
        {
            context.Tables.Remove(table);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Tables.AnyAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<List<Table>> GetAvailableTablesAsync(int seats, DateTime reservationDate, CancellationToken cancellationToken = default)
    {
        var tables = await context.Tables
            .Where(t => t.IsFree && t.Seats >= seats)
            .Include(t => t.Reservations)
            .ToListAsync(cancellationToken);

        return tables
            .Where(t => t.Reservations.All(r => r.ReservationDate.Date != reservationDate.Date))
            .ToList();
    }

    public async Task<int> AddReservationAsync(TableReservation reservation, CancellationToken cancellationToken = default)
    {
        await context.TableReservations.AddAsync(reservation, cancellationToken);
        return reservation.Id;
    }

    public async Task CancelReservationAsync(int reservationId, CancellationToken cancellationToken = default)
    {
        var reservation = await context.TableReservations.FindAsync([reservationId], cancellationToken);
        if (reservation != null)
        {
            context.TableReservations.Remove(reservation);
        }
    }

    public async Task<TableReservation?> GetReservationByIdAsync(int reservationId, CancellationToken cancellationToken = default)
    {
        return await context.TableReservations
            .Include(tr => tr.Table)
            .FirstOrDefaultAsync(tr => tr.Id == reservationId, cancellationToken);
    }

    public async Task<List<TableReservation>> GetReservationsByDateAsync(DateTime date, CancellationToken cancellationToken = default)
    {
        return await context.TableReservations
            .Include(tr => tr.Table)
            .Where(tr => tr.ReservationDate.Date == date.Date)
            .ToListAsync(cancellationToken);
    }
}