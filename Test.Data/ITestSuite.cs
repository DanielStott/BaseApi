using Domain.Users.Models;

namespace Data;

public interface ITestSuite
{
    ITestData<User> Users { get; init; }
}