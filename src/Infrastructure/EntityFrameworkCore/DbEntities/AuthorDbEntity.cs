namespace WhiteBear.Infrastructure.EntityFrameworkCore.DbEntities;

internal class AuthorDbEntity
{
    public int RecordId { get; set; }
    public Guid AuthorId { get; set; }
    public string Name { get; set; }
    public IEnumerable<BookDbEntity> Books { get; init; }
}
