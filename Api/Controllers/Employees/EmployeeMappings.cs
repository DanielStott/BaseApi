using AutoMapper;
using Domain.Employees.Models;

namespace Api.Controllers.Employees;

public class EmployeesMappings : Profile
{
    public EmployeesMappings()
    {
        CreateMap<Employee, EmployeesViewModel>();
    }
}