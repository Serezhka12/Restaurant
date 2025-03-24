using Api.Dtos.Menu;
using Application.Menu.Commands.UpdateMenuCategory;
using MapsterMapper;
using MediatR;

namespace Api.Endpoints.Menu.Categories;

internal sealed class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(Routes.Menu.CategoryById, async (int id, UpdateMenuCategoryDto dto, IMapper mapper, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = mapper.Map<UpdateMenuCategoryCommand>(dto) with { Id = id };
            await sender.Send(command, cancellationToken);
            return Results.NoContent();
        }).WithTags(Tags.Menu);
    }
} 