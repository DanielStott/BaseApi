using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Database
{
    using System;
    using System.Linq;
    using System.Reflection;
    using DbUp;

    internal static class Program
    {

        private static int Main(string[] args)
        {
            // var t = Directory.GetParent(Directory.GetCurrentDirectory());
            var basePath = AppDomain.CurrentDomain.Load("Api");
            var config = new ConfigurationBuilder()
                .SetBasePath(basePath.Location)
                .AddJsonFile($"appsettings.json")
                .Build();
            var connectionString = config.GetConnectionString("Default");
                // ?? "Data Source=localhost,1433;Initial Catalog=master;User Id=sa;Password=testPW12345678!;";

            Console.WriteLine(connectionString);
            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();

                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;
        }
    }
}