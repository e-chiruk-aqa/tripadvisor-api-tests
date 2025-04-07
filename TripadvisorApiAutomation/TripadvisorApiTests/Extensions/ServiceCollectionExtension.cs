using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using TripadvisorApiFramework;

namespace TripadvisorApiTests.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddTripadvisorServices(this IServiceCollection services, string logFilePath)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Infinite)
                .CreateLogger();

            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddSerilog();
            });
            services.AddTransient<TripadvisorApiClient>();

            return services;
        }
    }
}
