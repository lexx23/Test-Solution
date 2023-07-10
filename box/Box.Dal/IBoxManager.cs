public interface IBoxManager
{
    Task<Box.Core.Box> AddAsync(Box.Core.Box box, CancellationToken token = default);
    Task DeleteAsync(string id, CancellationToken token = default);
}