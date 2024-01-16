namespace WhiteBear.Application.Bookshelf.Commands;

internal sealed record class UpdateBookshelf : IRequest
{
    public required Isbn Isbn { get; init; }
}
