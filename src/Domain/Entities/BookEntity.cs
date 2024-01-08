namespace WhiteBear.Domain.Entities;

using WhiteBear.Domain.Exceptions;

public sealed class BookEntity
{
    public List<AuthorEntity> Authors { get; private set; } = new();
    //private List<Author> authors = new();

    //public IReadOnlyList<Author> Authors => this.authors.AsReadOnly();
    public Isbn Isbn { get; private init; }
    public string Title { get; private set; } = string.Empty;

    public BookEntity(Isbn isbn, string title)
    {
        this.Isbn = isbn;
        this.SetTitle(title);
    }

    public void AddAuthor(AuthorEntity author)
    {
        this.Authors.Add(author);
    }

    public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new EmptyTitleException(nameof(title));
        }

        this.Title = title;
    }
}
