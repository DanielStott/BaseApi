using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mongo2Go;
using static Test.Configuration.TestConfiguration;

namespace Test.Configuration;

public class TestApplication : WebApplicationFactory<Program>
{
    public HttpClient Client { get; private set; }
    public TestApplication() => Client = CreateClient();
    private MongoDbRunner MongoDbRunner { get; } = MongoDbRunner.Start();   


    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder
            .ConfigureServices(TestServices)
            .ConfigureServices(services => TestStorage(services, MongoDbRunner.ConnectionString));

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

    protected override void Dispose(bool disposing)
    {
        MongoDbRunner.Dispose();
        base.Dispose(disposing);
    }
}