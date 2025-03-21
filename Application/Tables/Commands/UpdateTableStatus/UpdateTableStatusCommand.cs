using MediatR;

namespace Application.Tables.Commands.UpdateTableStatus;

public record UpdateTableStatusCommand(
    int Id,
    bool IsFree) : IRequest;