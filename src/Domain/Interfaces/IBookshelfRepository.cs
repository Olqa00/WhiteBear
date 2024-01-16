namespace WhiteBear.Domain.Interfaces;

public interface IBookshelfRepository
{
    Task UpsertAsync(CancellationToken cancellationToken);
}
