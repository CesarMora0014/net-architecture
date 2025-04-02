using Pacagroup.Ecommerce.Application.Interface.Presentation;
using Pacagroup.Ecommerce.Application.UseCases.Common.Constants;
using System.Security.Claims;

namespace Pacagroup.Ecommerce.Services.WebApi.Services;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor contextAccessor;

    public CurrentUser(IHttpContextAccessor contextAccessor)
    {
        this.contextAccessor = contextAccessor;
    }

    public string UserId => contextAccessor.HttpContext?.User?.FindFirstValue("userId") ?? GlobalConstants.DefaultUserId;

    public string UserName => contextAccessor.HttpContext?.User?.FindFirstValue("username") ?? GlobalConstants.DefaultUserName;
}
