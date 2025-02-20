using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CustomerController : Controller
{
    private readonly ICustumerApplication _customerApplication;

    public CustomerController(ICustumerApplication custumerApplication)
    {
        _customerApplication = custumerApplication;
    }

    #region Métodos síncronos

    [HttpPost("Insert")]
    public IActionResult Insert([FromBody] CustomerDTO customerDTO)
    {
        if (customerDTO == null) return BadRequest();

        var response = _customerApplication.Insert(customerDTO);

        if(!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPut("Update")]
    public IActionResult Update([FromBody] CustomerDTO customerDTO)
    {
        if (customerDTO == null) return BadRequest();

        var response = _customerApplication.Update(customerDTO);

        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpDelete("Delete/{customerId}")]
    public IActionResult Delete(string customerId)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest();

        var response = _customerApplication.Delete(customerId);

        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpGet("Get/{customerId}")]
    public IActionResult Get(string customerId)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest();

        var response = _customerApplication.Get(customerId);

        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var response = _customerApplication.GetAll();

        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }

    #endregion

    #region Métodos asíncronos

    [HttpPost("InsertAsync")]
    public async Task<IActionResult> InsertAsync([FromBody] CustomerDTO customerDTO)
    {
        if (customerDTO == null) return BadRequest();

        var response = await _customerApplication.InsertAsync(customerDTO);

        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPut("UpdateAsync")]
    public async Task<IActionResult> UpdateAsync([FromBody] CustomerDTO customerDTO)
    {
        if (customerDTO == null) return BadRequest();

        var response = await _customerApplication.UpdateAsync(customerDTO);

        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpDelete("DeleteAsync/{customerId}")]
    public async Task<IActionResult> DeleteAsync(string customerId)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest();

        var response = await _customerApplication.DeleteAsync(customerId);

        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpGet("GetAsync/{customerId}")]
    public async Task<IActionResult> GetAsync(string customerId)
    {
        if (string.IsNullOrEmpty(customerId)) return BadRequest();

        var response = await _customerApplication.GetAsync(customerId);

        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpGet("GetAllAsync")]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = await _customerApplication.GetAllAsync();

        if (!response.IsSuccess)
            return BadRequest(response);

        return Ok(response);
    }

    #endregion

}
