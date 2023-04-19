using AutoMapper;
using Domain.Users.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Users;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UsersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<UserViewModel>> Create(CreateUser.Command command)
    {
        var user = await _mediator.Send(command);
        return _mapper.Map<UserViewModel>(user);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserViewModel>> Get(Guid id)
    {
        var user = await _mediator.Send(new GetUser.Query(id));
        return _mapper.Map<UserViewModel>(user);
    }
}