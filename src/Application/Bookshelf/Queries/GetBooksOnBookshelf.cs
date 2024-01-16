namespace WhiteBear.Application.Bookshelf.Queries;

using WhiteBear.Application.Bookshelf.ViewModels;

public sealed record class GetBooksOnBookshelf : IRequest<Books>
{
}
