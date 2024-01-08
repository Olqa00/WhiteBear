namespace WhiteBear.Application.Books.Commands;

public sealed record class AddBook : IRequest
{
    [JsonPropertyName("isbn")] public required string Isbn { get; init; }
    [JsonPropertyName("title")] public required string Title { get; init; }
}
