namespace Data
{
    using Domain.Users.Models;

    public interface ITestSuite
    {
        ITestData<User> Users { get; init; }
    }
}