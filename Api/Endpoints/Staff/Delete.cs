using Application.Staff.Commands.DeleteStaff;
using MediatR;

namespace Api.Endpoints.Staff;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(Routes.Staff.ById, async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            await sender.Send(new DeleteStaffCommand(id), cancellationToken);
            return Results.NoContent();
        }).WithTags(Tags.Staff);
    }
} 