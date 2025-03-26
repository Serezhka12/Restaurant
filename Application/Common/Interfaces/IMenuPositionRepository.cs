using Domain.Entities.Menu;

namespace Application.Common.Interfaces;

public interface IMenuPositionRepository
{
    Task<MenuPosition?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<MenuPosition>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<int> AddAsync(MenuPosition position, CancellationToken cancellationToken = default);
    Task UpdateAsync(MenuPosition position, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Domain.Entities.Menu.MenuPosition>> GetAllByCategoryIdAsync(int categoryId, CancellationToken cancellationToken);
} 