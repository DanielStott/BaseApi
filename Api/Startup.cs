using Lamar;

namespace Api
{
    using System;
    using Api.Configuration;
    using Api.Configuration.Middleware;
    using FluentValidation;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Storage.Users;

    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public virtual void ConfigureContainer(ServiceRegistry serviceRegistry)
        {
            serviceRegistry
                .AddControllers()
                .AddApplicationPart(AppDomain.CurrentDomain.Load("Api"));
            serviceRegistry.AddDependencyInjection();

            ConfigureStorage(serviceRegistry);

            var assembly = AppDomain.CurrentDomain.Load("Domain");
            serviceRegistry.AddMediatR(assembly);
            serviceRegistry.AddAutoMapper(typeof(Startup));
            serviceRegistry.AddMediatR(typeof(Startup));
            serviceRegistry.AddValidatorsFromAssembly(assembly);
            serviceRegistry.AddLogging(Configuration);
        }

        public virtual void ConfigureStorage(ServiceRegistry serviceRegistry)
        {
            serviceRegistry.AddDbContext<UserContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseExceptionHandler(ExceptionMiddleware.Handler);

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}