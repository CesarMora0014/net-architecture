using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Pacagroup.Ecommerce.Application.UseCases.Common.Behaviours;

public class LogginBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{

    private ILogger <LogginBehaviour<TRequest, TResponse>> logger;

    public LogginBehaviour(ILogger<LogginBehaviour<TRequest, TResponse>> logger)
    {
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("Clean arch rquest handling: {name} {@request}", typeof(TRequest).Name, JsonSerializer.Serialize(request));
        
        var response = await next();

        logger.LogInformation("Clean arch rquest handling: {name} {@response}", typeof(TRequest).Name, JsonSerializer.Serialize(response));

        return response;
    }
}
