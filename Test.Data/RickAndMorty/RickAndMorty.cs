using Domain.Employees.Models;
using Domain.Users.Models;

namespace Test.Data;

public class RickAndMorty : ITestSuite
{
    public ITestData<User> Users { get; init; } = new Users();
    public ITestData<Employee> Employees { get; init; } = new Employees();
}