using Application.Menu.Queries.GetAllergensById;
using MediatR;

namespace Api.Endpoints.Menu.Allergens;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Menu.AllergenById, async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            var allergen = await sender.Send(new GetAllergensByIdQuery(id), cancellationToken);
            return Results.Ok(allergen);
        }).WithTags(Tags.Menu);
    }
} 