using Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using static Test.Configuration.TestConfiguration;

namespace Test.Configuration;

public class TestApplication : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder
            .ConfigureServices(TestServices)
            .ConfigureServices(TestStorage);
        
        return base.CreateHost(builder);
    }
}