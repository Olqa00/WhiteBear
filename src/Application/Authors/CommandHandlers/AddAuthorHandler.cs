using WhiteBear.Domain.Entities;
using WhiteBear.Domain.Interfaces;

namespace WhiteBear.Application.Authors.CommandHandlers;

using System.Threading;
using System.Threading.Tasks;
using WhiteBear.Application.Authors.Commands;
using WhiteBear.Application.Authors.Events;

internal sealed class AddAuthorHandler : IRequestHandler<AddAuthor>
{
    private readonly ILogger<AddAuthorHandler> logger;
    private readonly IPublisher mediator;
    private readonly IAuthorRepository repository;

    public AddAuthorHandler(ILogger<AddAuthorHandler> logger,IPublisher mediator, IAuthorRepository repository)
    {
        this.logger = logger;
        this.mediator = mediator;
        this.repository = repository;
    }

    public async Task Handle(AddAuthor request, CancellationToken cancellationToken)
    {
        var entity = new AuthorEntity
        {
            Id = request.Id,
            Name = request.Name,
        };

        this.logger.LogInformation("Adding author to repository");
        await this.repository.CreateAsync(entity, cancellationToken);
        this.logger.LogInformation("Added author to repository");

        var @event = new AuthorAdded
        {
            Id = request.Id,
        };

        await this.mediator.Publish(@event, cancellationToken);
    }
}
