using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Application.Interface.UseCases;
using Pacagroup.Ecommerce.Application.UseCases.Categories;
using Pacagroup.Ecommerce.Application.UseCases.Common.Behaviours;
using Pacagroup.Ecommerce.Application.UseCases.Customers;
using Pacagroup.Ecommerce.Application.UseCases.Discounts;
using Pacagroup.Ecommerce.Application.UseCases.Users;
using System.Reflection;

namespace Pacagroup.Ecommerce.Application.UseCases;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LogginBehaviour<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        });

        services.AddScoped<ICustumerApplication, CustomerApplication>();
        services.AddScoped<IUserApplication, UserApplication>();
        services.AddScoped<ICategoriesApplication, CategoriesApplication>();
        services.AddScoped<IDiscountApplication, DiscountApplication>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
