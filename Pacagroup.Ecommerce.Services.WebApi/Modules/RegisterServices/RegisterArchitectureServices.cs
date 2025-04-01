using Pacagroup.Ecommerce.Services.WebApi.Modules.GlobalException;

namespace Pacagroup.Ecommerce.Services.WebApi.Infrastructure.RegisterServices;

public static class RegisterArchitectureServices
{

    public static IServiceCollection RegisterCommonInterfaces(this IServiceCollection services)
    {
        services.AddTransient<GlobalExceptionHandler>();
        return services;
    }


}
