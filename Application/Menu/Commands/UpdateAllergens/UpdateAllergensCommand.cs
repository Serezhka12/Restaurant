using MediatR;

namespace Application.Menu.Commands.UpdateAllergens;

public record UpdateAllergensCommand(
    int Id,
    string Name) : IRequest; 