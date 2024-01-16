namespace WhiteBear.Infrastructure.UnitTests;

using Microsoft.EntityFrameworkCore;
using WhiteBear.Infrastructure.EntityFrameworkCore;
using WhiteBear.Infrastructure.EntityFrameworkCore.Services;

[TestClass]
public sealed class BookshelfRepositoryTests
{
    private readonly static CancellationToken cancellationToken = CancellationToken.None;

    [TestMethod]
    public async Task Should_Add_First_Book_To_Bookshelf()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<BookContext>().UseInMemoryDatabase(databaseName: "database_name").Options;

        await using var sut = new BookContext(options);
        var repository = new BookshelfRepository(sut);

        // Act
        await repository.UpsertAsync(cancellationToken);

        // Assert
        sut.Bookshelfs
            .Should()
            .HaveCount(1)
            ;

        await sut.Bookshelfs
            .SingleAsync(cancellationToken)
            ;
    }

    [TestMethod]
    public async Task After_Added_To_Books_Should_Return_Two()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<BookContext>().UseInMemoryDatabase(databaseName: "database_name2").Options;

        await using var sut = new BookContext(options);
        var repository = new BookshelfRepository(sut);

        // Act
        await repository.UpsertAsync(cancellationToken);
        await repository.UpsertAsync(cancellationToken);

        // Assert
        sut.Bookshelfs
            .Should()
            .Contain(entity => entity.BookQuantity == 2)
            ;

    }
}
