using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class CustomerController : Controller
{
    private readonly ICustumerApplication _customerApplication;

    public CustomerController(ICustumerApplication custumerApplication)
    {
        _customerApplication = custumerApplication;
    }

    #region Métodos síncronos

    [HttpPost]
    public IActionResult InsertCustomer([FromBody] CustomerDTO customerDTO)
    {
        if (customerDTO == null) return BadRequest();

        var response = _customerApplication.Insert(customerDTO);

        if(!response.IsSuccess)
            return BadRequest(response.Message);

        return Ok(response);
    }

    [HttpPut]
    public IActionResult UpdateCustomer([FromBody] CustomerDTO customerDTO)
    {
        if (customerDTO == null) return BadRequest();

        var response = _customerApplication.Update(customerDTO);

        if (!response.IsSuccess)
            return BadRequest(response.Message);

        return Ok(response);
    }

    [HttpDelete("{customerId}")]
    public IActionResult DeleteCustomer(string customerId)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest();

        var response = _customerApplication.Delete(customerId);

        if (!response.IsSuccess)
            return BadRequest(response.Message);

        return Ok(response);
    }

    [HttpGet("{customerId}")]
    public IActionResult GetCustomer(string customerId)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest();

        var response = _customerApplication.Get(customerId);

        if (!response.IsSuccess)
            return BadRequest(response.Message);

        return Ok(response);
    }

    [HttpGet]
    public IActionResult GetAllCustomers()
    {
        var response = _customerApplication.GetAll();

        if (!response.IsSuccess)
            return BadRequest(response.Message);

        return Ok(response);
    }

    #endregion

    #region Métodos asíncronos

    [HttpPost]
    public async Task<IActionResult> InsertAsync([FromBody] CustomerDTO customerDTO)
    {
        if (customerDTO == null) return BadRequest();

        var response = await _customerApplication.InsertAsync(customerDTO);

        if (!response.IsSuccess)
            return BadRequest(response.Message);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] CustomerDTO customerDTO)
    {
        if (customerDTO == null) return BadRequest();

        var response = await _customerApplication.UpdateAsync(customerDTO);

        if (!response.IsSuccess)
            return BadRequest(response.Message);

        return Ok(response);
    }

    [HttpDelete("{customerId}")]
    public async Task<IActionResult> DeleteAsync(string customerId)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest();

        var response = await _customerApplication.DeleteAsync(customerId);

        if (!response.IsSuccess)
            return BadRequest(response.Message);

        return Ok(response);
    }

    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetAsync(string customerId)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest();

        var response = await _customerApplication.GetAsync(customerId);

        if (!response.IsSuccess)
            return BadRequest(response.Message);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = await _customerApplication.GetAllAsync();

        if (!response.IsSuccess)
            return BadRequest(response.Message);

        return Ok(response);
    }

    #endregion

}
