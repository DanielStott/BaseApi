namespace End2End
{
    using BaseApi;
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

        public override void ConfigureServices(IServiceCollection serviceCollection)
        {
            ConfigureDatabase(serviceCollection);

            base.ConfigureServices(serviceCollection);
        }

        private void ConfigureDatabase(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddDbContext<UserContext>(options =>
                {
                    options.UseSqlServer(_connection);
                });
        }
    }
}