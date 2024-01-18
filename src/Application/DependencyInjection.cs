namespace WhiteBear.Application;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WhiteBear.Application.Common.Behaviors;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = System.Reflection.Assembly.GetExecutingAssembly();
        
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        return services;
    }
}
