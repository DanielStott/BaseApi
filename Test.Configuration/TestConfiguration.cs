using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Data.Users;
using MongoDB.Driver;

namespace Test.Configuration;

public static class TestConfiguration
{
    public static void TestServices(IServiceCollection services)
    {
        services.AddScoped<Seeder>();
    }

    public static void TestStorage(IServiceCollection services, string connectionString)
    {
        services.RemoveAll<DbContextOptions<UserContext>>();
        services.RemoveAll<IMongoDatabase>();

        services.AddDbContext<UserContext>(options =>
            options.UseInMemoryDatabase("TestDb"));
        services.AddSingleton(_ => new MongoDatabaseFactory(connectionString, "BaseApi").CreateDatabase());
    }
}