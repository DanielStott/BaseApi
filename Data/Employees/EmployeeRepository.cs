using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Employees.Interfaces;
using Domain.Employees.Models;

namespace Data.Employees;

public class EmployeeRepository : IEmployeeRepository
{
    public EmployeeRepository()
    {
    }

    public Task<Employee> GetById(Guid id) => throw new NotImplementedException();
    public Task<Employee> Get(Employee entity) => throw new NotImplementedException();
    public Task<IEnumerable<Employee>> GetAll() => throw new NotImplementedException();
    public Task<Employee> Add(Employee entity) => throw new NotImplementedException();
    public Task<IEnumerable<Employee>> AddRange(IEnumerable<Employee> entities) => throw new NotImplementedException();
    public Task<Employee> Update(Employee entity) => throw new NotImplementedException();
    public Task<Employee> GetByEmail(string email) => throw new NotImplementedException();
}