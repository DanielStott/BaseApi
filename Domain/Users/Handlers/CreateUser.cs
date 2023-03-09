using System.Threading;
using System.Threading.Tasks;
using Domain.Shared.Attributes;
using Domain.Shared.Exceptions;
using Domain.Users.Interfaces;
using Domain.Users.Models;
using FluentValidation;
using MediatR;

namespace Domain.Users.Handlers;

public class CreateUser
{
    public record Command(
        string Username,
        string Email,
        string Password,
        string FirstName,
        string LastName) : IRequest<User>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.Username)
                .MaximumLength(30)
                .NotEmpty();
            RuleFor(m => m.Email)
                .MaximumLength(255)
                .EmailAddress()
                .NotEmpty();
            RuleFor(m => m.Password)
                .MaximumLength(255)
                .NotEmpty();
            RuleFor(m => m.FirstName)
                .MaximumLength(255)
                .NotEmpty();
            RuleFor(m => m.LastName)
                .MaximumLength(255)
                .NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, User>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<User> Handle(Command command, CancellationToken cancellationToken)
        {
            await ValidateRequest(command);

            var user = User.Create(command.Username, command.Email, command.Password, command.FirstName, command.LastName);

            var createdUser = await _userRepository.Add(user);

            return createdUser;
        }

        private async Task ValidateRequest(Command command)
        {
            var user = await _userRepository.GetByEmailOrUsername(command.Email, command.Username);

            if (user is null)
                return;

            if (user.Username == command.Username)
                throw new AlreadyExistsExceptions(nameof(command.Username));

            throw new AlreadyExistsExceptions(nameof(command.Email));
        }
    }
}