namespace WhiteBear.Infrastructure.HealthChecks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WhiteBear.Infrastructure.EntityFrameworkCore;

internal static class DependencyInjection
{
    public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddDbContextCheck<BookContext>("database")
            .AddCheck<TestHealthCheck>("test");

        return services;
    }

    public static void MapHealthChecks(this IEndpointRouteBuilder app) 
    {
        app.MapHealthChecks("/health", new HealthCheckOptions()
        {
            ResponseWriter = HealthCheckSerializer.WriteResponse
        });

        app.MapHealthChecks("/health/test", new HealthCheckOptions
        {
            Predicate = healthCheck => healthCheck.Name == "test",
            ResponseWriter = HealthCheckSerializer.WriteResponse
        });
    }
}
