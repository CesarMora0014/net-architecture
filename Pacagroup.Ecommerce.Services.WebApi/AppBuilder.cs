using Pacagroup.Ecommerce.Application.UseCases;
using Pacagroup.Ecommerce.Persistence;
using Pacagroup.Ecommerce.Services.WebApi.Helpers;
using Pacagroup.Ecommerce.Services.WebApi.Infrastructure.RegisterServices;

namespace Pacagroup.Ecommerce.Services.WebApi;

public class AppBuilder
{
    public static WebApplication Build(string[] args = default)
    {
        var builder = WebApplication.CreateBuilder(args);
        var appSettingSection = builder.Configuration.GetSection("Config");
        builder.Services.Configure<AppSettings>(appSettingSection);

        var corsPolicy = "policyApiEcommerce";


        // Add services to the container.
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(corsPolicy, policyBuilder =>
            {
                policyBuilder
                .WithOrigins(builder.Configuration["Config:OriginCors"]!)
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        builder.Services.AddControllers();

        builder.Services.RegisterCommonInterfaces();

        builder.Services.AddPersistenceServices();
        builder.Services.AddApplicationServices();

        builder.Services.RegisterAutoMapper();
        builder.Services.RegisterJwtAuthentication(appSettingSection);
        builder.Services.RegisterAPIVersioning();
        builder.Services.RegisterSwagger();
        builder.Services.RegisterValidators();
        builder.Services.RegisterHealthCheck(builder.Configuration);
        builder.Services.RegisterWatchDog(builder.Configuration);
        builder.Services.RegisterRedis(builder.Configuration);
        builder.Services.RegisterRateLimiter(builder.Configuration);

        return builder.Build();
    }
}
