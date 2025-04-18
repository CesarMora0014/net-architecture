﻿using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface.UseCases;
using Pacagroup.Ecommerce.Application.UseCases.Users.Commands.CreateUserTokenCommand;
using Pacagroup.Ecommerce.Services.WebApi.Helpers;
using Pacagroup.Ecommerce.Transversal.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers.v3;

[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("3.0")]
public class UserController : Controller
{
    private readonly IMediator mediator;
    private readonly AppSettings appSettings;

    public UserController(IMediator mediator, IOptions<AppSettings> appSettings)
    {
        this.mediator = mediator;
        this.appSettings = appSettings.Value;
    }

    [AllowAnonymous]
    [HttpPost("Authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] CreateUserTokenCommand command)
    {
        var response = await mediator.Send(command);

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

        var claims = new Dictionary<string, object>()
        {
            { "userId", userDTO.Data.UserId.ToString() },
            { "username", userDTO.Data.UserName },
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]{
                new Claim(ClaimTypes.Name, userDTO.Data.UserId.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = appSettings.Issuer,
            Audience = appSettings.Audience,
            Claims = claims
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
