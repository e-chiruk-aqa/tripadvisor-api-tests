using RestSharp;

namespace TripadvisorApiFramework.Helpers.Http.RequestResolvers.RestRequestResolvers
{
    public interface IRestRequestResolver
    {
        void AssignUrlParameters(RestRequest restRequest);

        void AssignHeaders(RestRequest restRequest);

        void AssignCookies(RestRequest restRequest);

        void AssignBody(IRestClient client, RestRequest restRequest);
    }
}
