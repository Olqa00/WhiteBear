namespace WhiteBear.Application.Books.CommandHandlers;

using System.Reflection.Metadata;
using WhiteBear.Application.Books.Commands;
using WhiteBear.Application.Books.Events;
using WhiteBear.Application.Common.Extensions;
using WhiteBear.Domain.Entities;
using WhiteBear.Domain.Interfaces;

internal sealed class AddBookHandler : IRequestHandler<AddBook>
{
    private readonly ILogger<AddBookHandler> logger;
    private readonly IPublisher mediator;
    private readonly IBookRepository repository;

    public AddBookHandler(ILogger<AddBookHandler> logger, IPublisher mediator, IBookRepository repository)
        => (this.logger, this.mediator, this.repository) = (logger, mediator, repository);

    public async Task Handle(AddBook request, CancellationToken cancellationToken)
    {
        using var loggedScope = this.logger.BeginPropertyScope(
            ("RequestType", "Command")
            );

        this.logger.LogInformation("Try to upsert customer");

        var isbn = new Isbn(request.Isbn);
        var entity = new BookEntity(isbn, request.Title, request.Cover);

        foreach (var author in request.Authors)
        {
            var authorEntity = new AuthorEntity
            {
                Id = author.Id,
                Name = author.Name,
            };
            entity.AddAuthor(authorEntity);
        }

        this.logger.LogInformation("Creating '{Isbn}' Book", request.Isbn);
        await this.repository.CreateAsync(entity, cancellationToken);
        this.logger.LogInformation("Created '{Isbn}' Book ", request.Isbn);

        var @event = new BookAdded
        {
            Isbn = isbn,
        };

        await mediator.Publish(@event, cancellationToken);
        this.logger.LogInformation("Event '{Isbn}' published", request.Isbn);
    }
}
