namespace Api.Controllers.Users
{
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

        [HttpPost(Name = nameof(Create))]
        public async Task<ActionResult<UserViewModel>> Create(CreateUser.Command command)
        {
            var user = await _mediator.Send(command);
            var viewModel = _mapper.Map<UserViewModel>(user);

            return Ok(viewModel);
        }
    }
}