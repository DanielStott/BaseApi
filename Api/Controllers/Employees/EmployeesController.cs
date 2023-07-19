using AutoMapper;
using Domain.Employees.Handlers;
using Domain.Users.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Employees;

[ApiController]
[Route("[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public EmployeesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<EmployeesViewModel>> Create(CreateEmployee.Command command)
    {
        var employee = await _mediator.Send(command);
        return _mapper.Map<EmployeesViewModel>(employee);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EmployeesViewModel>> Get(Guid id)
    {
        var employee = await _mediator.Send(new GetUser.Query(id));
        return _mapper.Map<EmployeesViewModel>(employee);
    }
}