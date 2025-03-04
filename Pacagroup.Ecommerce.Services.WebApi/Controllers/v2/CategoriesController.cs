using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pacagroup.Ecommerce.Application.Interface;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers.v2;

[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("2.0")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesApplication categoriesApplication;

    public CategoriesController(ICategoriesApplication categoriesApplication)
    {
        this.categoriesApplication = categoriesApplication;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var response = await categoriesApplication.GetAll();

        if(response.IsSuccess) 
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

}
