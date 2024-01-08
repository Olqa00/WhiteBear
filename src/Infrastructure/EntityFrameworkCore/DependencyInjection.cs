using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WhiteBear.Domain.Interfaces;
using WhiteBear.Infrastructure.EFCore;
using WhiteBear.Infrastructure.EntityFrameworkCore.Services;

namespace WhiteBear.Infrastructure.EntityFrameworkCore;

internal static class DependencyInjection
{
    public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<BookContext>(options =>
        {
            options.UseSqlServer(connectionString,
                _=> Assembly.GetExecutingAssembly());
        });

        services.AddScoped<IBookRepository, BookRepository>();

        return services;
    }
}