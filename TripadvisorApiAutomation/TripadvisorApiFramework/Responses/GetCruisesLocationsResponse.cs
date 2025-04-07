using Newtonsoft.Json;

namespace TripadvisorApiFramework.Responses
{
    public class GetCruisesLocationsResponse : BaseResponse
    {
        [JsonProperty("data")]
        public List<Item> Data { get; set; }

    }

    public class Item
    {
        [JsonProperty("destinationId")]
        public int DestinationId { get; set; }

        [JsonProperty("locationId")]
        public int LocationId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
