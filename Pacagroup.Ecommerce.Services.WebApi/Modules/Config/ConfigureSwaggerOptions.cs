using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pacagroup.Ecommerce.Services.WebApi.Modules.Config;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    readonly IApiVersionDescriptionProvider provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            
        }
    }

    static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Version = description.ApiVersion.ToString(),
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
        };

        if(description.IsDeprecated)
        {
            info.Description += " (Ésta versión está obsoleta)";
        }

        return info;
    }

    
}
