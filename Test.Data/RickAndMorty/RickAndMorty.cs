using Domain.Users.Models;

namespace Data;

public class RickAndMorty : ITestSuite
{
    public ITestData<User> Users { get; init; } = new Users();
}