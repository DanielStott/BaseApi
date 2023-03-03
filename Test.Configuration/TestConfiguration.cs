using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Storage.Users;

namespace Test.Configuration;

public static class TestConfiguration
{
    public static void TestServices(IServiceCollection services)
    {
        services.AddScoped<Seeder>();
    }

    public static void TestStorage(IServiceCollection services)
    {
        services.RemoveAll<DbContextOptions<UserContext>>();

        services.AddDbContext<UserContext>(options =>
            options.UseInMemoryDatabase("TestDb"));
    }
}