using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface.UseCases;
using Pacagroup.Ecommerce.Transversal.Common;
using Swashbuckle.AspNetCore.Annotations;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers.v2;

[Authorize]
[EnableRateLimiting("fixedWindow")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("2.0")]
[SwaggerTag("Get categories of products")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesApplication categoriesApplication;

    public CategoriesController(ICategoriesApplication categoriesApplication)
    {
        this.categoriesApplication = categoriesApplication;
    }

    [HttpGet("GetAll")]
    [SwaggerOperation(Summary = "Get Categories", 
        Description = "This endpoint returns all categories", 
        OperationId = "GetAll",
        Tags = new string[] {"GetAll"}
    )]
    [SwaggerResponse(200,"List of categories", typeof(Response<IEnumerable<CategoryDTO>>))]
    [SwaggerResponse(404,"Categories Not Found")]
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
