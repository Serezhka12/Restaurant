using Application.Menu.Commands.DeleteAllergens;
using MediatR;

namespace Api.Endpoints.Menu.Allergens;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(Routes.Menu.AllergenById, async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            await sender.Send(new DeleteAllergensCommand(id), cancellationToken);
            return Results.NoContent();
        }).WithTags(Tags.Menu);
    }
} 