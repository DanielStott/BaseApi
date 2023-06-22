using Domain.Users.Models;

namespace Test.Data;

public class RickAndMorty : ITestSuite
{
    public ITestData<User> Users { get; init; } = new Users();
}