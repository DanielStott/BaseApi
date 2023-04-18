using System.Threading.Tasks;
using NUnit.Framework;
using Test.Configuration;

namespace Test.Integration;

[SetUpFixture]
public class Test
{
    public static readonly TestApplication Application = new ();

    [OneTimeSetUp]
    public async Task Setup() => await Application.SeedTestData();
}