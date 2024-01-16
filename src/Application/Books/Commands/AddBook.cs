namespace WhiteBear.Application.Books.Commands;

using WhiteBear.Domain.Entities;

public sealed record class AddBook : IRequest
{
    [JsonPropertyName("isbn")] public required string Isbn { get; init; }
    [JsonPropertyName("title")] public required string Title { get; init; }
    [JsonPropertyName("cover")] public required string Cover { get; init; }
    [JsonPropertyName("authors")] public required List<AuthorEntity> Authors { get; init; }
}
