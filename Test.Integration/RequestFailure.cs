using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Test.Integration;

public class RequestFailure : Exception
{
    public HttpResponseMessage Response { get; }
    public string Content { get; }
    public Dictionary<string, List<Error>> Errors { get; }

    public RequestFailure(HttpResponseMessage response, string content)
        : base($"Request failed: {(int)response.StatusCode} - {response.StatusCode}\r\n\r\n{content}")
    {
        Response = response;
        Content = content;
        Errors = ExtractErrors(content);
    }

    public static async Task<RequestFailure> From(HttpResponseMessage response) => new (response, await response.Content.ReadAsStringAsync());

    private static Dictionary<string, List<Error>> ExtractErrors(string content)
    {
        ValidationProblemDetails problemDetails;
        try
        {
            problemDetails = JsonConvert.DeserializeObject<ValidationProblemDetails>(content);
        }
        catch (Exception)
        {
            return null;
        }

        if (problemDetails?.Errors == null)
            return null;

        Dictionary<string, List<Error>> errors = new();
        foreach (var (k, v) in problemDetails.Errors)
        {
            var errorList = v.Select(error => new Error(k, error)).ToList();

            errors.Add(k, errorList);
        }

        return errors;
    }
}

public record Error(string PropertyName, string ErrorMessage);