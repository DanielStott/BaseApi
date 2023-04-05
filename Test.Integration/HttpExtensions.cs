using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Test.Integration;

public static class HttpClientExtensions
{
    public static async Task<(HttpResponseMessage Response, dynamic Content)> Get(this HttpClient http, object requestUri) =>
         await Get<dynamic>(http, requestUri);

    public static async Task<(HttpResponseMessage Response, TResponse Content)> Get<TResponse>(this HttpClient http, object requestUri) =>
        await Result<TResponse>(await http.GetAsync($"{requestUri}"));

    public static async Task<(HttpResponseMessage Response, dynamic Content)> Post(this HttpClient http, object requestUri, object request = null) =>
        await Post<dynamic>(http, requestUri, request);

    public static async Task<(HttpResponseMessage Response, TResponse Content)> Post<TResponse>(this HttpClient http, object requestUri, object request = null) =>
        await Result<TResponse>(await http.PostAsJsonAsync($"{requestUri}", request));

    public static async Task<(HttpResponseMessage Response, dynamic Content)> Post(this HttpClient http, object requestUri, HttpContent content) =>
        await Post<dynamic>(http, requestUri, content);

    public static async Task<(HttpResponseMessage Response, TResponse Content)> Post<TResponse>(this HttpClient http, object requestUri, HttpContent content) =>
        await Result<TResponse>(await http.PostAsync($"{requestUri}", content));

    public static async Task<(HttpResponseMessage Response, dynamic Content)> Put(this HttpClient http, object requestUri, object request = null) =>
        await Put<dynamic>(http, requestUri, request);

    public static async Task<(HttpResponseMessage Response, TResponse Content)> Put<TResponse>(this HttpClient http, object requestUri, object request = null) =>
        await Result<TResponse>(await http.PutAsJsonAsync($"{requestUri}", request));

    public static async Task<(HttpResponseMessage Response, dynamic Content)> Delete(this HttpClient http, object requestUri) =>
        await Delete<dynamic>(http, requestUri);

    public static async Task<(HttpResponseMessage Response, TResponse Content)> Delete<TResponse>(this HttpClient http, object requestUri) =>
        await Result<TResponse>(await http.DeleteAsync($"{requestUri}"));

    private static async Task<(HttpResponseMessage, TResponse)> Result<TResponse>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.Redirect)
        {
            return (response, await GetResult<TResponse>(response));
        }

        throw await RequestFailure.From(response);
    }

    private static async Task<TResult> GetResult<TResult>(HttpResponseMessage response)
    {
        if (response.Content.Headers.ContentType is { MediaType: "application/json" })
            return Deserialize<TResult>(await response.Content.ReadAsStringAsync());

        return (dynamic)await response.Content.ReadAsStringAsync();
    }

    private static TResult Deserialize<TResult>(string content)
    {
        try
        {
            return JsonConvert.DeserializeObject<TResult>(content);
        }
        catch (Exception)
        {
            Console.WriteLine(content);
            throw;
        }
    }
}