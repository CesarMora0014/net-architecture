using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Pacagroup.Ecommerce.Application.UseCases.Customers.Commands.CreateCustomerCommand;
using Pacagroup.Ecommerce.Application.UseCases.Customers.Commands.UpdateCustomerCommand;
using Pacagroup.Ecommerce.Application.UseCases.Customers.Queries.GetCustomerQuery;
using Pacagroup.Ecommerce.Application.UseCases.Customers.Commands.DeleteCustomerCommand;
using Pacagroup.Ecommerce.Application.UseCases.Customers.Queries.GetAllCustomerQuery;
using Pacagroup.Ecommerce.Application.UseCases.Customers.Queries.GetAllPaginatedCustomerQuery;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers.v3;

[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("3.0")]
public class CustomerController : Controller
{
    private readonly IMediator mediator;

    public CustomerController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("Insert")]
    public async Task<IActionResult> Insert([FromBody] CreateCustomerCommand command)
    {
        if (command == null) return BadRequest();

        var response = await mediator.Send(command);

        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPut("Update/{customerId}")]
    public async Task<IActionResult> Update(string customerId, [FromBody] UpdateCustomerCommand command)
    {
        var responseCustomer = await mediator.Send(new GetCustomerQuery() { CustomerId = customerId });

        if (responseCustomer.Data == null) return BadRequest(responseCustomer);

        if (command == null) return BadRequest();

        var response = await mediator.Send(command);

        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpDelete("Delete/{customerId}")]
    public async Task<IActionResult> Delete([FromRoute] string customerId)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest();

        var response = await mediator.Send(new DeleteCustomerCommand() { CustomerID = customerId });

        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpGet("Get/{customerId}")]
    public async Task<IActionResult> GetAsync([FromRoute] string customerId)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest();

        var response = await mediator.Send(new GetCustomerQuery() { CustomerId = customerId });

        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var response = await mediator.Send(new GetAllCustomerQuery());

        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpGet("GetAllPaginated")]
    public async Task<IActionResult> GetAllPaginated([FromQuery] int pageNumber, int pageSize)
    {
        var response = await mediator.Send(new GetAllPaginatedCustomerQuery() { PageNumber = pageNumber, PageSize = pageSize });

        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }

}
