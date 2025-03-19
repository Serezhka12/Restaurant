using Application.Staff.Queries.GetAllStaff;
using MediatR;

namespace Api.Endpoints.Staff;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Staff.Base, async (ISender sender, CancellationToken cancellationToken) =>
        {
            var staff = await sender.Send(new GetAllStaffQuery(), cancellationToken);
            return Results.Ok(staff);
        }).WithTags(Tags.Staff);
    }
} 