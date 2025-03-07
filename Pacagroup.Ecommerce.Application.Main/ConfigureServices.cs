using Microsoft.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Application.Interface.UseCases;
using Pacagroup.Ecommerce.Application.UseCases.Categories;
using Pacagroup.Ecommerce.Application.UseCases.Customers;
using Pacagroup.Ecommerce.Application.UseCases.Users;

namespace Pacagroup.Ecommerce.Application.UseCases;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICustumerApplication, CustomerApplication>();
        services.AddScoped<IUserApplication, UserApplication>();
        services.AddScoped<ICategoriesApplication, CategoriesApplication>();

        return services;
    }
}
