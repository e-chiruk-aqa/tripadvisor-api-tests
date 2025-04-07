using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace TripadvisorApiFramework.DependencyInjection
{
    public static class ConfigurationFactory
    {
        public static IConfiguration GetConfiguration()
        {
            var stream = GetStreamConfiguration();
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonStream(stream);
            return configBuilder.Build();
        }

        public static string GetStringConfiguration()
        {
            string buildConfiguration;
#if DEBUG
            buildConfiguration = "Debug";
#else
            buildConfiguration = "Release";
#endif
            var appSettingsString = File.ReadAllText($"appsettings.{buildConfiguration}.json");
            var appSettings = JsonConvert.DeserializeObject<Dictionary<string, string>>(appSettingsString);

            var configurationFile = "config.json";

            var testSettings = File.ReadAllText(configurationFile);

            foreach (var setting in appSettings)
            {
                testSettings = testSettings.Replace("((" + setting.Key + "))", setting.Value);
            }

            return testSettings;
        }

        private static Stream GetStreamConfiguration()
        {
            var json = GetStringConfiguration();
            return new MemoryStream(Encoding.ASCII.GetBytes(json));
        }
    }
}
