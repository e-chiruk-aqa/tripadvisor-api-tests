using System.Reflection;
using TripadvisorApiFramework.Helpers.Http.Attributes;

namespace TripadvisorApiFramework.Helpers.Http.RequestResolvers.RequestParameters
{
    internal abstract class RequestParametersResolver : IRequestParametersResolver
    {
        protected HttpRequest _request;

        public RequestParametersResolver(HttpRequest request)
        {
            _request = request;
        }

        public abstract string RequestBodyToString();

        public Dictionary<string, string> GetUrlParameters()
        {
            return HttpRequestItemsToDictionary<UrlParameterAttribute>();
        }

        public Dictionary<string, string> GetCookies()
        {
            return HttpRequestItemsToDictionary<CookieAttribute>();
        }

        public Dictionary<string, string> GetHeaders()
        {
            return HttpRequestItemsToDictionary<HeaderAttribute>();
        }

        protected Dictionary<string, string> HttpRequestItemsToDictionary<TReqItem>() where TReqItem : HttpRequestItemAttribute
        {
            return (from prop in GetPropsValidatedAgainstNullCase()
                    where AttributeHelper.AttributeIsApplied<TReqItem>(prop)
                    select prop).ToDictionary((PropertyInfo prop) => GetStringNameOfProp(prop), (PropertyInfo prop) => GetStringValueOfProp(prop));
        }

        protected IEnumerable<PropertyInfo> GetPropsValidatedAgainstNullCase()
        {
            return from prop in _request.GetType().GetProperties()
                   where AttributeHelper.GetAttribute<HttpRequestItemAttribute>(prop) != null
                   where !AttributeHelper.GetAttribute<HttpRequestItemAttribute>(prop).IgnoreNullValue || prop.GetValue(_request) != null
                   select prop;
        }

        protected string GetStringValueOfProp(PropertyInfo prop)
        {
            if (prop.PropertyType == typeof(DateTime))
            {
                return ((DateTime)prop.GetValue(_request)).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'");
            }

            return prop.GetValue(_request)?.ToString();
        }

        protected string GetStringNameOfProp(PropertyInfo prop)
        {
            return AttributeHelper.GetAttribute<HttpRequestItemAttribute>(prop).Name ?? prop.Name;
        }
    }
}
