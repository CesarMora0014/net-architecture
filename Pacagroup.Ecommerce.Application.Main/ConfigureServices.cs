using Microsoft.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Application.Interface.UseCases;
using Pacagroup.Ecommerce.Application.UseCases.Categories;
using Pacagroup.Ecommerce.Application.UseCases.Customers;
using Pacagroup.Ecommerce.Application.UseCases.Discounts;
using Pacagroup.Ecommerce.Application.UseCases.Users;
using Pacagroup.Ecommerce.Application.Validator;
using System.Reflection;

namespace Pacagroup.Ecommerce.Application.UseCases;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICustumerApplication, CustomerApplication>();
        services.AddScoped<IUserApplication, UserApplication>();
        services.AddScoped<ICategoriesApplication, CategoriesApplication>();
        services.AddScoped<IDiscountApplication, DiscountApplication>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddTransient<UsersDTOValidator>();
        services.AddTransient<DiscountDTOValidator>();


        return services;
    }
}
