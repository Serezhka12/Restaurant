using Application.Common.Interfaces;
using Application.Menu.Contracts;
using Mapster;
using MediatR;
using Shared.Exceptions;

namespace Application.Menu.Queries.GetAllergensById;

public record GetAllergensByIdQuery(int Id) : IRequest<AllergensDto>;

public class GetAllergensByIdQueryHandler(IAllergensRepository allergensRepository)
    : IRequestHandler<GetAllergensByIdQuery, AllergensDto>
{
    public async Task<AllergensDto> Handle(GetAllergensByIdQuery request, CancellationToken cancellationToken)
    {
        var allergen = await allergensRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (allergen == null)
        {
            throw new NotFoundException($"Алерген з ID {request.Id} не знайдений");
        }
        
        return allergen.Adapt<AllergensDto>();
    }
} 