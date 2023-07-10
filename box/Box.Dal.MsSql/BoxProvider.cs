using Microsoft.EntityFrameworkCore;

namespace Box.Dal.MsSql;

public class BoxProvider : IBoxProvider
{
    private readonly IDbContextFactory<BoxContext> _contextFactory;

    public BoxProvider(IDbContextFactory<BoxContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<Core.Box>> GetListAsync(CancellationToken token)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(token);
        var boxQuery = context.Boxes
            .OrderByDescending(x => x.Id)
            .AsNoTracking();
        var boxes = await boxQuery.ToArrayAsync(token);
        return boxes.Select(ConvertBox);
    }

    private static Core.Box ConvertBox(Entity.Box box)
    {
        return new Core.Box
        {
            Id = box.Id,
            Color = box.Color
        };
    }
}