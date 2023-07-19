using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Shared.Exceptions;
using Domain.Users.Interfaces;
using Domain.Users.Models;
using MediatR;

namespace Domain.Users.Handlers;

public class GetUser
{
    public record Query(Guid UserId) : IRequest<User>;

    public class Handler : IRequestHandler<Query, User>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<User> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.UserId);

            if (user is null)
                throw new NotFoundException("User not found");

            return user;
        }
    }
}