using Application.Staff.Commands.CreateStaff;
using MapsterMapper;
using MediatR;
using Shared.Dtos.Staff;

namespace Api.Endpoints.Staff;

internal sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Staff.Base, async (CreateStaffDto dto, IMapper mapper, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = mapper.Map<CreateStaffCommand>(dto);
            var id = await sender.Send(command, cancellationToken);
            return Results.Created($"/{Routes.Staff.Base}/{id}", new { Id = id });
        }).WithTags(Tags.Staff);
    }
} 