using IdGen;
using Microsoft.EntityFrameworkCore;

namespace Box.Dal.MsSql;

public class BoxManager : IBoxManager
{
    private readonly IdGenerator _idGenerator;
    private readonly IDbContextFactory<BoxContext> _contextFactory;

    public BoxManager(IDbContextFactory<BoxContext> contextFactory, IdGenerator idGenerator)
    {
        _contextFactory = contextFactory;
        _idGenerator = idGenerator;
    }

    public async Task<Box.Core.Box> AddAsync(Core.Box box, CancellationToken token = default)
    {
        if (box == null)
        {
            throw new ArgumentException(nameof(box));
        }

        await using var context = _contextFactory.CreateDbContext();
        var boxDto = new Entity.Box
        {
            Id = _idGenerator.CreateId().ToString(),
            Color = box.Color
        };

        box.Id = boxDto.Id;
        context.Boxes.Add(boxDto);
        

        // Save changes
        await context.SaveChangesAsync(token);

        return box;
    }

    public async Task DeleteAsync(string id, CancellationToken token = default)
    {
        if (id == null)
        {
            return;
        }

        await using var context = await _contextFactory.CreateDbContextAsync(token);

        var queryTable = context.Boxes.EntityType.GetSchemaQualifiedTableName();
        var query = $"DELETE FROM {queryTable} WHERE [Id] = @p0";

        await context.Database.ExecuteSqlRawAsync(query, id);
    }
}