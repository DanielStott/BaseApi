using Api.Configuration.Middleware;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Storage.Users;

namespace Api.Configuration;

public static class Configuration
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder webApp)
    {
        var services = webApp.Services;
        var configuration = webApp.Configuration;

        services
            .AddControllers()
            .AddApplicationPart(AppDomain.CurrentDomain.Load("Api"));
        services.AddDependencyInjection();

        var assembly = AppDomain.CurrentDomain.Load("Domain");
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        services.AddAutoMapper(typeof(Program));
        services.AddValidatorsFromAssembly(assembly);
        services.AddLogging(configuration);
        services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "BaseApi", Version = "v1" }));

        return webApp;
    }

    public static WebApplicationBuilder ConfigureStorage(this WebApplicationBuilder webApp)
    {
        webApp.Services.AddDbContext<UserContext>(options =>
            options.UseSqlServer(webApp.Configuration.GetConnectionString("Default")));

        return webApp;
    }

    public static WebApplicationBuilder ConfigureHost(this WebApplicationBuilder webApp)
    {
        webApp.Host.UseSerilog();

        return webApp;
    }

    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        var env = app.Environment;

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "BaseApi"); });
        }

        app.Map("/api", api =>
        {
            api.UseRouting();
            api.UseExceptionHandler(ExceptionMiddleware.Handler);
            api.UseAuthentication();
            api.UseAuthorization();
            api.UseHttpsRedirection();
            api.UseEndpoints(endpoints => endpoints.MapControllers());
        });

        return app;
    }
}