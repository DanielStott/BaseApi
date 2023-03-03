using System.Threading.Tasks;
using Data;
using Domain.Shared.Interfaces;
using Domain.Users.Handlers;
using Domain.Users.Models;
using MediatR;

namespace Test.Configuration;

public class Seeder
{
    private readonly IMediator _mediator;
    private readonly IContext<User> _userContext;

    public Seeder(IMediator mediator, IContext<User> userContext)
    {
        _mediator = mediator;
        _userContext = userContext;
    }

    public async Task Seed()
    {
        _userContext.BuildTable();
        await _mediator.Send(new CreateUsers.Command(TestSuites.GetAll(x => x.Users)));
    }
}