namespace WhiteBear.Infrastructure.EntityFrameworkCore;

using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WhiteBear.Domain.Interfaces;
using WhiteBear.Infrastructure.EntityFrameworkCore.Services;

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

        services.AddHostedService<DatabaseInitializer>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();

        return services;
    }
}