using Lamar;

namespace End2End
{
    using Api;
    using Data;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Storage.Users;

    public class TestStartup : Startup
    {
        private readonly SqliteConnection _connection;

        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
        }

        public override void ConfigureContainer(ServiceRegistry serviceCollection)
        {
            ConfigureStorage(serviceCollection);
            ConfigureDependencies(serviceCollection);

            base.ConfigureContainer(serviceCollection);
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
}