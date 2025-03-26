using Application.Common.Interfaces;
using Domain.Entities.Menu;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Menu;

public class MenuPositionRepository(AppDbContext context) : IMenuPositionRepository
{
    public async Task<MenuPosition?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.MenuPositions
            .Include(x => x.MenuCategory)
            .Include(x => x.Allergens)
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<MenuPosition>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.MenuPositions
            .Include(x => x.MenuCategory)
            .Include(x => x.Allergens)
            .Include(x => x.Products)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> AddAsync(MenuPosition position, CancellationToken cancellationToken = default)
    {
        await context.MenuPositions.AddAsync(position, cancellationToken);
        return position.Id;
    }

    public async Task UpdateAsync(MenuPosition position, CancellationToken cancellationToken = default)
    {
        var existingPosition = await context.MenuPositions
            .Include(x => x.Allergens)
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == position.Id, cancellationToken);

        if (existingPosition != null)
        {
            context.Entry(existingPosition).CurrentValues.SetValues(position);

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
            context.MenuPositions.Update(position);
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var position = await context.MenuPositions.FindAsync(new object[] { id }, cancellationToken);
        if (position != null)
        {
            context.MenuPositions.Remove(position);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.MenuPositions.AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<MenuPosition>> GetAllByCategoryIdAsync(int categoryId, CancellationToken cancellationToken)
    {
        return await context.MenuPositions
            .Where(p => p.MenuCategoryId == categoryId)
            .Include(x => x.Allergens)
            .Include(x => x.Products)
            .ToListAsync(cancellationToken);
    }
}