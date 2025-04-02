using Pacagroup.Ecommerce.Application.Interface.Presentation;
using Pacagroup.Ecommerce.Services.WebApi.Modules.GlobalException;
using Pacagroup.Ecommerce.Services.WebApi.Services;

namespace Pacagroup.Ecommerce.Services.WebApi.Infrastructure.RegisterServices;

public static class RegisterArchitectureServices
{

    public static IServiceCollection RegisterCommonInterfaces(this IServiceCollection services)
    {
        services.AddTransient<GlobalExceptionHandler>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        return services;
    }


}
