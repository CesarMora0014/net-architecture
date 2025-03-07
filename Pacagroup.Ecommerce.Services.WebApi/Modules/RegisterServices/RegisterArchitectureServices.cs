using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Application.Interface.UseCases;
using Pacagroup.Ecommerce.Application.UseCases;
using Pacagroup.Ecommerce.Persistence.Data;
using Pacagroup.Ecommerce.Persistence.Repository;
using Pacagroup.Ecommerce.Transversal.Common;
using Pacagroup.Ecommerce.Transversal.Logging;

namespace Pacagroup.Ecommerce.Services.WebApi.Infrastructure.RegisterServices;

public static class RegisterArchitectureServices
{
    public static IServiceCollection RegisterApplicationInterfaces(this IServiceCollection services)
    {
        services.AddScoped<ICustumerApplication, CustomerApplication>();
        services.AddScoped<IUserApplication, UserApplication>();
        services.AddScoped<ICategoriesApplication, CategoriesApplication>();

        return services;
    }

    public static IServiceCollection RegisterInfrastructureInterfaces(this IServiceCollection services)
    {

        services.AddScoped<ICustomersRepository, CustomerRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddSingleton<DapperContext>();

        return services;
    }

    public static IServiceCollection RegisterCommonInterfaces(this IServiceCollection services)
    {
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }


}
