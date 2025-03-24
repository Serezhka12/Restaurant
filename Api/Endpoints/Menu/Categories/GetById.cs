using Application.Menu.Queries.GetMenuCategoryById;
using MediatR;

namespace Api.Endpoints.Menu.Categories;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Menu.CategoryById, async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            var category = await sender.Send(new GetMenuCategoryByIdQuery(id), cancellationToken);
            return Results.Ok(category);
        }).WithTags(Tags.Menu);
    }
} 