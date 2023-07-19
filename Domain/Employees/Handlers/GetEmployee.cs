using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Employees.Interfaces;
using Domain.Employees.Models;
using MediatR;

namespace Domain.Employees.Handlers;

public class GetEmployee
{
    public record Query(Guid EmployeeId) : IRequest<Employee>;

    public class Handler : IRequestHandler<Query, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public Handler(IEmployeeRepository employeeRepository) => _employeeRepository = employeeRepository;

        public async Task<Employee> Handle(Query request, CancellationToken cancellationToken) =>
            await _employeeRepository.GetById(request.EmployeeId);
    }
}