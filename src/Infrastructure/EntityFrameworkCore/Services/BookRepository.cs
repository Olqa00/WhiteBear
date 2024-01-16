namespace WhiteBear.Infrastructure.EntityFrameworkCore.Services;

using Microsoft.EntityFrameworkCore;
using System.Data;
using WhiteBear.Infrastructure.EntityFrameworkCore.DbEntities;
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
            Cover = entity.Cover,
        };
        context.Books.Add(bookDbEntity);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<BookEntity?> ReadAsync(Isbn isbn, CancellationToken cancellationToken = default)
    {
        var bookDbEntity = await this.context.Books
            .Include(book => book.Authors)
            .SingleOrDefaultAsync(book => book.IsbnNumber == isbn.Value, cancellationToken);

        if (bookDbEntity is null)
        {
            return default;
        }

        var bookEntity = new BookEntity(isbn, bookDbEntity.Title, bookDbEntity.Cover);

        return bookEntity;
    }

    public async Task UpdateAsync(BookEntity entity, CancellationToken cancellationToken = default)
    {
        var bookDbEntity = this.context.Books.SingleOrDefault(book => book.IsbnNumber == entity.Isbn.Value);

        if (bookDbEntity is null)
        {
            throw new RowNotInTableException(); //TODO Create Custom Exception
        }

        bookDbEntity.Title = entity.Title;

        await this.context.SaveChangesAsync(cancellationToken);

    }
}
