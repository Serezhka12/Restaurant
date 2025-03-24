using Application.Common.Interfaces;
using Application.Menu.Contracts;
using Mapster;
using MediatR;

namespace Application.Menu.Queries.GetAllAllergens;

public class GetAllAllergensQueryHandler : IRequestHandler<GetAllAllergensQuery, List<AllergensDto>>
{
    private readonly IAllergensRepository _allergensRepository;

    public GetAllAllergensQueryHandler(IAllergensRepository allergensRepository)
    {
        _allergensRepository = allergensRepository;
    }

    public async Task<List<AllergensDto>> Handle(GetAllAllergensQuery request, CancellationToken cancellationToken)
    {
        var allergens = await _allergensRepository.GetAllAsync(cancellationToken);
        return allergens.Adapt<List<AllergensDto>>();
    }
} 