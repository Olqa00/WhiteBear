namespace WhiteBear.Infrastructure.Interfaces;

internal interface IBookshelfReadService
{
    Task<int> GetBookshelfCountAsync(CancellationToken cancellationToken = default);
}
