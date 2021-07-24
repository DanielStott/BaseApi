namespace Domain.Shared.Extensions
{
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public static class HttpResponseMessageExtensions
    {
        public static async Task<(T, HttpStatusCode)> GetResponse<T>(this HttpResponseMessage message)
        {
            message.EnsureSuccessStatusCode();
            return (await message.Content.ReadFromJsonAsync<T>(), message.StatusCode);
        }
    }
}