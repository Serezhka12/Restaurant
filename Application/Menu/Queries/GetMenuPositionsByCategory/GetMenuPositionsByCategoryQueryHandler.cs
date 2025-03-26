using Application.Common.Interfaces;
using Application.Menu.Contracts;
using Domain.Entities.Menu;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Menu.Queries.GetMenuPositionsByCategory;

public class GetMenuPositionsByCategoryQueryHandler : IRequestHandler<GetMenuPositionsByCategoryQuery, List<MenuPositionGroupDto>>
{
    private readonly IMenuPositionRepository _menuPositionRepository;
    private readonly IMenuCategoryRepository _menuCategoryRepository;

    public GetMenuPositionsByCategoryQueryHandler(
        IMenuPositionRepository menuPositionRepository,
        IMenuCategoryRepository menuCategoryRepository)
    {
        _menuPositionRepository = menuPositionRepository;
        _menuCategoryRepository = menuCategoryRepository;
    }

    public async Task<List<MenuPositionGroupDto>> Handle(GetMenuPositionsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await _menuCategoryRepository.GetAllAsync(cancellationToken);
        var result = new List<MenuPositionGroupDto>();

        foreach (var category in categories.Where(category => category.IsAvailable))
        {
            var positions = await _menuPositionRepository.GetAllByCategoryIdAsync(category.Id, cancellationToken);

            var positionDtos = positions.Select(p => new MenuPositionLimitedDto
            {
                Id = p.Id,
                Name = p.Name,
                IsVegan = p.IsVegan,
                Price = p.Price,
                Allergens = p.Allergens.Select(a => a.Name).ToList(),
                Products = p.Products.Select(prod => prod.Name).ToList()
            }).ToList();

            if (positionDtos.Count != 0)
            {
                result.Add(new MenuPositionGroupDto
                {
                    CategoryName = category.Name,
                    Positions = positionDtos
                });
            }
        }

        return result;
    }
}