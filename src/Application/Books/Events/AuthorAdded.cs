namespace WhiteBear.Application.Books.Events;

internal sealed class AuthorAdded : INotification
{
    public required Guid Id { get; init; }
}

