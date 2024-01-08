namespace WhiteBear.Domain.UnitTests;

using WhiteBear.Domain.Entities;
using WhiteBear.Domain.Exceptions;
using WhiteBear.Domain.Types;

[TestClass]
public class UnitTest1
{
    private const string EMPTY_TITLE = "";
    private const string TITLE = "Struktury Danych w Języku C";
    private static readonly Isbn ISBN = new("8320419816");

    [TestMethod]
    public void TestMethod1()
    {
        var sut = new BookEntity(ISBN, TITLE);

        Assert.AreEqual(sut.Isbn, ISBN);
        Assert.AreEqual(sut.Title, TITLE);
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
        var sut = () => new BookEntity(ISBN, EMPTY_TITLE);

        sut.Should().Throw<EmptyTitleException>();
    }

    [TestMethod]
    public void TestMethod4()
    {
        var book = new BookEntity(ISBN, TITLE);

        var sut = () => book.SetTitle(EMPTY_TITLE);

        sut.Should().Throw<EmptyTitleException>();
    }
}
