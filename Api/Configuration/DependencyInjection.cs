namespace BaseApi.Configuration
{
    using Domain.Shared.Interfaces;
    using Domain.Users.Interfaces;
    using Domain.Users.Models;
    using Microsoft.Extensions.DependencyInjection;
    using Storage.Users;

    public static class DependencyInjection
    {
         public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IContext<User>, UserContext>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}