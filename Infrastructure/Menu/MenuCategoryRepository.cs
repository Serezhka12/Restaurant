using Application.Common.Interfaces;
using Domain.Entities.Menu;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Menu;

public class MenuCategoryRepository : RepositoryBase, IMenuCategoryRepository
{
    private readonly AppDbContext _context;

    public MenuCategoryRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<MenuCategory?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.MenuCategories
            .Include(x => x.Positions)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<MenuCategory>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.MenuCategories
            .ToListAsync(cancellationToken);
    }

    public async Task<int> AddAsync(MenuCategory category, CancellationToken cancellationToken = default)
    {
        await _context.MenuCategories.AddAsync(category, cancellationToken);
        await SaveChangesAsync(cancellationToken);
        return category.Id;
    }

    public async Task UpdateAsync(MenuCategory category, CancellationToken cancellationToken = default)
    {
        _context.MenuCategories.Update(category);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var category = await _context.MenuCategories.FindAsync(new object[] { id }, cancellationToken);
        if (category != null)
        {
            _context.MenuCategories.Remove(category);
            await SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.MenuCategories.AnyAsync(x => x.Id == id, cancellationToken);
    }
}