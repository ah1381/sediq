using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Raya.Hrm.Shared.Library.Models.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.Utilities.Securities
{
    public class LogDirectoryHealthCheck(IOptions<ErrorConfig> errorConfig) : IHealthCheck
    {
        private readonly string _logDirectory = errorConfig.Value.LogDirectory;

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (Directory.Exists(_logDirectory))
            {
                return Task.FromResult(HealthCheckResult.Healthy("Log directory exists"));
            }
            return Task.FromResult(HealthCheckResult.Unhealthy("Log directory is missing"));
        }
    }
}
