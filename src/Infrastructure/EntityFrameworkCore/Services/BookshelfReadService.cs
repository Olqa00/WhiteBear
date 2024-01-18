namespace WhiteBear.Infrastructure.EntityFrameworkCore.Services;

using WhiteBear.Infrastructure.Interfaces;

internal sealed class BookshelfReadService : IBookshelfReadService
{
    public Task<int> GetBookshelfCountAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

