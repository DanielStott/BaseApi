using System;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Test.Integration;

public abstract class BaseTest
{
    protected HttpClient Api { get; private set; }

    [SetUp]
    public virtual Task Setup()
    {
        Api = Test.Application.GetClient() ??
              throw new InvalidOperationException("Create a new instance of TestApplication<T> before calling BaseTest<T>");
        return Task.CompletedTask;
    }

    [TearDown]
    public virtual void TearDown() => Api.Dispose();
}