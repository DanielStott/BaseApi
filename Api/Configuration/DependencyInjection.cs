using Api.Configuration.Mediator;
using Domain.Shared.Interfaces;
using Domain.Users.Interfaces;
using Domain.Users.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Storage.Users;

namespace Api.Configuration;

public static class DependencyInjection
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        services.TryAddScoped<IContext<User>, UserContext>();
        services.TryAddScoped<IUserRepository, UserRepository>();
        services.TryAddScoped<LinkGenerator>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
    }
}