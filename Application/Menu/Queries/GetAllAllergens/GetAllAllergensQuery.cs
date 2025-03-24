using Application.Menu.Contracts;
using MediatR;

namespace Application.Menu.Queries.GetAllAllergens;

public record GetAllAllergensQuery : IRequest<List<AllergensDto>>; 