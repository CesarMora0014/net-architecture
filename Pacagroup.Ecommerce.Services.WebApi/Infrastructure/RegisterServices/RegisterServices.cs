using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pacagroup.Ecommerce.Application.Validator;
using Pacagroup.Ecommerce.Services.WebApi.Helpers;
using Pacagroup.Ecommerce.Transversal.Mapper;
using System.Reflection;
using System.Text;

namespace Pacagroup.Ecommerce.Services.WebApi.Infrastructure.RegisterServices;

public static class RegisterServices
{
    public static void RegisterAutoMapper(this IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.AddProfile<MappingsProfile>();
        });
        IMapper mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);
    }

    public static void RegisterSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Version = "v1",
                Title = "Pacagroup Technologies Services API Market",
                Description = "Simple ASP.NET Core API",
                TermsOfService = new Uri("http://pacagroup.com"),
                Contact = new Microsoft.OpenApi.Models.OpenApiContact
                {
                    Name = "Cesar Mora",
                    Email = "cesarmora014@gmail.com",
                    Url = new Uri("http://pacagroup.com")
                },
                License = new Microsoft.OpenApi.Models.OpenApiLicense
                {
                    Name = "Use under LICS",
                    Url = new Uri("http://pacagroup.com")
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Type = SecuritySchemeType.Http,
                Name = "Authorization",
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement 
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                    },
                    new List<string>()
                }
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

    public static void RegisterValidators(this IServiceCollection services)
    {
        services.AddTransient<UsersDTOValidator>();
    }
}
