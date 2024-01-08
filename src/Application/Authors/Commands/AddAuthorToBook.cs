namespace WhiteBear.Application.Authors.Commands;

public sealed record class AddAuthorToBook : IRequest
{
    public required string Isbn { get; init; }
    public required Guid AuthorId { get; init; }
    public required string Name { get; init; }
}
