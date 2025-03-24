using Application.Menu.Commands.DeleteMenuCategory;
using MediatR;

namespace Api.Endpoints.Menu.Categories;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(Routes.Menu.CategoryById, async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            await sender.Send(new DeleteMenuCategoryCommand(id), cancellationToken);
            return Results.NoContent();
        }).WithTags(Tags.Menu);
    }
} 