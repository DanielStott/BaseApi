using System;
using Domain.Shared.Models;

namespace Domain.Employees.Models;

public class Employee : Entity
{
    public string Email { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public Contract? Contract { get; private set; }

    public Employee(string email, string firstName, string lastName)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public void SetContract(string jobTitle, decimal salary, DateTime startDate, DateTime? endDate = null) =>
        Contract = new Contract(jobTitle, salary, startDate, endDate);
}

public record Contract(string JobTitle, decimal Salary, DateTime StartDate, DateTime? EndDate = null);