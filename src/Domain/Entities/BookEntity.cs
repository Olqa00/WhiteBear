namespace WhiteBear.Domain.Entities;

using WhiteBear.Domain.Exceptions;

public sealed class BookEntity
{
    //public List<AuthorEntity> Authors { get; private set; } = new();
    private List<AuthorEntity> authors = new();

    public IReadOnlyList<AuthorEntity> Authors => this.authors.AsReadOnly();
    public Isbn Isbn { get; private init; }
    public string Title { get; private set; } = string.Empty;
    public string Cover { get; private set; } = string.Empty;

    public BookEntity(Isbn isbn, string title, string cover) : this(isbn, title, cover, new List<AuthorEntity>())
    {
    }

    public BookEntity(Isbn isbn, string title, string cover, List<AuthorEntity> authors)
    {
        this.Isbn = isbn;
        this.SetTitle(title);
        this.SetCover(cover);
        foreach (var author in authors)
        {
            this.AddAuthor(author);
        }
    }

    public void AddAuthor(AuthorEntity author)
    {
        if (string.IsNullOrWhiteSpace(author.Name))
        {
            throw new EmptyAuthorNameException(nameof(author.Name));
        }
        this.authors.Add(author);
    }

    public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new EmptyTitleException(nameof(title));
        }

        this.Title = title;
    }
    public void SetCover(string cover)
    {
        if (string.IsNullOrWhiteSpace(cover))
        {
            throw new EmptyCoverException(nameof(cover));
        }

        this.Cover = cover;
    }
}
