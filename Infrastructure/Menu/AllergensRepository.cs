using Application.Common.Interfaces;
using Domain.Entities.Menu;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Menu;

public class AllergensRepository : RepositoryBase, IAllergensRepository
{
    private readonly AppDbContext _context;

    public AllergensRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Allergens?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Allergens
            .Include(x => x.Positions)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<Allergens>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Allergens
            .ToListAsync(cancellationToken);
    }

    public async Task<int> AddAsync(Allergens allergen, CancellationToken cancellationToken = default)
    {
        await _context.Allergens.AddAsync(allergen, cancellationToken);
        await SaveChangesAsync(cancellationToken);
        return allergen.Id;
    }

    public async Task UpdateAsync(Allergens allergen, CancellationToken cancellationToken = default)
    {
        _context.Allergens.Update(allergen);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var allergen = await _context.Allergens.FindAsync(new object[] { id }, cancellationToken);
        if (allergen != null)
        {
            _context.Allergens.Remove(allergen);
            await SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Allergens.AnyAsync(x => x.Id == id, cancellationToken);
    }
} 