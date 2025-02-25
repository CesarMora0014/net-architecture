using Asp.Versioning.ApiExplorer;
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

app.Run();
