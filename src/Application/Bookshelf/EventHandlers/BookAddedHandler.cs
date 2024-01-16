using WhiteBear.Application.Books.Events;
using WhiteBear.Application.Bookshelf.Commands;

namespace WhiteBear.Application.Bookshelf.EventHandlers;

internal class BookAddedHandler : INotificationHandler<BookAdded>
{
    private readonly ILogger<BookAddedHandler> logger;
    private readonly ISender mediator;

    public BookAddedHandler(ILogger<BookAddedHandler> logger, ISender mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    public async Task Handle(BookAdded notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Publish update shelf event: {notification}", notification);

        var command = new UpdateBookshelf
        {
            Isbn=notification.Isbn,
        };

        await mediator
            .Send(command, cancellationToken);
    }
}

