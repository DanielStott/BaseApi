using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Test.Integration.Employees;

[TestFixture]
public class EmployeeTests : BaseTest
{
    [Test]
    public async Task successfully_create_employee()
    {
        var command = new
        {
            Email = "test@test.com",
            FirstName = "Daniel",
            LastName = "Test",
        };

        var (response, employee) = await Api.Post("/api/employees", command);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That((string)employee.email, Is.EqualTo(command.Email));
            Assert.That((string)employee.firstName, Is.EqualTo(command.FirstName));
            Assert.That((string)employee.lastName, Is.EqualTo(command.LastName));
            Assert.That(employee.id, Is.Not.Null);
        });
    }

    [Test]
    public async Task successfully_get_employee()
    {
        var (response, employee) = await Api.Get($"/api/employees/{Data.Employees.Summer.Id}");

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That((Guid)employee.id, Is.EqualTo(Data.Employees.Summer.Id));
            Assert.That((string)employee.email, Is.EqualTo(Data.Employees.Summer.Email));
            Assert.That((string)employee.firstName, Is.EqualTo(Data.Employees.Summer.FirstName));
            Assert.That((string)employee.lastName, Is.EqualTo(Data.Employees.Summer.LastName));
        });
    }

    [Test]
    public async Task successfully_update_employee_contract()
    {
        var (_, employee) = await CreateEmployee();

        var command = new
        {
            JobTitle = "Scientist",
            Salary = 1m,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
        };

        var (updateResponse, contract) = await Api.Put($"/api/employees/{employee.id}/contract", command);

        Assert.Multiple(() =>
        {
            Assert.That(updateResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That((string)contract.jobTitle, Is.EqualTo(command.JobTitle));
            Assert.That((decimal)contract.salary, Is.EqualTo(command.Salary));
            Assert.That((DateTime)contract.startDate, Is.EqualTo(command.StartDate));
            Assert.That((DateTime)contract.endDate, Is.EqualTo(command.EndDate));
        });
    }

    private async Task<(HttpResponseMessage Response, dynamic Content)> CreateEmployee()
    {
        var command = new
        {
            Email = $"{Guid.NewGuid()}@test.com",
            FirstName = Guid.NewGuid().ToString(),
            LastName = Guid.NewGuid().ToString(),
        };

        return await Api.Post("/api/employees", command);
    }
}