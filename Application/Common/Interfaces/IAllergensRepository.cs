using Domain.Entities.Menu;

namespace Application.Common.Interfaces;

public interface IAllergensRepository
{
    Task<Allergens?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Allergens>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<int> AddAsync(Allergens allergen, CancellationToken cancellationToken = default);
    Task UpdateAsync(Allergens allergen, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
} 