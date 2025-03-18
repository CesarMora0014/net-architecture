using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pacagroup.Ecommerce.Services.WebApi.Helpers;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Config;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;
using WatchDog;

namespace Pacagroup.Ecommerce.Services.WebApi.Infrastructure.RegisterServices;

public static class RegisterServices
{
    public static void RegisterSwagger(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        services.AddSwaggerGen(c =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Scheme = "Bearer",
                Type = SecuritySchemeType.Http,
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme.",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, new List<string>() }
            });
        });
    }

    public static void RegisterJwtAuthentication(this IServiceCollection services, IConfigurationSection appSettingsSection)
    {
        var settings = appSettingsSection.Get<AppSettings>();
        var secretKey = Encoding.ASCII.GetBytes(settings.Secret);
        var issuer = settings.Issuer;
        var audience = settings.Audience;

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = context =>
                {
                    var userId = int.Parse(context.Principal.Identity.Name);
                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Add("Token-Expired", "true");
                    }

                    return Task.CompletedTask;
                }
            };

            options.RequireHttpsMetadata = true;
            options.SaveToken = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });
    }

    public static void RegisterAPIVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(o =>
        {
            o.DefaultApiVersion = new ApiVersion(1, 0);
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.ReportApiVersions = true;
            //o.ApiVersionReader = new QueryStringApiVersionReader("api-version");
            //o.ApiVersionReader = new HeaderApiVersionReader("x-version");
            o.ApiVersionReader = new UrlSegmentApiVersionReader();

        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true; //For URLSegmentApiVersionReader
        });


    }

    public static void RegisterHealthCheck(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks().AddSqlServer(configuration.GetConnectionString("NorthwindConnection"), tags: new[] { "database" });
        services.AddHealthChecks().AddCheck<HealthChecksCustom>("healthCheckCustom", tags: new[] { "custom" });
        services.AddHealthChecks().AddRedis(configuration.GetConnectionString("RedisConnection"), tags: new[] { "cache" });
        //check more dependencies

        services.AddHealthChecksUI().AddInMemoryStorage();
    }

    public static void RegisterWatchDog(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddWatchDogServices(opt =>
        {
            opt.SetExternalDbConnString = configuration.GetConnectionString("NorthwindConnection");
            opt.DbDriverOption = WatchDog.src.Enums.WatchDogDbDriverEnum.MSSQL;
            opt.IsAutoClear = true;
            opt.ClearTimeSchedule = WatchDog.src.Enums.WatchDogAutoClearScheduleEnum.Monthly;
        });
    }

    public static void RegisterRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("RedisConnection");
        });
    }

    public static void RegisterRateLimiter(this IServiceCollection services, IConfiguration configuration)
    {
        var policy = "fixedWindow";

        services.AddRateLimiter(options => {
            options.AddFixedWindowLimiter(policyName: policy, fixedWindowProperties =>
            {
                fixedWindowProperties.PermitLimit = int.Parse(configuration["RateLimiting:PermitLimit"]);
                fixedWindowProperties.Window = TimeSpan.FromSeconds(int.Parse(configuration["RateLimiting:Window"]));
                fixedWindowProperties.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                fixedWindowProperties.QueueLimit = int.Parse(configuration["RateLimiting:QueueLimit"]);
            });

            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

        });
    }
}
