public interface IBoxProvider
{
    Task<IEnumerable<Box.Core.Box>> GetListAsync(CancellationToken token = default);
}