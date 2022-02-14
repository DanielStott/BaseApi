using Api;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Storage.Users;

namespace Test.Configuration;

public class TestStartup : Startup
{
    private readonly SqliteConnection _connection;

    public TestStartup(IConfiguration configuration)
        : base(configuration)
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();
    }

    public override void ConfigureServices(IServiceCollection serviceCollection)
    {
        ConfigureStorage(serviceCollection);
        ConfigureDependencies(serviceCollection);

        base.ConfigureServices(serviceCollection);
    }

    private void ConfigureDependencies(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<Seeder>();
    }

    public override void ConfigureStorage(IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddDbContext<UserContext>(options =>
            {
                options.UseSqlite(_connection);
            });
    }
}