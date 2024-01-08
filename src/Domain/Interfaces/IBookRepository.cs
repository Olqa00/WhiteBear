namespace WhiteBear.Domain.Interfaces;

using WhiteBear.Domain.Entities;

public interface IBookRepository
{
    Task CreateAsync(BookEntity entity, CancellationToken cancellationToken = default); // CRUD, Create, Read, Update, Delete
    Task<BookEntity?> ReadAsync(Isbn isbn, CancellationToken cancellationToken = default);
    Task UpdateAsync(BookEntity entity, CancellationToken cancellationToken = default);
}
