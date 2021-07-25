using Api.Configuration.Mediator;
using Microsoft.AspNetCore.Routing;

namespace Api.Configuration
{
    using Api.Configuration.Mediator;
    using Domain.Shared.Interfaces;
    using Domain.Users.Interfaces;
    using Domain.Users.Models;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Storage.Users;

    public static class DependencyInjection
    {
         public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.TryAddScoped<IContext<User>, UserContext>();
            services.TryAddScoped<IUserRepository, UserRepository>();
            services.TryAddScoped<IUserRepository, UserRepository>();
            services.TryAddScoped<LinkGenerator>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        }
    }
}