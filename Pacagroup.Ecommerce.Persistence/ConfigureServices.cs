﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Persistence.Contexts;
using Pacagroup.Ecommerce.Persistence.Interceptors;
using Pacagroup.Ecommerce.Persistence.Repositories;

namespace Pacagroup.Ecommerce.Persistence;

public static class ConfigureServices
{

    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<DapperContext>();
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("NorthwindConnection"),
                builder =>
                {
                    builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                });

        });

        services.AddScoped<ICustomersRepository, CustomersRepository>();
        services.AddScoped<IUserRepository, UsersRepository>();
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        services.AddScoped<IDiscountsRepository, DiscountRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
