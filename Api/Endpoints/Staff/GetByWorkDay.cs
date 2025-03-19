using Application.Staff.Queries.GetEmployeesByWorkDay;
using Domain.Entities.Staff;
using MediatR;

namespace Api.Endpoints.Staff;

internal sealed class GetByWorkDay : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Staff.ByDay, async (string day, ISender sender, CancellationToken cancellationToken) =>
        {
            if (Enum.TryParse<DayOfWeek>(day, true, out var dayEnum))
            {
                var staff = await sender.Send(new GetEmployeesByWorkDayQuery(dayEnum), cancellationToken);
                return Results.Ok(staff);
            }

            return Results.BadRequest($"Невірний день: {day}. Допустимі значення: {string.Join(", ", Enum.GetNames<DayOfWeek>())}");
        }).WithTags(Tags.Staff);
    }
}