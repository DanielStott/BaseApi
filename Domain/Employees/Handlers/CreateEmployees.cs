using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Employees.Interfaces;
using Domain.Employees.Models;
using FluentValidation;
using MediatR;

namespace Domain.Employees.Handlers;

public class CreateEmployees
{
    public record Command(IEnumerable<Employee> Employees) : IRequest<IEnumerable<Employee>>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleForEach(x => x.Employees)
                .ChildRules(Rules =>
                {
                    Rules.RuleFor(m => m.Email)
                        .MaximumLength(255)
                        .EmailAddress()
                        .NotEmpty();
                    Rules.RuleFor(m => m.FirstName)
                        .MaximumLength(255)
                        .NotEmpty();
                    Rules.RuleFor(m => m.LastName)
                        .MaximumLength(255)
                        .NotEmpty();
                });
        }
    }

    public class Handler : IRequestHandler<Command, IEnumerable<Employee>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public Handler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> Handle(Command request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.AddRange(request.Employees);
        }
    }
}