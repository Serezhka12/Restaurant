using Application.Menu.Queries.GetAllAllergens;
using MediatR;

namespace Api.Endpoints.Menu.Allergens;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Menu.Allergens, async (ISender sender, CancellationToken cancellationToken) =>
        {
            var allergens = await sender.Send(new GetAllAllergensQuery(), cancellationToken);
            return Results.Ok(allergens);
        }).WithTags(Tags.Menu);
    }
} 