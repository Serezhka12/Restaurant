using MediatR;

namespace Application.Tables.Commands.CreateTable;

public record CreateTableCommand(
    int Seats,
    bool IsFree = true) : IRequest<int>;