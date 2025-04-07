using Newtonsoft.Json;
using TripadvisorApiFramework.DependencyInjection;

namespace TripadvisorApiFramework.Configuration
{
    public static class ConfigManager
    {
        public static ConfigurationData Configuration { get; private set; }

        static ConfigManager()
        {
            try
            {
                Configuration = JsonConvert.DeserializeObject<ConfigurationData>(ConfigurationFactory.GetStringConfiguration());
            }
            catch (Exception ex)
            {
                throw new Exception($"Configuration issue", ex);
            }
        }
    }
}
