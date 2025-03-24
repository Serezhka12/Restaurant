using MediatR;

namespace Application.Menu.Commands.CreateMenuPosition;

public record CreateMenuPositionCommand(
    string Name,
    bool IsVegan,
    bool IsAvailable,
    List<int> AllergenIds,
    int MenuCategoryId,
    List<int> ProductIds) : IRequest<int>; 