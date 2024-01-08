namespace WhiteBear.Application.UnitTests;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using WhiteBear.Application.CommandHandlers;
using WhiteBear.Application.Commands;
using WhiteBear.Domain.Entities;
using WhiteBear.Domain.Interfaces;

[TestClass]
public class UnitTest1
{
    private const string ISBN = "8320419816";
    private const string TITLE = "Struktury Danych w Języku C";

    private readonly ILogger<AddBookHandler> logger = new NullLogger<AddBookHandler>();

    [TestMethod]
    public async Task TestMethod1()
    {
        var command = new AddBook
        {
            Isbn = ISBN,
            Title = TITLE,
        };

        var sut = NSubstitute.Substitute.For<IBookRepository>();

        var handler = new AddBookHandler(this.logger, sut);

        await handler.Handle(command, CancellationToken.None);

        await sut
            .Received(1)
            .CreateAsync(Arg.Any<BookEntity>(), Arg.Any<CancellationToken>());
    }
}
