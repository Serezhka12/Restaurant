using MediatR;

namespace Application.Menu.Commands.UpdateMenuPosition;

public record UpdateMenuPositionCommand(
    int Id,
    string Name,
    bool IsVegan,
    bool IsAvailable,
    List<int> AllergenIds,
    int MenuCategoryId,
    List<int> ProductIds) : IRequest; 