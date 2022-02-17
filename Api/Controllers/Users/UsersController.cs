namespace Api.Controllers.Users
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Domain.Users.Handlers;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

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

        // Get /Users/{userID} //         [HttpGet("{userId:guid}")] adds id in Get(userId) with values -  
        [HttpGet("{userId:guid}")] //GET 01233 from postman to code ()
        public async Task<ActionResult<UserViewModel>> Get(Guid userId) //id is 012233 whatever function name it will know id 124343
        {
            GetUser.Query query = new GetUser.Query(userId);
            var user = await _mediator.Send(query);
            var viewModel = _mapper.Map<UserViewModel>(user);
            return Ok(viewModel);
        }
    }
}