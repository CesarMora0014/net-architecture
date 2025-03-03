using Asp.Versioning.ApiExplorer;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Pacagroup.Ecommerce.Services.WebApi;

var app = AppBuilder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();

var apiVersionProvider = app.Services.GetService<IApiVersionDescriptionProvider>();
app.UseSwaggerUI(c => {
    foreach(var description in apiVersionProvider.ApiVersionDescriptions)
    {
        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"My API Ecommerce {description.GroupName.ToUpperInvariant()}");
    }
});

app.UseCors("policyApiEcommerce");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecksUI();
app.MapHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

public partial class Program { };
