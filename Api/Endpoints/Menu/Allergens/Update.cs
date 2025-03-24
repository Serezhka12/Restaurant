using Api.Dtos.Menu;
using Application.Menu.Commands.UpdateAllergens;
using MapsterMapper;
using MediatR;

namespace Api.Endpoints.Menu.Allergens;

internal sealed class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(Routes.Menu.AllergenById, async (int id, UpdateAllergensDto dto, IMapper mapper, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = mapper.Map<UpdateAllergensCommand>(dto) with { Id = id };
            await sender.Send(command, cancellationToken);
            return Results.NoContent();
        }).WithTags(Tags.Menu);
    }
} 