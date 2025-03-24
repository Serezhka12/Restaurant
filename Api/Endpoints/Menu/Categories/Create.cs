using Application.Menu.Commands.CreateMenuCategory;
using MapsterMapper;
using MediatR;
using Shared.Dtos.Menu;

namespace Api.Endpoints.Menu.Categories;

internal sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Menu.Categories, async (CreateMenuCategoryDto dto, IMapper mapper, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = mapper.Map<CreateMenuCategoryCommand>(dto);
            var id = await sender.Send(command, cancellationToken);
            return Results.Created($"/{Routes.Menu.Categories}/{id}", new { Id = id });
        }).WithTags(Tags.Menu);
    }
} 