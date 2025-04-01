
using Pacagroup.Ecommerce.Transversal.Common;
using System.Net;
using System.Text.Json;

namespace Pacagroup.Ecommerce.Services.WebApi.Modules.GlobalException;

public class GlobalExceptionHandler : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandler> logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            string message = ex.Message.ToString();
            logger.LogError(message, ex);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            var response = new Response<Object>();

            response.Message = "Ocurrió un error inesperado";

            await JsonSerializer.SerializeAsync(context.Response.Body, response);
        }
    }
}
