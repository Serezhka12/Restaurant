using Application.Menu.Queries.GetAllMenuCategories;
using MediatR;

namespace Api.Endpoints.Menu.Categories;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Menu.Categories, async (ISender sender, CancellationToken cancellationToken) =>
        {
            var categories = await sender.Send(new GetAllMenuCategoriesQuery(), cancellationToken);
            return Results.Ok(categories);
        }).WithTags(Tags.Menu);
    }
} 