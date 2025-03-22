using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface.UseCases;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers.v2
{
    [Authorize]
    [EnableRateLimiting("fixedWindow")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountApplication discountApplication;

        public DiscountsController(IDiscountApplication discountApplication)
        {
            this.discountApplication = discountApplication;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] DiscountDTO discountDTO)
        {
            if (discountDTO == null) return BadRequest();

            var response = await discountApplication.Create(discountDTO);

            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DiscountDTO discountDTO)
        {
            if (discountDTO == null) return BadRequest();
            
            var responseDiscount = await discountApplication.Get(id);

            if (responseDiscount.Data == null) return BadRequest(responseDiscount);

            var response = await discountApplication.Update(discountDTO);

            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await discountApplication.Delete(id);

            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await discountApplication.Get(id);

            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await discountApplication.GetAll();

            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPagination(int pageNumber, int pageSize)
        {
            var response = await discountApplication.GetAllWithPagination(pageNumber,pageSize);

            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
