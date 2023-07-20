using System;
using System.Collections.Generic;
using Domain.Employees.Models;
using Domain.Users.Models;

namespace Test.Data;

public class Employees : ITestData<Employee>
{
    public IEnumerable<Employee> All { get; set; } = new List<Employee> { Summer, Jerry };
    public static Employee Summer { get; }
    public static Employee Jerry { get; }

    static Employees()
    {
        Summer = new Employee("Summer@test.com", "Summer", "Smith");
        Summer.SetContract("Daughter", 1m, new DateTime(2021, 1, 1));
        Jerry = new Employee("Jerry@test.com", "Jerry", "Smith");
        Jerry.SetContract("Father", 1m, new DateTime(2022, 1, 1));
    }
}