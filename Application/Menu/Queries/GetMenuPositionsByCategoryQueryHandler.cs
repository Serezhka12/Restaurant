using Application.Common.Interfaces;
using Application.Menu.Contracts;
using Domain.Entities.Menu;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Menu.Queries.GetMenuPositionsByCategory;

public record GetMenuPositionsByCategoryQuery : IRequest<List<MenuPositionGroupDto>>;

public class GetMenuPositionsByCategoryQueryHandler(
    IMenuPositionRepository menuPositionRepository,
    IMenuCategoryRepository menuCategoryRepository)
    : IRequestHandler<GetMenuPositionsByCategoryQuery, List<MenuPositionGroupDto>>
{
    public async Task<List<MenuPositionGroupDto>> Handle(GetMenuPositionsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await menuCategoryRepository.GetAllAsync(cancellationToken);
        var result = new List<MenuPositionGroupDto>();

        foreach (var category in categories.Where(category => category.IsAvailable))
        {
            var positions = await menuPositionRepository.GetAllByCategoryIdAsync(category.Id, cancellationToken);

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