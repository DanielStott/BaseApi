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

namespace Api;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public virtual void ConfigureServices(IServiceCollection services)
    {
        services
            .AddControllers()
            .AddApplicationPart(AppDomain.CurrentDomain.Load("Api"));
        services.AddDependencyInjection();

        ConfigureStorage(services);

        var assembly = AppDomain.CurrentDomain.Load("Domain");
        services.AddMediatR(assembly);
        services.AddAutoMapper(typeof(Startup));
        services.AddValidatorsFromAssembly(assembly);
        services.AddLogging(Configuration);
    }

    public virtual void ConfigureStorage(IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<UserContext>(options =>
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