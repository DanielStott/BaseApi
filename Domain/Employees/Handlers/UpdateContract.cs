using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Employees.Interfaces;
using Domain.Employees.Models;
using Domain.Shared.Exceptions;
using FluentValidation;
using MediatR;

namespace Domain.Employees.Handlers;

public class UpdateContract
{
    public record Command(
        string JobTitle,
        decimal Salary,
        DateTime StartDate,
        DateTime? EndDate = null) : IRequest<Employee>
    {
        public Guid EmployeeId { get; init; }

        public Command WithId(Guid id) => this with
        {
            EmployeeId = id
        };
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.JobTitle)
                .MaximumLength(255)
                .NotEmpty();
            RuleFor(m => m.Salary)
                .GreaterThan(0)
                .NotEmpty();
            RuleFor(m => m.StartDate)
                .NotEmpty();
            RuleFor(m => m.EndDate)
                .GreaterThan(m => m.StartDate)
                .When(m => m.EndDate.HasValue)
                .WithMessage("End date must be after the start date");
        }
    }

    public class Handler : IRequestHandler<Command, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public Handler(IEmployeeRepository employeeRepository) => _employeeRepository = employeeRepository;

        public async Task<Employee> Handle(Command command, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetById(command.EmployeeId);

            employee.SetContract(command.JobTitle, command.Salary, command.StartDate, command.EndDate);

            await _employeeRepository.Update(employee);

            return employee;
        }
    }
}