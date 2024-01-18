namespace WhiteBear.Infrastructure.HealthChecks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WhiteBear.Infrastructure.EntityFrameworkCore;

internal static class DependencyInjection
{
    public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddDbContextCheck<BookContext>()
            .AddCheck<TestHealthCheck>("test");

        return services;
    }
}
