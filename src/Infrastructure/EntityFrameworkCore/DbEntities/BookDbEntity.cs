namespace WhiteBear.Infrastructure.EntityFrameworkCore.DbEntities;

internal class BookDbEntity
{
    public int RecordId { get; set; } = default;
    public Guid BookId { get; set; } = Guid.Empty;
    public string IsbnNumber { get; set; } = "0000000000";
    public string Title { get; set; } = string.Empty;
    public string Cover { get; set; } = string.Empty;
    public IEnumerable<AuthorDbEntity> Authors { get; set; } = new List<AuthorDbEntity>()!;
}
