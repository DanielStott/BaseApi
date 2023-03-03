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

    [HttpPost("Create", Name = nameof(Create))]
    public async Task<ActionResult<UserViewModel>> Create(CreateUser.Command command)
    {
        var user = await _mediator.Send(command);
        var viewModel = _mapper.Map<UserViewModel>(user);

        return Ok(viewModel);
    }

    [HttpGet("{id:guid}", Name = nameof(Get))]
    public async Task<ActionResult<UserViewModel>> Get(Guid id)
    {
        GetUser.Query query = new GetUser.Query(id);
        var user = await _mediator.Send(query);
        var viewModel = _mapper.Map<UserViewModel>(user);
        return Ok(viewModel);
    }
}