using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
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

    [Test]
    public void fail_on_employee_not_found()
    {
        var requestFailure = Assert.ThrowsAsync<RequestFailure>(async () => await Api.Get($"/api/employees/{Guid.NewGuid()}"));

        Assert.Multiple(() =>
        {
            Assert.That(requestFailure?.Response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(requestFailure.Message.Contains("Employee could not be found"));
        });
    }

    [Test]
    public void fail_on_contract_not_found()
    {
        var command = new
        {
            JobTitle = "Scientist",
            Salary = 1m,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(1),
        };

        var requestFailure = Assert.ThrowsAsync<RequestFailure>(async () => await Api.Put($"/api/employees/{Guid.NewGuid()}/contract", command));

        Assert.Multiple(() =>
        {
            Assert.That(requestFailure?.Response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(requestFailure.Message.Contains("Employee could not be found"));
        });
    }

    [Test]
    public void fail_on_invalid_create_employee()
    {
        var command = new
        {
            Email = string.Empty,
            FirstName = string.Empty,
            LastName = string.Empty,
        };

        var requestFailure = Assert.ThrowsAsync<RequestFailure>(async () => await Api.Post("/api/employees", command));

        Assert.Multiple(() =>
        {
            Assert.That(requestFailure?.Response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(requestFailure.Errors["Email"].Any(x => x.ErrorMessage == "'Email' is not a valid email address."));
            Assert.That(requestFailure.Errors["Email"].Any(x => x.ErrorMessage == "'Email' must not be empty."));
            Assert.That(requestFailure.Errors["FirstName"].Any(x => x.ErrorMessage == "'First Name' must not be empty."));
            Assert.That(requestFailure.Errors["LastName"].Any(x => x.ErrorMessage == "'Last Name' must not be empty."));
        });
    }

    [Test]
    public async Task fail_on_invalid_update_contract()
    {
        var (_, employee) = await CreateEmployee();

        var command = new
        {
            JobTitle = string.Empty,
            Salary = 0m,
            StartDate = default(DateTime),
            EndDate = default(DateTime),
        };

        var requestFailure = Assert.ThrowsAsync<RequestFailure>(async () => await Api.Put($"/api/employees/{employee.id}/contract", command));

        Assert.Multiple(() =>
        {
            Assert.That(requestFailure?.Response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(requestFailure.Errors["JobTitle"].Any(x => x.ErrorMessage == "'Job Title' must not be empty."));
            Assert.That(requestFailure.Errors["Salary"].Any(x => x.ErrorMessage == "'Salary' must not be empty."));
            Assert.That(requestFailure.Errors["Salary"].Any(x => x.ErrorMessage == "'Salary' must be greater than '0'."));
            Assert.That(requestFailure.Errors["StartDate"].Any(x => x.ErrorMessage == "'Start Date' must not be empty."));
            Assert.That(requestFailure.Errors["EndDate"].Any(x => x.ErrorMessage == "'End Date' must be after the start date."));
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