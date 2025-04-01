using Pacagroup.Ecommerce.Services.WebApi.Modules.GlobalException;
using Pacagroup.Ecommerce.Transversal.Common;
using Pacagroup.Ecommerce.Transversal.Logging;

namespace Pacagroup.Ecommerce.Services.WebApi.Infrastructure.RegisterServices;

public static class RegisterArchitectureServices
{

    public static IServiceCollection RegisterCommonInterfaces(this IServiceCollection services)
    {
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
        services.AddTransient<GlobalExceptionHandler>();
        return services;
    }


}
