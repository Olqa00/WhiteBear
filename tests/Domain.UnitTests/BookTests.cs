namespace WhiteBear.Domain.UnitTests;

using WhiteBear.Domain.Entities;
using WhiteBear.Domain.Exceptions;
using WhiteBear.Domain.Types;

[TestClass]
public class BookTests
{
    private const string EMPTY_STRING = "";
    private const string TITLE = "Struktury Danych w Języku C";
    private static readonly Isbn ISBN = new("8320419816");
    private const string COVER = "image.img";
    private const string NAME = "PAWEŁ NOWACKI";
    private const string SECONDAUTHORNAME = "KAROL PAWELSKI";

    [TestMethod]
    public void TestMethod1()
    {
        var sut = new BookEntity(ISBN, TITLE, COVER);

        Assert.AreEqual(sut.Isbn, ISBN);
        Assert.AreEqual(sut.Title, TITLE);
        Assert.AreEqual(sut.Cover, COVER);
    }

    [DataTestMethod, DataRow(""), DataRow("832041981"), DataRow("832041981Y"), DataRow("83204198160")]
    public void TestMethod2(string value)
    {
        var sut = () => new Isbn(value);
        sut.Should().Throw<IncorrectIsbnException>();
    }

    [TestMethod]
    public void TestMethod3()
    {
        var sut = () => new BookEntity(ISBN, EMPTY_STRING, COVER);

        sut.Should().Throw<EmptyTitleException>();
    }

    [TestMethod]
    public void TestMethod4()
    {
        var book = new BookEntity(ISBN, TITLE, COVER);

        var sut = () => book.SetTitle(EMPTY_STRING);

        sut.Should().Throw<EmptyTitleException>();
    }

    [TestMethod]
    public void Should_Throw_Empty_Cover_Exception_When_Trying_To_Set_Empty_Cover()
    {
        // Arrange
        var book = new BookEntity(ISBN, TITLE, COVER);

        // Act
        var sut = () => book.SetCover(EMPTY_STRING);

        // Assert
        sut.Should()
            .Throw<EmptyCoverException>()
            ;
    }

    [TestMethod]
    public void Can_Create_Book_With_Author_And_Other_Details()
    {
        var authors = new List<AuthorEntity>
        {
            new AuthorEntity
            {
                Id = Guid.NewGuid(),
                Name = NAME,
            }
        };

        var sut = new BookEntity(ISBN, TITLE, COVER, authors);

        Assert.AreEqual(sut.Isbn, ISBN);
        Assert.AreEqual(sut.Title, TITLE);
        Assert.AreEqual(sut.Cover, COVER);
    }

    [TestMethod]
    public void Should_Throw_Empty_Author_Exception_When_Add_Empty_Author_Name()
    {
        // Arrange
        var newAuthor = new AuthorEntity
        {
            Id = Guid.NewGuid(),
            Name = EMPTY_STRING,
        };

        var authors = new List<AuthorEntity>
        {
            new AuthorEntity
            {
                Id = Guid.NewGuid(),
                Name = NAME,
            }
        };

        var book = new BookEntity(ISBN, TITLE, COVER, authors);

        // Act
        var sut = () => book.AddAuthor(newAuthor);

        // Assert
        sut.Should()
            .Throw<EmptyAuthorNameException>()
            ;
    }

    [TestMethod]
    public void Should_Update_Author_Name()
    {
        // Arrange
        var newAuthor = new AuthorEntity
        {
            Id = Guid.NewGuid(),
            Name = SECONDAUTHORNAME,
        };

        var authors = new List<AuthorEntity>
        {
            new AuthorEntity
            {
                Id = Guid.NewGuid(),
                Name = NAME,
            }
        };

        var sut = new BookEntity(ISBN, TITLE, COVER, authors);

        // Act
        sut.AddAuthor(newAuthor);

        // Assert
        sut.Authors.Should()
            .HaveCount(2)
            ;
        sut.Authors.Should()
            .ContainEquivalentOf(newAuthor)
            ;
    }

}
