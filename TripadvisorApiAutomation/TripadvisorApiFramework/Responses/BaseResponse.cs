using Newtonsoft.Json;

namespace TripadvisorApiFramework.Responses
{
    public class BaseResponse
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("timestamp")]
        public decimal Timestamp { get; set; }
    }
}
