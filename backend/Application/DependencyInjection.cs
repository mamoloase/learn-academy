
using Application.Behaviours;

using MediatR;
using FluentValidation;

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddMediatR(option =>
            option.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
