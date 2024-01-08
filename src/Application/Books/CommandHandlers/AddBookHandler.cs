namespace WhiteBear.Application.Books.CommandHandlers;

using WhiteBear.Application.Books.Commands;
using WhiteBear.Domain.Entities;
using WhiteBear.Domain.Interfaces;

internal sealed class AddBookHandler : IRequestHandler<AddBook>
{
    private readonly ILogger<AddBookHandler> logger;
    private readonly IBookRepository repository;

    public AddBookHandler(ILogger<AddBookHandler> logger, IBookRepository repository)
        => (this.logger, this.repository) = (logger, repository);

    public async Task Handle(AddBook request, CancellationToken cancellationToken)
    {
        var isbn = new Isbn(request.Isbn);
        var entity = new BookEntity(isbn, request.Title);

        this.logger.LogInformation("Creating Book");
        await this.repository.CreateAsync(entity, cancellationToken);
        this.logger.LogInformation("Created Book");
    }
}
