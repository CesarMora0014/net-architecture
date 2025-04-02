using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;

namespace Pacagroup.Ecommerce.Application.UseCases.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly Stopwatch timer;
    private readonly ILogger<TRequest> logger;

    public PerformanceBehaviour(ILogger<TRequest> logger)
    {
        this.timer = new Stopwatch();
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        timer.Start();

        var response = await next();

        timer.Stop();

        var elapsedMiliseconds = timer.ElapsedMilliseconds;

        if(elapsedMiliseconds > 10)
        {
            var requestName = typeof(TRequest).Name;

            logger.LogWarning("Clean architecture Long Running Request: {name} ({elapsedMiliseconds} miliseconds) {@requestName}",
                requestName,elapsedMiliseconds,JsonSerializer.Serialize(request));
        }

        return response;
    }
}
