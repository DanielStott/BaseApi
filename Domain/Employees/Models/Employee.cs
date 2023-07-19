using System;
using Domain.Shared.Models;

namespace Domain.Employees.Models;

public class Employee : Entity
{
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Contract Contract { get; private set; }

    public Employee(string email, string firstName, string lastName)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public void SetContract(string jobTitle, decimal salary, DateTime startDate, DateTime? endDate = null)
    {
        if (endDate != null && endDate < startDate)
            throw new ArgumentException("End date cannot be before start date");

        if (salary < 0)
            throw new ArgumentException("Salary cannot be negative");

        Contract = new Contract(jobTitle, salary, startDate, endDate);
    }
}

public record Contract(string JobTitle, decimal Salary, DateTime StartDate, DateTime? EndDate = null);