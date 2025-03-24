using Application.Common.Interfaces;
using Application.Menu.Contracts;
using Mapster;
using MediatR;
using Shared.Exceptions;

namespace Application.Menu.Queries.GetAllergensById;

public class GetAllergensByIdQueryHandler : IRequestHandler<GetAllergensByIdQuery, AllergensDto>
{
    private readonly IAllergensRepository _allergensRepository;

    public GetAllergensByIdQueryHandler(IAllergensRepository allergensRepository)
    {
        _allergensRepository = allergensRepository;
    }

    public async Task<AllergensDto> Handle(GetAllergensByIdQuery request, CancellationToken cancellationToken)
    {
        var allergen = await _allergensRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (allergen == null)
        {
            throw new NotFoundException($"Алерген з ID {request.Id} не знайдений");
        }
        
        return allergen.Adapt<AllergensDto>();
    }
} 