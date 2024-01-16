using Microsoft.Extensions.Logging.Abstractions;

namespace WhiteBear.Application.UnitTests;

using Microsoft.Extensions.Logging;
using WhiteBear.Application.Authors.CommandHandlers;
using WhiteBear.Application.Authors.Commands;
using WhiteBear.Application.Authors.Events;
using WhiteBear.Domain.Entities;
using WhiteBear.Domain.Interfaces;

[TestClass]
public class AddAuthorUnitTests
{
    private const string AUTHOR_NAME = "Jan Kowalski";
    private readonly static CancellationToken cancellationToken = CancellationToken.None;
    private readonly ILogger<AddAuthorHandler> logger = new NullLogger<AddAuthorHandler>();

    [TestMethod]
    public async Task Should_Publish_Notification_After_Creating_Author()
    {
        // Arrange
        var command = new AddAuthor
        {
            Id = Guid.NewGuid(),
            Name = AUTHOR_NAME,
        };

        var repository = Substitute.For<IAuthorRepository>();
        var sut = Substitute.For<IPublisher>();

        var handler = new AddAuthorHandler(this.logger, sut, repository);

        // Act
        await handler.Handle(command, cancellationToken);

        // Assert
        await sut
                .Received()
                .Publish(Arg.Any<AuthorAdded>(), Arg.Any<CancellationToken>())
            ;
    }

    [TestMethod]
    public async Task Should_Add_To_Repository()
    {
        // Arrange
        var command = new AddAuthor
        {
            Id = new Guid(),
            Name = AUTHOR_NAME,
        };

        var sut = Substitute.For<IAuthorRepository>();
        var mediator = Substitute.For<IPublisher>();

        var handler = new AddAuthorHandler(this.logger, mediator, sut);

        // Act
        await handler.Handle(command, cancellationToken);

        // Assert
        await sut
                .Received(1)
                .CreateAsync(Arg.Any<AuthorEntity>(), Arg.Any<CancellationToken>())
            ;
    }

    [TestMethod]
    public async Task Should_Compared_Created_Author_With_Actual_Author_In_Repository()
    {
        // Arrange
        var command = new AddAuthor
        {
            Id = new Guid(),
            Name = AUTHOR_NAME,
        };

        var sut = Substitute.For<IAuthorRepository>();
        var mediator = Substitute.For<IPublisher>();

        var handler = new AddAuthorHandler(this.logger, mediator, sut);
        await handler.Handle(command, cancellationToken);

        // Act
        await sut
            .Received(1)
            .CreateAsync(Arg.Is<AuthorEntity>(entity => string.Equals(command.Id, entity.Id) && string.Equals(command.Name, entity.Name)), cancellationToken)
            ;
    }

    [TestMethod]
    public async Task Should_Add_Messages_To_Log()
    {
        // Arrange
        var command = new AddAuthor
        {
            Id = new Guid(),
            Name = AUTHOR_NAME,
        };

        var repository = Substitute.For<IAuthorRepository>();
        var mediator = Substitute.For<IPublisher>();
        var sut = Substitute.For<ILogger<AddAuthorHandler>>();

        var handler = new AddAuthorHandler(sut, mediator, repository);

        // Act
        await handler.Handle(command, cancellationToken);

        // Assert
        sut
            .Received(1)
            .LogInformation("Adding author to repository")
            ;

        sut
            .Received(1)
            .LogInformation("Added author to repository")
            ;
    }
}

