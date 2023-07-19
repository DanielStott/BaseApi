using System.Threading.Tasks;
using Domain.Employees.Models;
using Domain.Shared.Interfaces;

namespace Domain.Employees.Interfaces;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<Employee> GetByEmailOrUsername(string email, string username);
}