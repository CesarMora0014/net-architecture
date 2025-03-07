using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface.UseCases;
using Pacagroup.Ecommerce.Services.WebApi.Helpers;
using Pacagroup.Ecommerce.Transversal.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers.v1;

[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0", Deprecated = true)]
public class UserController : Controller
{
    private readonly IUserApplication userApplication;
    private readonly AppSettings appSettings;

    public UserController(IUserApplication userApplication, IOptions<AppSettings> appSettings)
    {
        this.userApplication = userApplication;
        this.appSettings = appSettings.Value;
    }

    [AllowAnonymous]
    [HttpPost("Authenticate")]
    public IActionResult Authenticate([FromBody] UserDTO userDTO)
    {
        var response = userApplication.Authenticate(userDTO.UserName, userDTO.Password);

        if (response == null)
            return BadRequest(response);

        if (!response.IsSuccess)
            return NotFound(response);

        response.Data.Token = BuildToken(response);

        return Ok(response);
    }

    private string BuildToken(Response<UserDTO> userDTO)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]{
                new Claim(ClaimTypes.Name, userDTO.Data.UserId.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = appSettings.Issuer,
            Audience = appSettings.Audience,
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
