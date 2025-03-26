using Application.Common.Interfaces;
using Application.Menu.Contracts;
using Mapster;
using MediatR;

namespace Application.Menu.Queries.GetAllAllergens;

public record GetAllAllergensQuery : IRequest<List<AllergensDto>>;

public class GetAllAllergensQueryHandler(IAllergensRepository allergensRepository)
    : IRequestHandler<GetAllAllergensQuery, List<AllergensDto>>
{
    public async Task<List<AllergensDto>> Handle(GetAllAllergensQuery request, CancellationToken cancellationToken)
    {
        var allergens = await allergensRepository.GetAllAsync(cancellationToken);
        return allergens.Adapt<List<AllergensDto>>();
    }
} 