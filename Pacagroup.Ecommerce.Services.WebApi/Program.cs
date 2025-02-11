
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Application.Main;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Domain.Core;
using Pacagroup.Ecommerce.Infraestructure.Interface;
using Pacagroup.Ecommerce.Infraestructure.Repository;
using Pacagroup.Ecommerce.Infraestructure.Data;
using Pacagroup.Ecommerce.Transversal.Common;
using Pacagroup.Ecommerce.Transversal.Mapper;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using Pacagroup.Ecommerce.Services.WebApi.Helpers;

var builder = WebApplication.CreateBuilder(args);
var corsPolicy = "policyApiEcommerce";
var appSettingSection = builder.Configuration.GetSection("Config");
// Add services to the container.

builder.Services.AddAutoMapper(x => x.AddProfile(new MappingsProfile()));

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy,policyBuilder =>
    {
        policyBuilder
        .WithOrigins(builder.Configuration["Config:OriginCors"]!)
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

builder.Services.Configure<AppSettings>(appSettingSection);

builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>();

builder.Services.AddScoped<ICustumerApplication, CustomerApplication>();
builder.Services.AddScoped<ICustomersDomain, CustomersDomain>();
builder.Services.AddScoped<ICustomersRepository, CustomerRepository>();
builder.Services.AddScoped<IUserApplication, UserApplication>();
builder.Services.AddScoped<IUserDomain, UserDomain>();
builder.Services.AddScoped<IUserRepository,UserRepository>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
        Version = "v1",
        Title = "Pacagroup Technologies Services API Market",
        Description = "Simple ASP.NET Core API",
        TermsOfService = new Uri("http://pacagroup.com"),
        Contact =  new Microsoft.OpenApi.Models.OpenApiContact
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
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API Ecommerce v1") );

app.UseCors(corsPolicy);
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
