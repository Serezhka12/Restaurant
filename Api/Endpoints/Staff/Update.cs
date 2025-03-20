using Application.Staff.Commands.UpdateStaff;
using MapsterMapper;
using MediatR;
using Shared.Dtos.Staff;

namespace Api.Endpoints.Staff;

internal sealed class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(Routes.Staff.ById, async (int id, UpdateStaffDto dto, IMapper mapper, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = mapper.Map<UpdateStaffCommand>(dto);
            command = command with { Id = id };

            await sender.Send(command, cancellationToken);
            return Results.NoContent();
        }).WithTags(Tags.Staff);
    }
}