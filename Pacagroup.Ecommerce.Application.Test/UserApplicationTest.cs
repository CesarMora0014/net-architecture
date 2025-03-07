using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Application.Interface.UseCases;

namespace Pacagroup.Ecommerce.Application.Test;

[TestClass]
public class UserApplicationTest
{
    public static IServiceScopeFactory scopeFactory = null;
    public static WebApplicationFactory<Program> webApplicationFactory = null;
    
    [ClassInitialize]
    public static void Initialize(TestContext _)
    {
        webApplicationFactory = new CustomWebApplicationFactory();
        scopeFactory = webApplicationFactory.Services.GetService<IServiceScopeFactory>();
    }

    [TestMethod]
    public void Authenticate_NotParamsSended_ShouldReturnValidationErrorMessage()
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetService<IUserApplication>();

        string username = string.Empty;
        string password = string.Empty;

        var expected = "Errores de validación";

        var result = context.Authenticate(username, password);
        var actual = result.Message;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Authenticate_CorrectParamsSended_ShouldReturnSuccessMessage()
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetService<IUserApplication>();

        string username = "cesarmora";
        string password = "123456";

        var expected = "Autenticación exitosa.";

        var result = context.Authenticate(username, password);
        var actual = result.Message;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Authenticate_IncorrectParamsSended_ShouldReturnUserNotExistsMessage()
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetService<IUserApplication>();

        string username = "cesarssssssmora";
        string password = "123456";

        var expected = "Usuario no existe.";

        var result = context.Authenticate(username, password);
        var actual = result.Message;

        Assert.AreEqual(expected, actual);
    }
}