using Application.Common.Interfaces;
using Domain.Entities.Menu;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Menu;

public class AllergensRepository(AppDbContext context) : IAllergensRepository
{
    public async Task<Allergens?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Allergens
            .Include(x => x.Positions)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<Allergens>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Allergens
            .ToListAsync(cancellationToken);
    }

    public async Task<int> AddAsync(Allergens allergen, CancellationToken cancellationToken = default)
    {
        await context.Allergens.AddAsync(allergen, cancellationToken);
        return allergen.Id;
    }

    public async Task UpdateAsync(Allergens allergen, CancellationToken cancellationToken = default)
    {
        context.Allergens.Update(allergen);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var allergen = await context.Allergens.FindAsync(new object[] { id }, cancellationToken);
        if (allergen != null)
        {
            context.Allergens.Remove(allergen);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Allergens.AnyAsync(x => x.Id == id, cancellationToken);
    }
}