namespace WhiteBear.Application.Authors.Events;

internal sealed record AuthorAdded : INotification
{
    public required Guid Id { get; init; }
}

