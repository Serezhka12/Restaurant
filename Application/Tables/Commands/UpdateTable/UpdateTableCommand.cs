using MediatR;

namespace Application.Tables.Commands.UpdateTable;

public record UpdateTableCommand(
    int Id,
    int Seats) : IRequest;