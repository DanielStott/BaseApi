using System.Threading.Tasks;
using NUnit.Framework;
using Test.Configuration;

namespace Test.Integration;

[SetUpFixture]
public class Test
{
    public static TestApplication Application = new ();

    [OneTimeSetUp]
    public async Task Setup()
    {
        Application = new TestApplication();
        await Application.SeedTestData();
    }
}