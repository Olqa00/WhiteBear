using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using WhiteBear.Infrastructure.EntityFrameworkCore;
using WhiteBear.Infrastructure.EntityFrameworkCore;

namespace WhiteBear.Infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WhiteBear.Domain.Interfaces;
using WhiteBear.Infrastructure.EntityFrameworkCore.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = System.Reflection.Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        services.AddEntityFrameworkCore(configuration);

        services.AddScoped<IBookRepository, BookRepository>();

        return services;
    }
}
