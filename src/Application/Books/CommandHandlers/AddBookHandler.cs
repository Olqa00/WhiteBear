namespace WhiteBear.Application.Books.CommandHandlers;

using FluentValidation.Results;
using WhiteBear.Application.Books.Commands;
using WhiteBear.Application.Books.Events;
using WhiteBear.Application.Validators;
using WhiteBear.Domain.Entities;
using WhiteBear.Domain.Exceptions;
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
        var isbn = new Isbn(request.Isbn);
        var entity = new BookEntity(isbn, request.Title, request.Cover);

        AddBookValidator bookvalidator = new AddBookValidator();
        ValidationResult bookResults = bookvalidator.Validate(entity);

        if (!bookResults.IsValid)
        {
            throw new EmptyTitleException(nameof(entity.Title));
        }

        foreach (var author in request.Authors)
        {
            var authorEntity = new AuthorEntity
            {
                Id = author.Id,
                Name = author.Name,
            };
            entity.AddAuthor(authorEntity);
        }

        this.logger.LogInformation("Creating Book");

        await this.repository.CreateAsync(entity, cancellationToken);
        this.logger.LogInformation("Created Book");

        var @event = new BookAdded
        {
            Isbn = isbn,
        };

        await mediator.Publish(@event, cancellationToken);
    }
}
