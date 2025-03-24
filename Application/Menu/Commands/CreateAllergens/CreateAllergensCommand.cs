using MediatR;

namespace Application.Menu.Commands.CreateAllergens;

public record CreateAllergensCommand(string Name) : IRequest<int>; 