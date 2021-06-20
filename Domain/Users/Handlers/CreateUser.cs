namespace Domain.Users.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Users.Models;
    using MediatR;

    public class CreateUser
    {
        public record Command(string Email, string Password, string FirstName, string LastName) : IRequest<User>;

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