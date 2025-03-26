using Domain.Entities.Menu;

namespace Application.Common.Interfaces;

public interface IMenuCategoryRepository
{
    Task<MenuCategory?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<MenuCategory>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<int> AddAsync(MenuCategory category, CancellationToken cancellationToken = default);
    Task UpdateAsync(MenuCategory category, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
} 