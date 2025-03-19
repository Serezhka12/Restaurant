namespace Infrastructure.Common;

public class RepositoryBase(AppDbContext context)
{
    protected async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}