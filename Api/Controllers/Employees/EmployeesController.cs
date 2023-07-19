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
    public async Task<ActionResult<EmployeeViewModel>> Create(CreateEmployee.Command command)
    {
        var employee = await _mediator.Send(command);
        return _mapper.Map<EmployeeViewModel>(employee);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EmployeeViewModel>> Get(Guid id)
    {
        var employee = await _mediator.Send(new GetUser.Query(id));
        return _mapper.Map<EmployeeViewModel>(employee);
    }

    [HttpPut("{id:guid}/contract")]
    public async Task<ActionResult<EmployeeContractViewModel>> UpdateContract(Guid id, UpdateContract.Command command)
    {
        var employee = await _mediator.Send(command.WithId(id));
        return _mapper.Map<EmployeeContractViewModel>(employee);
    }
}