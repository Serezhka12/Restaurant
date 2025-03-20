using MediatR;

namespace Application.Tables.Commands.DeleteTable;

public record DeleteTableCommand(int Id) : IRequest;