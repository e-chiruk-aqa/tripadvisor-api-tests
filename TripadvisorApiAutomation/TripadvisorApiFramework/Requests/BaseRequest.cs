using System.Text.Json.Serialization;
using TripadvisorApiFramework.Configuration;
using TripadvisorApiFramework.Helpers.Http;
using TripadvisorApiFramework.Helpers.Http.Attributes;

namespace TripadvisorApiFramework.Requests
{
    public class BaseRequest : HttpRequest
    {
        [Header(Name = "x-rapidapi-key")]
        [JsonIgnore]
        public string ApiKey { get; set; } = ConfigManager.Configuration.TripadvisorApiOptions.DefaultRequestHeaders.ApiKey;

        [Header(Name = "x-rapidapi-host")]
        [JsonIgnore]
        public string ApiHost { get; set; } = ConfigManager.Configuration.TripadvisorApiOptions.DefaultRequestHeaders.ApiHost;
    }
}
