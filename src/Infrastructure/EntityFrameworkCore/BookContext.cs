namespace WhiteBear.Infrastructure.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using WhiteBear.Infrastructure.EntityFrameworkCore.DbEntities;

internal class BookContext(DbContextOptions<BookContext> options) : DbContext(options)
{
    public DbSet<BookDbEntity> Books { get; set; }
    public DbSet<AuthorDbEntity> Authors { get; set; }
    public DbSet<BookshelfDbEntity> Bookshelfs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookDbEntity>()
            .HasKey(book => book.RecordId);

        modelBuilder.Entity<AuthorDbEntity>()
            .HasKey(author => author.RecordId);

        modelBuilder.Entity<BookshelfDbEntity>()
            .HasKey(book => book.RecordId);

        modelBuilder.Entity<BookDbEntity>()
            .HasMany(e => e.Authors)
            .WithMany(e => e.Books)
            .UsingEntity("BookAuthors");
    }
}