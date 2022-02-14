namespace Domain.Users.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Shared.Attributes;
    using Domain.Shared.Exceptions;
    using Domain.Users.Interfaces;
    using Domain.Users.Models;
    using FluentValidation;
    using MediatR;

    public class CreateUser
    {
        public record Command : IRequest<User>
        {
            public string Username { get; set; }
            public string Email { get; set; }
            [DontLog]
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

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

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<User> Handle(Command request, CancellationToken cancellationToken)
            {
                await ValidateRequest(request);

                var user = User.Create(request.Username, request.Email, request.Password, request.FirstName, request.LastName);

                var createdUser = await _userRepository.Add(user);

                return createdUser;
            }

            private async Task ValidateRequest(Command request)
            {
                var userAlreadyExists = await _userRepository.GetByEmailOrUsername(request.Email, request.Username);

                if (userAlreadyExists is not null)
                {
                    if (userAlreadyExists.Username == request.Username)
                        throw new AlreadyExistsExceptions(nameof(request.Username));

                    throw new AlreadyExistsExceptions(nameof(request.Email));
                }
            }
        }
    }
}