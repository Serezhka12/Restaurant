using Application.Staff.Queries.GetStaffById;
using MediatR;

namespace Api.Endpoints.Staff;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Staff.ById, async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            var staff = await sender.Send(new GetStaffByIdQuery(id), cancellationToken);
            return Results.Ok(staff);
        }).WithTags(Tags.Staff);
    }
} 