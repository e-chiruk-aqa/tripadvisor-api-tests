using TripadvisorApiFramework.Enums;
using TripadvisorApiFramework.Helpers.Http.RequestResolvers.RequestParameters;
using TripadvisorApiFramework.Helpers.Http.RequestResolvers.RestRequestResolvers;

namespace TripadvisorApiFramework.Helpers.Http.RequestResolvers
{
    internal static class RequestResolver
    {
        internal static IRestRequestResolver GetRestRequestResolver(HttpRequest request)
        {
            return request.BodyContentType switch
            {
                ContentType.Json => new JsonRestRequestResolver(request),
                _ => throw new NotImplementedException($"{"BodyContentType"} was not recognized: {request.BodyContentType}"),
            };
        }

        internal static IRequestParametersResolver GetRequestParametersResolver(HttpRequest request)
        {
            return request.BodyContentType switch
            {
                ContentType.Json => new JsonRequestParametersResolver(request),
                _ => throw new NotImplementedException($"{"BodyContentType"} was not recognized: {request.BodyContentType}"),
            };
        }
    }
}
