using TripadvisorApiFramework.Responses;
using Microsoft.Extensions.Logging;
using TripadvisorApiFramework.Helpers.Http;
using TripadvisorApiFramework.Helpers.Http.RequestResolvers;
using RestSharp;
using TripadvisorApiFramework.Requests;

namespace TripadvisorApiFramework
{
    public class TripadvisorApiClient : Helpers.Http.HttpClient
    {
        public TripadvisorApiClient(ILogger<TripadvisorApiClient> logger, int timeout = 30000)
            : base(logger, timeout) { }

        public async Task<RestResponse<GetCruisesLocationsResponse>> GetCruisesLocationsAsync()
        {
            return await SendGetRequestAsync<GetCruisesLocationsResponse>(
                ApiUrl.CruisesApi.GetCruisesLocations,
                new BaseRequest()
            );
        }

        public async Task<RestResponse<CruiseSearchResponse>> SearchCruisesAsync(SearchCruisesRequest request)
        {
            return await SendGetRequestAsync<CruiseSearchResponse>(
                ApiUrl.CruisesApi.SearchCruises,
                request
            );
        }
    }
}
