using TripadvisorApiFramework.Configuration;

namespace TripadvisorApiFramework
{
    public static class ApiUrl
    {
        public static class CruisesApi
        {
            public static string BaseUrl => $"{ConfigManager.Configuration.TripadvisorApiOptions.BaseUrl}/cruises";
            public static string GetCruisesLocations => $"{BaseUrl}/getLocation";
            public static string SearchCruises => $"{BaseUrl}/searchCruises";
        }
    }
}
