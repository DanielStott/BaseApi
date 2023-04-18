using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static Test.Configuration.TestConfiguration;

namespace Test.Configuration;

public class TestApplication : WebApplicationFactory<Program>
{
    public HttpClient Client { get; private set; }
    private SqliteConnection _connection;

    public TestApplication() => Client = CreateClient();

    protected override IHost CreateHost(IHostBuilder builder)
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        builder
            .ConfigureServices(services => TestStorage(services, _connection.ConnectionString))
            .ConfigureServices(TestServices);

        return base.CreateHost(builder);
    }

    public HttpClient GetClient()
    {
        return CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost"),
        });
    }

    public T GetService<T>()
    {
        var scope = Services
            .CreateScope();

        return scope
            .ServiceProvider
            .GetService<T>();
    }

    public async Task SeedTestData()
        => await GetService<Seeder>().Seed();

    public override ValueTask DisposeAsync()
    {
        _connection.Dispose();
        return base.DisposeAsync();
    }
}