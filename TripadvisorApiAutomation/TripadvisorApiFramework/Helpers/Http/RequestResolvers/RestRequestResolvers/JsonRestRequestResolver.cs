using RestSharp;

namespace TripadvisorApiFramework.Helpers.Http.RequestResolvers.RestRequestResolvers
{
    internal class JsonRestRequestResolver : RestRequestResolver
    {
        public JsonRestRequestResolver(HttpRequest request)
            : base(request)
        {
        }

        public override void AssignBody(IRestClient client, RestRequest restRequest)
        {
            string text = GetContentTypeHeader() ?? "application/json";
            string body = _request.GetBody();
            restRequest.RequestFormat = DataFormat.Json;
            if (body != null)
            {
                restRequest.AddStringBody(body, text);
            }
        }
    }
}
