using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Domain.Shared.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task<(T, HttpStatusCode)> GetResponse<T>(this HttpResponseMessage message)
        => (await message.Content.ReadFromJsonAsync<T>(), message.StatusCode);
}