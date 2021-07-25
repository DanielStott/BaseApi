namespace Data
{
    using Domain.Users.Models;

    public class RickAndMorty : ITestSuite
    {
        public ITestData<User> Users { get; init; } = new Users();
    }
}