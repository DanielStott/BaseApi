using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Employees.Interfaces;
using Domain.Employees.Models;
using Domain.Shared.Interfaces;

namespace Data.Employees;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IMongoStore<Employee> _employeeStore;

    public EmployeeRepository(IMongoStore<Employee> employeeStore) => _employeeStore = employeeStore;

    public async Task<Employee?> GetById(Guid id) =>
        await _employeeStore.Find(x => x.Id == id);

    public async Task<Employee?> Get(Employee entity) =>
        await _employeeStore.Find(x => x == entity);

    public Task<IEnumerable<Employee?>> GetAll() => throw new NotImplementedException();
    public async Task<Employee> Add(Employee entity)
    {
        await _employeeStore.Insert(entity);
        return entity;
    }

    public async Task<IEnumerable<Employee>> AddRange(IEnumerable<Employee> entities)
    {
        await _employeeStore.InsertMany(entities);
        return entities;
    }

    public async Task<Employee> Update(Employee entity)
    {
        await _employeeStore.Replace(x => x.Id == entity.Id, entity);
        return entity;
    }

    public async Task<Employee?> GetByEmail(string email) =>
        await _employeeStore.Find(x => x.Email == email);
}