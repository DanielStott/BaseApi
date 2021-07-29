namespace End2End
{
    using Api;
    using Data;
    using Lamar;
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

        public override void ConfigureContainer(ServiceRegistry serviceRegistry)
        {
            ConfigureStorage(serviceRegistry);
            ConfigureDependencies(serviceRegistry);

            base.ConfigureContainer(serviceRegistry);
        }

        public override void ConfigureStorage(ServiceRegistry serviceRegistry)
        {
            serviceRegistry
                .AddDbContext<UserContext>(options =>
                {
                    options.UseSqlite(_connection);
                });
        }

        private void ConfigureDependencies(ServiceRegistry serviceRegistry)
        {
            serviceRegistry.AddSingleton<Seeder>();
        }
    }
}