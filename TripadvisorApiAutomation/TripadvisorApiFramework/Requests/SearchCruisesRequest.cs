using TripadvisorApiFramework.Helpers.Http.Attributes;

namespace TripadvisorApiFramework.Requests
{
    public class SearchCruisesRequest : BaseRequest
    {
        [UrlParameter(Name = "destinationId")]
        public string DestinationId { get; set; }

        [UrlParameter(Name = "order")]
        public string Order { get; set; }

        [UrlParameter(Name = "page")]
        public string Page { get; set; }

        [UrlParameter(Name = "currencyCode")]
        public string CurrencyCode { get; set; }
    }
}
