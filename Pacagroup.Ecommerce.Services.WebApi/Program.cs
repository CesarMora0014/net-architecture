using Pacagroup.Ecommerce.Services.WebApi.Helpers;
using Pacagroup.Ecommerce.Services.WebApi.Infrastructure.RegisterServices;

var builder = WebApplication.CreateBuilder(args);
var appSettingSection = builder.Configuration.GetSection("Config");
builder.Services.Configure<AppSettings>(appSettingSection);

var corsPolicy = "policyApiEcommerce";


// Add services to the container.
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

builder.Services.AddControllers();

builder.Services
    .RegisterApplicationInterfaces()
    .RegisterDomainInterfaces()
    .RegisterInfrastructureInterfaces()
    .RegisterCommonInterfaces();

builder.Services.RegisterAutoMapper();
builder.Services.RegisterJwtAuthentication(appSettingSection);
builder.Services.RegisterSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API Ecommerce v1") );

app.UseCors(corsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
