namespace WhiteBear.Domain.Entities;

public sealed record class AuthorEntity
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}
