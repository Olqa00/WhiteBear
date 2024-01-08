namespace WhiteBear.Infrastructure.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;
using WhiteBear.Infrastructure.EFCore;

internal sealed class BookDbContextFactory : IDesignTimeDbContextFactory<BookContext>
{
    public BookContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BookContext>();
        optionsBuilder.UseSqlServer("Data Source=(local)\\SQLExpress;Initial Catalog=WhiteBear;Integrated Security=True;Encrypt=False;");
        optionsBuilder.ReplaceService<IMigrationsIdGenerator, FixedMigrationsIdGenerator>();

        return new BookContext(optionsBuilder.Options);
    }
}