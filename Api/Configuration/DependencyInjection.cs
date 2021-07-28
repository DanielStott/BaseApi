namespace Api.Configuration
{
    using Api.Configuration.Mediator;
    using Lamar;
    using MediatR;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this ServiceRegistry services)
        {
            services.Scan(s =>
            {
                s.TheCallingAssembly();
                s.Assembly("Domain");
                s.Assembly("Storage");
                s.WithDefaultConventions();
            });

            // TODO: Worth checking lamar documentation in more detail to see if the following can be implemented easier
            services.TryAddScoped<LinkGenerator>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        }
    }
}