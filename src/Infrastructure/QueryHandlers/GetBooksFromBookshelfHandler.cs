using WhiteBear.Infrastructure.Interfaces;

namespace WhiteBear.Infrastructure.QueryHandlers;

using WhiteBear.Application.Bookshelf.ViewModels;
using WhiteBear.Application.Bookshelf.Queries;
using System.Threading.Tasks;
using System.Threading;

internal sealed class GetBooksFromBookshelfHandler : IRequestHandler<GetBooksOnBookshelf, Books>
{
    private readonly ILogger<GetBooksFromBookshelfHandler> logger;
    private readonly IBookshelfReadService readService;

    public GetBooksFromBookshelfHandler(ILogger<GetBooksFromBookshelfHandler> logger, IBookshelfReadService readService)
    {
        this.logger = logger;
        this.readService = readService;
    }
    public async Task<Books> Handle(GetBooksOnBookshelf request, CancellationToken cancellationToken)
    {
        var count = await this.readService.GetBookshelfCountAsync(cancellationToken);
        
        var books = new Books
        {
            Count = count,
        };

        return books;
    }
}
