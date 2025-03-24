using MediatR;

namespace Application.Menu.Commands.DeleteMenuPosition;

public record DeleteMenuPositionCommand(int Id) : IRequest; 