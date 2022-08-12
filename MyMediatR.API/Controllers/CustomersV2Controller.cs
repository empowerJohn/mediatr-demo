using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyMediatR.API.Application.Customers.Commands;
using MyMediatR.API.Application.Customers.Queries;
using MyMediatR.API.Models;

namespace MyMediatR.API.Controllers;

[Route("api/v2/[controller]")]
[ApiController]
public class CustomersV2Controller : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersV2Controller(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost()]
    public async Task<IActionResult> PostAsync([FromBody] CreateCustomer customer, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateCustomerCommand(customer.FirstName, customer.LastName), cancellationToken);

        return result != null
            ? Ok(result)
            : BadRequest();
    }

    [HttpGet("{customerId:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int customerId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCustomerByIdQuery(customerId), cancellationToken);

        return result != null
            ? Ok(result)
            : NotFound();
    }
}
