using MediatR;

namespace Application.Menu.Commands.DeleteAllergens;

public record DeleteAllergensCommand(int Id) : IRequest; 