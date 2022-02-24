using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Domain.Shared.Extensions;
using Domain.Shared.Interfaces;
using Domain.Users.Interfaces;
using Domain.Users.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Storage.Users;
using Test.Configuration;

namespace Test.E2E;

[SetUpFixture]
public class TestApi
{
    private static TestApplication _testApplication;

    [OneTimeSetUp]
    public async Task Setup()
    {
        _testApplication = new TestApplication();
        await _testApplication.SeedTestData();
    }

    public static async Task<(T, HttpStatusCode)> Get<T>(string endpointName, object urlValues = null)
    {
        var url = GetUrl(endpointName, urlValues);
        var httpMessage = await _testApplication.Client.GetAsync(url);
        return await httpMessage.GetResponse<T>();
    }

    public static async Task<(T, HttpStatusCode)> Get<T>(string endpointName, Guid id)
        => await Get<T>(endpointName, new { Id = id });

    public static async Task<(T, HttpStatusCode)> Get<T>(string endpointName, string paramName, Guid id)
    {
        var paramDict = new Dictionary<string, object>
        {
            [paramName] = id,
        };
        return await Get<T>(endpointName, paramDict);
    }

    public static async Task<(T, HttpStatusCode)> Get<T>(string endpointName, Dictionary<string, object> paramDict)
    {
        var url = GetUrl(endpointName, paramDict);
        var httpMessage = await _testApplication.Client.GetAsync(url);
        return await httpMessage.GetResponse<T>();
    }

    public static Task<(TResponse, HttpStatusCode)> Post<TRequest, TResponse>(string endpointName, TRequest request)
        => Post<TRequest, TResponse>(endpointName, request, null);

    public static async Task<(TResponse, HttpStatusCode)> Post<TRequest, TResponse>(string endpointName, TRequest request, object urlValues)
    {
        var url = GetUrl(endpointName, urlValues);
        var response = await _testApplication.Client.PostAsJsonAsync(url, request);
        return await response.GetResponse<TResponse>();
    }

    private static string GetUrl(string endpointName, object values)
        => $"http://localhost{_testApplication.LinkGenerator.GetPathByName(endpointName, values)}";

    public static T GetService<T>()
        => _testApplication.GetService<T>();
}