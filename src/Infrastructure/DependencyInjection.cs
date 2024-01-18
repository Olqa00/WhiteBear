namespace WhiteBear.Infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WhiteBear.Domain.Interfaces;
using WhiteBear.Infrastructure.EntityFrameworkCore;
using WhiteBear.Infrastructure.EntityFrameworkCore.Services;
using WhiteBear.Infrastructure.HealthChecks;
using WhiteBear.Infrastructure.Interfaces;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = System.Reflection.Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        services.AddEntityFrameworkCore(configuration);
        services.AddHealthChecks(configuration);

        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookshelfRepository, BookshelfRepository>();
        services.AddScoped<IBookshelfReadService, BookshelfReadService>();

        return services;
    }
}
