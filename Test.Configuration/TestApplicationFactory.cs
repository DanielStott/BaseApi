using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Test.Configuration;

public class TestApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override IWebHostBuilder CreateWebHostBuilder()
    {
        return WebHost
            .CreateDefaultBuilder()
            .UseStartup<TStartup>();
    }
}