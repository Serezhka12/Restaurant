using Application.Common.Interfaces;
using Domain.Entities.Menu;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Menu;

public class MenuCategoryRepository(AppDbContext context) : IMenuCategoryRepository
{
    public async Task<MenuCategory?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.MenuCategories
            .Include(x => x.Positions)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<MenuCategory>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.MenuCategories
            .ToListAsync(cancellationToken);
    }

    public async Task<int> AddAsync(MenuCategory category, CancellationToken cancellationToken = default)
    {
        await context.MenuCategories.AddAsync(category, cancellationToken);
        return category.Id;
    }

    public async Task UpdateAsync(MenuCategory category, CancellationToken cancellationToken = default)
    {
        context.MenuCategories.Update(category);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var category = await context.MenuCategories.FindAsync(new object[] { id }, cancellationToken);
        if (category != null)
        {
            context.MenuCategories.Remove(category);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.MenuCategories.AnyAsync(x => x.Id == id, cancellationToken);
    }
}