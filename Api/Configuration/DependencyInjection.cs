namespace BaseApi.Configuration
{
    using BaseApi.Configuration.Mediator;
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
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }
    }
}