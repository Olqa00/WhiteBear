using WhiteBear.Infrastructure.EFCore;
using WhiteBear.Infrastructure.EFCore.DbEntities;

namespace WhiteBear.Infrastructure.EntityFrameworkCore.Services;

using WhiteBear.Domain.Entities;
using WhiteBear.Domain.Interfaces;
using WhiteBear.Domain.Types;

internal sealed class BookRepository : IBookRepository
{
    private readonly BookContext context;

    public BookRepository(BookContext context)
    {
        this.context = context;
    }

    public async Task CreateAsync(BookEntity entity, CancellationToken cancellationToken = default)
    {
        var bookDbEntity = new BookDbEntity
        {
            IsbnNumber = entity.Isbn.Value,
            Title = entity.Title,
        };

        context.Books.Add(bookDbEntity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public Task<BookEntity?> ReadAsync(Isbn isbn, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(BookEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
