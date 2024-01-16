namespace WhiteBear.Domain.Interfaces;

using WhiteBear.Domain.Entities;

public interface IAuthorRepository
{
    Task CreateAsync(AuthorEntity entity, CancellationToken cancellationToken = default);
}
