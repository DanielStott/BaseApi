﻿using System.Collections.Generic;

namespace Domain.Users.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Shared.Attributes;
    using Domain.Users.Interfaces;
    using Domain.Users.Models;
    using FluentValidation;
    using MediatR;

    public class CreateUsers
    {
        public record Command(IEnumerable<User> users) : IRequest<IEnumerable<User>>;

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleForEach(x => x.users)
                    .ChildRules(Rules =>
                    {
                        Rules.RuleFor(m => m.Username)
                            .MaximumLength(30)
                            .NotEmpty();
                        Rules.RuleFor(m => m.Email)
                            .MaximumLength(255)
                            .EmailAddress()
                            .NotEmpty();
                        Rules.RuleFor(m => m.Password)
                            .MaximumLength(255)
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

        public class Handler : IRequestHandler<Command, IEnumerable<User>>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<IEnumerable<User>> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _userRepository.AddRange(request.users);
            }
        }
    }
}