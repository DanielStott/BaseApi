using Data.Users;
using Domain.Employees.Interfaces;
using Test.Data;

namespace Test.Configuration;

public class Seeder
{
    private readonly UserContext _userContext;
    private readonly IEmployeeRepository _employeeRepository;

    public Seeder(UserContext userContext, IEmployeeRepository employeeRepository)
    {
        _userContext = userContext;
        _employeeRepository = employeeRepository;
    }

    public async Task Seed()
    {
        await _userContext.BuildTable();
        await _userContext.AddRange(TestSuites.GetAll(x => x.Users));
        await _employeeRepository.AddRange(TestSuites.GetAll(x => x.Employees));
    }
}