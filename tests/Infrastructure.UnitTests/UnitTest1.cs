namespace WhiteBear.Infrastructure.UnitTests;

using WhiteBear.Application.Authors.Commands;
using WhiteBear.Domain.Entities;
using WhiteBear.Domain.Types;
using Microsoft.EntityFrameworkCore;
using WhiteBear.Infrastructure.EntityFrameworkCore;
using WhiteBear.Infrastructure.EntityFrameworkCore.Services;

[TestClass]
public class UnitTest1
{
    private static readonly Isbn ISBN = new("8320419816");
    private const string TITLE = "Struktury Danych w Jêzyku C";
    private const string UPDATED_TITLE = "Czysty Kod";
    private const string COVER = "image.img";

    [TestMethod]
    public async Task TestMethod1()
    {
        var options = new DbContextOptionsBuilder<BookContext>().UseInMemoryDatabase(databaseName: "database_name").Options;

        await using var sut = new BookContext(options);
        var repository = new BookRepository(sut);
        var entity = new BookEntity(ISBN, TITLE, COVER);
        await repository.CreateAsync(entity);

        sut.Books.Should()
            .HaveCount(1)
            ;

        var book = await sut.Books
            .Include(book => book.Authors)
            .SingleAsync()
            ;

        book.Title.Should()
            .Be(TITLE)
            ;

        book.IsbnNumber.Should()
            .Be(ISBN.Value)
            ;

        book.Authors.Should()
            .BeEmpty()
            ;
    }

    [TestMethod]
    public async Task Should_Update_Book_In_Database()
    {
        var options = new DbContextOptionsBuilder<BookContext>().UseInMemoryDatabase(databaseName: "database_name2").Options;

        await using var sut = new BookContext(options);
        var repository = new BookRepository(sut);
        var entity = new BookEntity(ISBN, TITLE, COVER);
        await repository.CreateAsync(entity);

        entity.SetTitle(UPDATED_TITLE);

        await repository.UpdateAsync(entity);

        var updatedBook = sut.Books.Single();

        updatedBook.Title.Should()
            .Be(UPDATED_TITLE)
            ;
    }

    [TestMethod]
    public async Task Should_Return_Book_By_Isbn()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var options = new DbContextOptionsBuilder<BookContext>().UseInMemoryDatabase(databaseName: "database_name3").Options;

        await using var sut = new BookContext(options);
        var repository = new BookRepository(sut);
        var entity = new BookEntity(ISBN, TITLE, COVER);
        await repository.CreateAsync(entity, cancellationToken);

        // Act
        var createdBook = await repository.ReadAsync(ISBN, cancellationToken);

        // Assert
        createdBook.Should()
            .NotBeNull()
            ;

        createdBook!.Title.Should()
            .NotBeNullOrWhiteSpace()
            .And
            .Be(TITLE)
            ;

        createdBook!.Cover.Should()
            .NotBeNullOrWhiteSpace()
            .And
            .Be(COVER)
            ;

    }

    //[TestMethod]
    //public async Task Should_Return_Null_When_Book_Do_Not_Exists()
    //{
    //    // Arrange
    //    var cancellationToken = CancellationToken.None;
    //    var options = new DbContextOptionsBuilder<BookContext>().UseInMemoryDatabase(databaseName: "database_name4").Options;
    //    await using var sut = new BookContext(options);
    //    var repository = new BookRepository(sut);

    //    // Act
    //    var book = await repository.ReadAsync(new("1234567890"), cancellationToken);

    //    // Assert
    //    book.Should()
    //        .BeNull()
    //        ;
    //}
}