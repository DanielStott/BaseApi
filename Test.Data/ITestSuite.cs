using Domain.Employees.Models;
using Domain.Users.Models;

namespace Test.Data;

public interface ITestSuite
{
    ITestData<User> Users { get; init; }
    ITestData<Employee> Employees { get; init; }
}