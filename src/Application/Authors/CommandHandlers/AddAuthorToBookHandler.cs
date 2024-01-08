using WhiteBear.Domain.Entities;

namespace WhiteBear.Application.Authors.CommandHandlers;

using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using WhiteBear.Application.Authors.Commands;
using WhiteBear.Domain.Interfaces;

internal sealed class AddAuthorToBookHandler : IRequestHandler<AddAuthorToBook>
{
    private readonly ILogger<AddAuthorToBookHandler> logger;
    private readonly IBookRepository repository;

    public AddAuthorToBookHandler(ILogger<AddAuthorToBookHandler> logger, IBookRepository repository)
        => (this.logger, this.repository) = (logger, repository);

    public async Task Handle(AddAuthorToBook request, CancellationToken cancellationToken)
    {
        var isbn = new Isbn(request.Isbn);

        var book = await this.repository.ReadAsync(isbn, cancellationToken);

        if (book is null)
        {

        }

        var authorEntity = new AuthorEntity
        {
            Id = request.AuthorId,
            Name = request.Name,
        };

        book.AddAuthor(authorEntity);

        await this.repository.UpdateAsync(book, cancellationToken);
    }
}
