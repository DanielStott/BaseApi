﻿namespace Domain.Users.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Interfaces;
    using Models;
    using MediatR;

    public class GetUser
    {
        public class Query : IRequest<User>
        {
            public Guid UserId { get; set; }
            public Query(Guid userId)
            {
                UserId = userId;
            }
        }

        public class Handler : IRequestHandler<Query, User>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<User> Handle(Query request, CancellationToken cancellationToken)
            {
                var createdUser = await _userRepository.GetById(request.UserId);

                return createdUser;
            }
        }
    }
}