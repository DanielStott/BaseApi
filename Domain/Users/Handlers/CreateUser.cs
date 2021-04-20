using System.Threading;
using System.Threading.Tasks;
using Domain.Users.Models;
using MediatR;

namespace Domain.Users.Handlers
{
    public class CreateUser
    {
        public class Command : IRequest<User>
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
        
        public class Handler : IRequestHandler<Command, User>
        {
            public async Task<User> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = User.Create(request.Email, request.Password, request.FirstName, request.LastName);
                return await Task.FromResult(user);
            }
        }
    }
}