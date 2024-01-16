using WhiteBear.Infrastructure.EntityFrameworkCore.DbEntities;

namespace WhiteBear.Infrastructure.EntityFrameworkCore.Services;

using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhiteBear.Domain.Interfaces;

internal sealed class BookshelfRepository : IBookshelfRepository
{
    private readonly BookContext context;

    public BookshelfRepository(BookContext context)
    {
        this.context = context;
    }

    public async Task UpsertAsync(CancellationToken cancellationToken)
    {
        var bookshelf = await this.context.Bookshelfs.FirstOrDefaultAsync(cancellationToken);

        if (bookshelf is null)
        {
            bookshelf = new BookshelfDbEntity
            {
                BookQuantity = 0,
            };
            context.Bookshelfs.Add(bookshelf);
        }

        bookshelf.BookQuantity = bookshelf.BookQuantity + 1;

        await context.SaveChangesAsync(cancellationToken);
    }
}
