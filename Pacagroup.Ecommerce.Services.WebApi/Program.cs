using Asp.Versioning.ApiExplorer;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Pacagroup.Ecommerce.Application.UseCases;
using Pacagroup.Ecommerce.Persistence;
using Pacagroup.Ecommerce.Services.WebApi.Helpers;
using Pacagroup.Ecommerce.Services.WebApi.Infrastructure.RegisterServices;
using System.Text.Json.Serialization;
using Pacagroup.Ecommerce.Infrastructure;
using Microsoft.AspNetCore.Http.Timeouts;

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

builder.Services.AddControllers().AddJsonOptions(opts =>
{
    var enumConverter = new JsonStringEnumConverter();
    opts.JsonSerializerOptions.Converters.Add(enumConverter);
});

builder.Services.AddRequestTimeouts(options =>
{
    options.DefaultPolicy = new RequestTimeoutPolicy { Timeout = TimeSpan.FromSeconds(1.5) };
    options.AddPolicy("CustomPolicy", TimeSpan.FromSeconds(2));
});

builder.Services.RegisterCommonInterfaces();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.RegisterJwtAuthentication(appSettingSection);
builder.Services.RegisterAPIVersioning();
builder.Services.RegisterSwagger();
builder.Services.RegisterHealthCheck(builder.Configuration);
builder.Services.RegisterRedis(builder.Configuration);
builder.Services.RegisterRateLimiter(builder.Configuration);



//----------------App build-------------------//

var app = builder.Build();

// Configure the HTTP request pipeline.


var apiVersionProvider = app.Services.GetService<IApiVersionDescriptionProvider>();
app.UseSwagger();
app.UseSwaggerUI(c => {
    foreach(var description in apiVersionProvider.ApiVersionDescriptions)
    {
        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"My API Ecommerce {description.GroupName.ToUpperInvariant()}");
    }
});

app.UseReDoc(options =>
{
    foreach (var description in apiVersionProvider.ApiVersionDescriptions)
    {
        options.DocumentTitle = "Pacagroup Technology Services API Market";
        options.SpecUrl = $"/swagger/{description.GroupName}/swagger.json";
    }
});

app.UseCors("policyApiEcommerce");
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
app.UseRequestTimeouts();
app.MapControllers();
app.MapHealthChecksUI();
app.MapHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.RegisterMiddlewares();

app.Run();

public partial class Program { };
