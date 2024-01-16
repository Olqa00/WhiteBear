using System.Runtime.InteropServices.JavaScript;
using WhiteBear.Application.Bookshelf.Queries;
using WhiteBear.Domain.Entities;

namespace WhiteBear.Infrastructure.UnitTests;

using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using WhiteBear.Infrastructure.Interfaces;
using WhiteBear.Infrastructure.QueryHandlers;

[TestClass]
public sealed class BookServiceReadServiceUnitTests
{
    private readonly ILogger<GetBooksFromBookshelfHandler> logger = new NullLogger<GetBooksFromBookshelfHandler>();
    private readonly static CancellationToken cancellationToken= CancellationToken.None;

    [TestMethod]
    public async Task Should_Return_Books()
    {
        // Arrange
        var readService = Substitute.For<IBookshelfReadService>();
        var handler = new GetBooksFromBookshelfHandler(logger, readService);
        
        var query = new GetBooksOnBookshelf
        {
        };

        readService.GetBookshelfCountAsync(default).ReturnsForAnyArgs(_ => 2);
        
        // Act
        var sut = await handler.Handle(query, cancellationToken);

        // Assert
        sut.Count.Should()
            .Be(2)
            ;
    }

    [TestMethod]
    public async Task Should_Return_Two()
    {
        // Arrange


        // Act


        // Assert


    }
}

