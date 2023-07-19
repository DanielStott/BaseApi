using System.Threading;
using System.Threading.Tasks;
using Domain.Employees.Interfaces;
using Domain.Employees.Models;
using Domain.Shared.Exceptions;
using FluentValidation;
using MediatR;

namespace Domain.Employees.Handlers;

public class CreateEmployee
{
    public record Command(
        string Email,
        string FirstName,
        string LastName) : IRequest<Employee>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.Email)
                .MaximumLength(255)
                .EmailAddress()
                .NotEmpty();
            RuleFor(m => m.FirstName)
                .MaximumLength(255)
                .NotEmpty();
            RuleFor(m => m.LastName)
                .MaximumLength(255)
                .NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public Handler(IEmployeeRepository employeeRepository) => _employeeRepository = employeeRepository;

        public async Task<Employee> Handle(Command command, CancellationToken cancellationToken)
        {
            await ValidateRequest(command);

            var employee = new Employee(command.Email, command.FirstName, command.LastName);

            var createdUser = await _employeeRepository.Add(employee);

            return createdUser;
        }

        private async Task ValidateRequest(Command command)
        {
            var employee = await _employeeRepository.GetByEmail(command.Email);

            if (employee is null)
                return;

            if (employee.Email == command.Email)
                throw new AlreadyExistsExceptions(nameof(command.Email));

            throw new AlreadyExistsExceptions(nameof(command.Email));
        }
    }
}