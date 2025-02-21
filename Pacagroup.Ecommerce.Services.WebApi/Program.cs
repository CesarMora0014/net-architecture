using Pacagroup.Ecommerce.Services.WebApi;

var app = AppBuilder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API Ecommerce v1") );

app.UseCors("policyApiEcommerce");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
