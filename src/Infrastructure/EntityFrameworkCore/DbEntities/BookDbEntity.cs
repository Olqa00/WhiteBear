namespace WhiteBear.Infrastructure.EFCore.DbEntities;

internal class BookDbEntity
{
    public int RecordId { get; set; }
    public Guid BookId { get; set; }
    public string IsbnNumber { get; set; }
    public string Title { get; set; }
    public IEnumerable<AuthorDbEntity> Authors { get; set; }
}