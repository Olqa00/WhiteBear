namespace WhiteBear.Infrastructure.EntityFrameworkCore.Services;

using System.Threading;
using System.Threading.Tasks;
using WhiteBear.Domain.Entities;
using WhiteBear.Domain.Interfaces;

internal sealed class AuthorRepository : IAuthorRepository
{
    public Task CreateAsync(AuthorEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

