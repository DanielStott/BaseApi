using System.Threading.Tasks;
using Domain.Users.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BaseApi.Controllers.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("")]
        public async Task<ActionResult<UserViewModel>> Create(CreateUser.Command command)
        {
            var user = await _mediator.Send(command);
            var viewModel = new UserViewModel()
            {
                Email = user.Email,
                Id = user.Id
            };
            
            return viewModel;
        }
    }
}