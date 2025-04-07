using RestSharp;

namespace TripadvisorApiFramework.Helpers.Http.RequestResolvers.RestRequestResolvers
{
    internal abstract class RestRequestResolver : IRestRequestResolver
    {
        protected HttpRequest _request;

        public RestRequestResolver(HttpRequest request)
        {
            _request = request;
        }

        public abstract void AssignBody(IRestClient client, RestRequest restRequest);

        public void AssignCookies(RestRequest restRequest)
        {
            foreach (var cookie in _request.GetCookies())
            {
                restRequest.AddCookie(cookie.Key, cookie.Value, null, null);
            }
        }

        public void AssignHeaders(RestRequest restRequest)
        {
            foreach (var header in _request.GetHeaders())
            {
                restRequest.AddHeader(header.Key, header.Value);
            }
        }

        public void AssignUrlParameters(RestRequest restRequest)
        {
            foreach (var parameter in _request.GetUrlParameters())
            {
                restRequest.AddQueryParameter(parameter.Key, parameter.Value);
            }
        }

        protected string GetContentTypeHeader() => _request
            .GetHeaders()
            .FirstOrDefault(header => header.Key.ToLower() == "content-type")
            .Value;
    }
}
