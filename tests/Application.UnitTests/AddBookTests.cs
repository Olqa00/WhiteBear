namespace WhiteBear.Application.UnitTests;

using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute.ExceptionExtensions;
using Substitute = NSubstitute.Substitute;
using WhiteBear.Application.Books.Events;
using WhiteBear.Application.Bookshelf.CommandHandlers;
using WhiteBear.Application.Bookshelf.Commands;
using WhiteBear.Application.Books.CommandHandlers;
using WhiteBear.Application.Books.Commands;
using WhiteBear.Application.Bookshelf.EventHandlers;
using WhiteBear.Domain.Entities;
using WhiteBear.Domain.Interfaces;
using WhiteBear.Domain.Types;
using WhiteBear.Domain.Exceptions;
using NSubstitute;

[TestClass]
public class AddBookTests
{
    private const string ISBN = "8320419816";
    private const string TITLE = "Struktury Danych w Języku C";
    private const string COVER = "image.img";
    private const string NULL_STRING = "";
    private const int BOOK_INITIAL_QUANTITY = 3;
    private static readonly AuthorEntity AUTHOR = new()
    {
        Id = Guid.NewGuid(),
        Name = "Name Surname",
    };
    private static readonly List<AuthorEntity> AUTHORS = new()
    {
        AUTHOR,
    };

    private readonly ILogger<AddBookHandler> logger = new NullLogger<AddBookHandler>();

    [TestMethod]
    public async Task Should_Save_Book_in_Repository()
    {
        var command = new AddBook
        {
            Authors = AUTHORS,
            Cover = COVER,
            Isbn = ISBN,
            Title = TITLE,
        };

        var sut = Substitute.For<IBookRepository>();
        var mediator = Substitute.For<IMediator>();

        var handler = new AddBookHandler(this.logger, mediator, sut);

        await handler.Handle(command, CancellationToken.None);

        await sut
            .Received(1)
            .CreateAsync(Arg.Any<BookEntity>(), Arg.Any<CancellationToken>())
            ;
    }

    [TestMethod]
    public async Task Should_Compare_Created_Book_With_Actual_Book_in_Repository()
    {
        // Arrange
        var command = new AddBook
        {
            Authors = AUTHORS,
            Cover = COVER,
            Isbn = ISBN,
            Title = TITLE,
        };

        var sut = Substitute.For<IBookRepository>();
        var mediator = Substitute.For<IPublisher>();

        var handler = new AddBookHandler(this.logger, mediator, sut);

        await handler.Handle(command, CancellationToken.None);

        // Act
        await sut
            .Received(1)
            .CreateAsync(Arg.Is<BookEntity>(entity => string.Equals(command.Isbn, entity.Isbn.Value) && string.Equals(command.Title, entity.Title) && string.Equals(command.Cover, entity.Cover) && CompareLists(command.Authors, entity.Authors)), Arg.Any<CancellationToken>())
            ;
    }

    private bool CompareLists(List<AuthorEntity> left, IReadOnlyList<AuthorEntity> right)
    {
        if (left.Count() != right.Count())
        {
            return false;
        }

        for (var index = 0; index < left.Count(); index++)
        {
            if (left[index].Id != right[index].Id || string.Equals(left[index].Name, right[index].Name) is false)
            {
                return false;
            }
        }

        return true;
    }

    [TestMethod]
    public async Task Should_Publish_Notification_After_Creating_Book()
    {
        // Arrange
        var command = new AddBook
        {
            Authors = AUTHORS,
            Cover = COVER,
            Isbn = ISBN,
            Title = TITLE,
        };

        var repository = Substitute.For<IBookRepository>();
        var sut = Substitute.For<IPublisher>();
        var handler = new AddBookHandler(this.logger, sut, repository);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        await sut
            .Received()
            .Publish(Arg.Any<BookAdded>(), Arg.Any<CancellationToken>())
            ;
    }

    [TestMethod]
    public async Task Should_Handle_Book_Added_Notification()
    {
        // Arrange
        var mediator = Substitute.For<ISender>();
        var isbn = new Isbn(ISBN);

        var @event = new BookAdded
        {
            Isbn = isbn,
        };

        var logger = new NullLogger<BookAddedHandler>();

        // Act
        var sut = new BookAddedHandler(logger, mediator);
        await sut.Handle(@event, CancellationToken.None);

        // Assert
        await mediator
                .Received(1)
                .Send(Arg.Is<UpdateBookshelf>(command => command.Isbn == isbn), Arg.Any<CancellationToken>())
            ;
    }

    [TestMethod]
    public async Task UpdateBookshelf_Handler_Should_Call_Upsert_Bookshelf_Method()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();
        var isbn = new Isbn(ISBN);

        var command = new UpdateBookshelf
        {
            Isbn = isbn,
        };
        var sut = Substitute.For<IBookshelfRepository>();
        var handler = new UpdateBookshelfHandler(sut);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        await sut
                .Received(1)
                .UpsertAsync(Arg.Any<CancellationToken>())
            ;
    }

    [TestMethod]
    public async Task Should_Throw_Exception_When_Title_Null()
    {
        // Arrange
        var command = new AddBook
        {
            Authors = AUTHORS,
            Cover = COVER,
            Isbn = ISBN,
            Title = NULL_STRING,
        };

        var repository = Substitute.For<IBookRepository>();
        var sut = Substitute.For<IPublisher>();
        var handler = new AddBookHandler(this.logger, sut, repository);

        // Act
        //var action = await handler.Handle(command, CancellationToken.None);

        // Assert
        //sut.Should()
        //    .Received(1)
        //    .ReturnsForAnyArgs(Arg.Any<EmptyTitleException>())
        //    ;

        // Act & Assert
        await Assert.ThrowsExceptionAsync<EmptyTitleException>(() 
            => handler.Handle(command, CancellationToken.None))
            ;

    }

    //[DataTestMethod, DataRow(""), DataRow("832041981"), DataRow("832041981Y"), DataRow("83204198160")]
    //public async Task Should_Throw_Exception_When_ISBN_Valid(string value)
    //{
    //    // Arrange
    //    var command = new AddBook
    //    {
    //        Authors = AUTHORS,
    //        Cover = COVER,
    //        Isbn = value,
    //        Title = TITLE,
    //    };

    //    var repository = Substitute.For<IBookRepository>();
    //    var sut = Substitute.For<IPublisher>();
    //    var handler = new AddBookHandler(this.logger, sut, repository);

    //    // Act & Assert
    //    await Assert.ThrowsExceptionAsync<IncorrectIsbnException>(()
    //            => handler.Handle(command, CancellationToken.None))
    //        ;
    //}
}
