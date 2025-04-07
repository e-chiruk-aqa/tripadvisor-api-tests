using Newtonsoft.Json;
using RestSharp;
using TripadvisorApiFramework.Helpers.Http.RequestResolvers;
using Microsoft.Extensions.Logging;

namespace TripadvisorApiFramework.Helpers.Http
{
    public class HttpClient
    {
        private readonly RestClient _client;
        private readonly ILogger _logger;

        public HttpClient(ILogger logger, int timeout = 30000)
        {
            var options = new RestClientOptions
            {
                Timeout = TimeSpan.FromMilliseconds(timeout)
            };
            _client = new RestClient(options);
            _logger = logger;
        }

        public virtual Task<RestResponse<T>> SendGetRequestAsync<T>(string url, HttpRequest requestModel) =>
            SendRequestAsync<T>(url, Method.Get, requestModel);

        public virtual Task<RestResponse<T>> SendPostRequestAsync<T>(string url, HttpRequest requestModel) =>
            SendRequestAsync<T>(url, Method.Post, requestModel);

        public virtual Task<RestResponse<T>> SendPutRequestAsync<T>(string url, HttpRequest requestModel) =>
            SendRequestAsync<T>(url, Method.Put, requestModel);

        public virtual Task<RestResponse<T>> SendDeleteRequestAsync<T>(string url, HttpRequest requestModel) =>
            SendRequestAsync<T>(url, Method.Delete, requestModel);

        public virtual Task<RestResponse<T>> SendPatchRequestAsync<T>(string url, HttpRequest requestModel) =>
            SendRequestAsync<T>(url, Method.Patch, requestModel);

        public virtual async Task<RestResponse<T>> SendRequestAsync<T>(string url, Method method, HttpRequest requestModel)
        {
            var restRequest = new RestRequest(url, method);
            var resolver = RequestResolver.GetRestRequestResolver(requestModel);
            resolver.AssignUrlParameters(restRequest);
            resolver.AssignHeaders(restRequest);
            resolver.AssignCookies(restRequest);
            resolver.AssignBody(_client, restRequest);

            _client.BuildUri(restRequest);

            _logger.LogInformation("[" + DateTime.UtcNow + "] Sending request:");
            _logger.LogInformation($"{method} {url}");
            _logger.LogInformation(requestModel.ToString());

            RestResponse<T> response = await _client.ExecuteAsync<T>(restRequest);

            _logger.LogInformation("[" + DateTime.UtcNow + "] Response:");
            _logger.LogInformation($"{response.StatusCode}");

            if (response.Content != null)
                _logger.LogInformation("Body:\n" + response.Content);

            if (response.ErrorMessage != null)
                _logger.LogWarning("Error:\n" + response.ErrorMessage);

            return HandleResponse(response);
        }

        private RestResponse<T> HandleResponse<T>(RestResponse<T> response)
        {
            if (response.ContentType == null)
            {
                response.Data = default;
                return response;
            }

            try
            {
                response.Data = JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (JsonSerializationException)
            {
                response.Data = default;
            }

            return response;
        }
    }
}
