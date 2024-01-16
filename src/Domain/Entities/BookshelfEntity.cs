namespace WhiteBear.Domain.Entities;

public sealed class BookshelfEntity
{
    public int BookQuantity { get; private set; } = default;

    public BookshelfEntity(int bookQuantity)
    {
        this.BookQuantity = bookQuantity;
    }

    public void AddBook() => this.BookQuantity += 1;

}

