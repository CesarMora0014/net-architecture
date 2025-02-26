using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Application.Main;
using Pacagroup.Ecommerce.Domain.Core;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Infraestructure.Data;
using Pacagroup.Ecommerce.Infraestructure.Interface;
using Pacagroup.Ecommerce.Infraestructure.Repository;
using Pacagroup.Ecommerce.Transversal.Common;
using Pacagroup.Ecommerce.Transversal.Logging;

namespace Pacagroup.Ecommerce.Services.WebApi.Infrastructure.RegisterServices;

public static class RegisterLayersServices
{
    public static IServiceCollection RegisterApplicationInterfaces(this IServiceCollection services)
    {
        services.AddScoped<ICustumerApplication, CustomerApplication>();
        services.AddScoped<IUserApplication, UserApplication>();

        return services;
    }

    public static IServiceCollection RegisterDomainInterfaces(this IServiceCollection services)
    {
        services.AddScoped<ICustomersDomain, CustomersDomain>();
        services.AddScoped<IUserDomain, UserDomain>();

        return services;
    }

    public static IServiceCollection RegisterInfrastructureInterfaces(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();
        services.AddScoped<ICustomersRepository, CustomerRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection RegisterCommonInterfaces(this IServiceCollection services)
    {
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }


}
