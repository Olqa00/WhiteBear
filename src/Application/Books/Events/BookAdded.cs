namespace WhiteBear.Application.Books.Events;

internal sealed record BookAdded : INotification
{
    public required Isbn Isbn { get; init; }
}
