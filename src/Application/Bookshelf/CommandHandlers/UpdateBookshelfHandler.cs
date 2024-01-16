namespace WhiteBear.Application.Bookshelf.CommandHandlers;

using WhiteBear.Application.Bookshelf.Commands;
using WhiteBear.Domain.Interfaces;

internal class UpdateBookshelfHandler : IRequestHandler<UpdateBookshelf>
{
    private readonly IBookshelfRepository repository;

    public UpdateBookshelfHandler(IBookshelfRepository repository)
    {
        this.repository = repository;
    }

    public async Task Handle(UpdateBookshelf request, CancellationToken cancellationToken)
    {
        await this.repository.UpsertAsync(cancellationToken);
    }
}
