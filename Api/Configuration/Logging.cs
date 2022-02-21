using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Api.Configuration;

public static class Logging
{
    public static void AddLogging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging();
        Log.Logger = new LoggerConfiguration()
            .ReadFrom
            .Configuration(configuration)
            .CreateLogger();
        services.AddSingleton(Log.Logger);
    }
}