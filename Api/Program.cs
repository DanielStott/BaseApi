using Api.Configuration;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication
            .CreateBuilder(args);

        builder
            .ConfigureServices()
            .ConfigureHost()
            .ConfigureStorage();

        var app = builder.Build();

        app.ConfigureApplication();
        app.Run();
    }
}
