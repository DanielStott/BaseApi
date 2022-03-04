using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
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
        services.RemoveAll(typeof(DbContextOptions<UserContext>));
        
        services.AddDbContext<UserContext>(options =>
            options.UseInMemoryDatabase("TestDb", new InMemoryDatabaseRoot())); 
    }
}