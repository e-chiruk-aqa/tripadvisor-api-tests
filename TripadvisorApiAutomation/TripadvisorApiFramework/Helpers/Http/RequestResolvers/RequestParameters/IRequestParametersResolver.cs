namespace TripadvisorApiFramework.Helpers.Http.RequestResolvers.RequestParameters
{
    public interface IRequestParametersResolver
    {
        string RequestBodyToString();

        Dictionary<string, string> GetCookies();

        Dictionary<string, string> GetHeaders();

        Dictionary<string, string> GetUrlParameters();
    }
}
