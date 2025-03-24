using Application.Menu.Contracts;
using MediatR;

namespace Application.Menu.Queries.GetAllergensById;

public record GetAllergensByIdQuery(int Id) : IRequest<AllergensDto>; 