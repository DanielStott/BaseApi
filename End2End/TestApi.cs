using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace End2End
{
    using System.Net.Http;
    using NUnit.Framework;

    public class TestApi
    {
        public static TestApplicationFactory<TestStartup> TestApplicationFactory { get; private set; }
        public static HttpClient Client { get; private set; }

        [OneTimeSetUp]
        public void Setup()
        {
            TestApplicationFactory = new TestApplicationFactory<TestStartup>();
            Client = TestApplicationFactory.CreateClient();
        }

        public static async Task<(T, HttpStatusCode)> Get<T>(string url)
        {
            var response = await Client.GetAsync(url);
            return (await response.Content.ReadFromJsonAsync<T>(), response.StatusCode);
        }
    }
}