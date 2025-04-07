using Newtonsoft.Json;

namespace TripadvisorApiFramework.Responses
{
    public class CruiseSearchResponse : BaseResponse
    {
        [JsonProperty("data")]
        public CruiseSearchData Data { get; set; }
    }

    public class CruiseSearchData
    {
        [JsonProperty("list")]
        public List<CruiseItem> List { get; set; }
    }

    public class CruiseItem
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("ship")]
        public Ship Ship { get; set; }

        [JsonProperty("destination")]
        public Destination Destination { get; set; }
    }

    public class Ship
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("crewCount")]
        public int CrewCount { get; set; }
    }

    public class Destination
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
