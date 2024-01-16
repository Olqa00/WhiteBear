using WhiteBear.Domain.Entities;

namespace WhiteBear.Domain.UnitTests;

[TestClass]
public class BookShelfTests
{
    private const int BOOK_INITIAL_QUANTITY = 3;

    [TestMethod]
    public void Should_Create_Bookshelf()
    {
        // Arrange

        // Act
        var sut = new BookshelfEntity(BOOK_INITIAL_QUANTITY);

        // Assert
        sut.BookQuantity
            .Should()
            .Be(BOOK_INITIAL_QUANTITY)
            ;
    }

    [TestMethod]
    public void Should_Increase_Book_Quantity_After_Add_Book()
    {
        // Arrange
        var sut = new BookshelfEntity(BOOK_INITIAL_QUANTITY);

        //Act
        sut.AddBook();

        //Assert
        sut.BookQuantity
            .Should()
            .Be(BOOK_INITIAL_QUANTITY + 1)
            ;
    }
}

