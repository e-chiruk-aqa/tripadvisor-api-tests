using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripadvisorApiFramework.Configuration;

namespace TripadvisorApiFramework.DependencyInjection
{
    public static class ServiceProviderFactory
    {
        private static readonly IServiceCollection _serviceCollection = InitializeServices();

        public static IServiceCollection ServiceCollection()
        {
            return _serviceCollection;
        }

        private static IServiceCollection InitializeServices()
        {
            var services = new ServiceCollection();
            var configuration = ConfigurationFactory.GetConfiguration();

            services.Configure<BaseConfigurations>(configuration.GetSection(nameof(BaseConfigurations)).Bind);

            return services;
        }
    }
}
