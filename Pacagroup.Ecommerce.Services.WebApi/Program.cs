using Asp.Versioning.ApiExplorer;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using WatchDog;
using Pacagroup.Ecommerce.Application.UseCases;
using Pacagroup.Ecommerce.Persistence;
using Pacagroup.Ecommerce.Services.WebApi.Helpers;
using Pacagroup.Ecommerce.Services.WebApi.Infrastructure.RegisterServices;
using System.Text.Json.Serialization;
using Pacagroup.Ecommerce.Infrastructure;

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

builder.Services.RegisterCommonInterfaces();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.RegisterJwtAuthentication(appSettingSection);
builder.Services.RegisterAPIVersioning();
builder.Services.RegisterSwagger();
builder.Services.RegisterHealthCheck(builder.Configuration);
builder.Services.RegisterWatchDog(builder.Configuration);
builder.Services.RegisterRedis(builder.Configuration);
builder.Services.RegisterRateLimiter(builder.Configuration);



//----------------App build-------------------//

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();

var apiVersionProvider = app.Services.GetService<IApiVersionDescriptionProvider>();
app.UseSwaggerUI(c => {
    foreach(var description in apiVersionProvider.ApiVersionDescriptions)
    {
        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"My API Ecommerce {description.GroupName.ToUpperInvariant()}");
    }
});

app.UseWatchDogExceptionLogger();
app.UseCors("policyApiEcommerce");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseRateLimiter();
app.UseEndpoints(endpoints => { });
app.MapControllers();
app.MapHealthChecksUI();
app.MapHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseWatchDog(opt =>
{
    opt.WatchPageUsername = app.Configuration["WatchDog:WatchDogPageUserName"];
    opt.WatchPagePassword = app.Configuration["WatchDog:WatchDogPagePassword"];
});


app.Run();

public partial class Program { };
