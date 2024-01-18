using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WhiteBear.Infrastructure.HealthChecks
{
    internal class TestHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            await Task.CompletedTask;

            // it could be anything
            var tmp = new Dictionary<string, object>()
            {
                {"Check step 1", new {Id=0, Message = "Some result" } },
                {"Check step 2", new {Message = "some other data" } },
            };

            return HealthCheckResult.Healthy("Healthy - you can add here any message", tmp);
        }
    }
}
