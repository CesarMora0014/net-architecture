using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Pacagroup.Ecommerce.Services.WebApi.Modules.Config;

public class HealthChecksCustom : IHealthCheck
{
    private Random random = new();

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var responseTime = random.Next(1,300);

        if (responseTime > 100)
            return Task.FromResult(HealthCheckResult.Healthy("Healthy result from HealthChecksCustom"));

        if(responseTime < 200)
            return Task.FromResult(HealthCheckResult.Degraded("Degraded result from HealthChecksCustom"));

        return Task.FromResult(HealthCheckResult.Unhealthy("Unhealthy result from HealthChecksCustom"));
    }
}
