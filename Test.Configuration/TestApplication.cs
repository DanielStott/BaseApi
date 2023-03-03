using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static Test.Configuration.TestConfiguration;

namespace Test.Configuration;

internal class TestApplication : WebApplicationFactory<Program>
{
    public HttpClient Client { get; private set; }
    public LinkGenerator LinkGenerator { get; private set; }

    public TestApplication()
    {
        LinkGenerator = GetService<LinkGenerator>();
        Client = CreateClient();
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder
            .ConfigureServices(TestServices)
            .ConfigureServices(TestStorage);

        return base.CreateHost(builder);
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
}