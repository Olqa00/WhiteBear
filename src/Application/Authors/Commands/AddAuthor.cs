namespace WhiteBear.Application.Authors.Commands;

public sealed record class AddAuthor : IRequest
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}