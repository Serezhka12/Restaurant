using Application.Menu.Commands.CreateAllergens;
using MapsterMapper;
using MediatR;
using Shared.Dtos.Menu;

namespace Api.Endpoints.Menu.Allergens;

internal sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Menu.Allergens, async (CreateAllergensDto dto, IMapper mapper, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = mapper.Map<CreateAllergensCommand>(dto);
            var id = await sender.Send(command, cancellationToken);
            return Results.Created($"/{Routes.Menu.Allergens}/{id}", new { Id = id });
        }).WithTags(Tags.Menu);
    }
} 