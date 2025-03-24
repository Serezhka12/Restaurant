using Application.Menu.Commands.CreateMenuPosition;
using MapsterMapper;
using MediatR;
using Shared.Dtos.Menu;

namespace Api.Endpoints.Menu.Positions;

internal sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Menu.Positions, async (CreateMenuPositionDto dto, IMapper mapper, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = mapper.Map<CreateMenuPositionCommand>(dto);
            var id = await sender.Send(command, cancellationToken);
            return Results.Created($"/{Routes.Menu.Positions}/{id}", new { Id = id });
        }).WithTags(Tags.Menu);
    }
} 