using Application.Common.Interfaces;
using Domain.Entities.Menu;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Menu;

public class MenuPositionRepository : RepositoryBase, IMenuPositionRepository
{
    private readonly AppDbContext _context;

    public MenuPositionRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<MenuPosition?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.MenuPositions
            .Include(x => x.MenuCategory)
            .Include(x => x.Allergens)
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<MenuPosition>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.MenuPositions
            .Include(x => x.MenuCategory)
            .Include(x => x.Allergens)
            .Include(x => x.Products)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> AddAsync(MenuPosition position, CancellationToken cancellationToken = default)
    {
        await _context.MenuPositions.AddAsync(position, cancellationToken);
        await SaveChangesAsync(cancellationToken);
        return position.Id;
    }

    public async Task UpdateAsync(MenuPosition position, CancellationToken cancellationToken = default)
    {
        var existingPosition = await _context.MenuPositions
            .Include(x => x.Allergens)
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == position.Id, cancellationToken);

        if (existingPosition != null)
        {
            _context.Entry(existingPosition).CurrentValues.SetValues(position);

            existingPosition.Allergens.Clear();
            foreach (var allergen in position.Allergens)
            {
                existingPosition.Allergens.Add(allergen);
            }

            existingPosition.Products.Clear();
            foreach (var product in position.Products)
            {
                existingPosition.Products.Add(product);
            }
        }
        else
        {
            _context.MenuPositions.Update(position);
        }

        await SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var position = await _context.MenuPositions.FindAsync(new object[] { id }, cancellationToken);
        if (position != null)
        {
            _context.MenuPositions.Remove(position);
            await SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.MenuPositions.AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<MenuPosition>> GetAllByCategoryIdAsync(int categoryId, CancellationToken cancellationToken)
    {
        return await _context.MenuPositions
            .Where(p => p.MenuCategoryId == categoryId)
            .Include(x => x.Allergens)
            .Include(x => x.Products)
            .ToListAsync(cancellationToken);
    }
}