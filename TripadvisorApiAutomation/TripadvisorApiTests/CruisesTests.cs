using Microsoft.Extensions.Logging;
using System.Net;
using TripadvisorApiFramework.Requests;

namespace TripadvisorApiTests
{
    public class CruisesTests : BaseTest
    {

        [TestCase("Antarctica")]
        public async Task PrintCruisesSortedByCrewCount(string destinationName)
        {
            var cruisesLocations = await TripadvisorApiClient.GetCruisesLocationsAsync();

            Assert.That(cruisesLocations.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(cruisesLocations.Data?.Status, Is.True);

            var destination = cruisesLocations.Data?.Data.FirstOrDefault(x =>
            x.Name.Equals(destinationName, StringComparison.OrdinalIgnoreCase));

            Assert.IsNotNull(destination, $"Destination '{destinationName}' not found");

            Logger.LogInformation($"Found destinationId for '{destinationName}': {destination.DestinationId}");

            var cruisesResponse = await TripadvisorApiClient.SearchCruisesAsync(new SearchCruisesRequest
            {
                DestinationId = destination.DestinationId.ToString(),
                Order = "popularity"
            });

            Assert.That(cruisesResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(cruisesResponse.Data?.Status, Is.True);

            var sorted = cruisesResponse.Data?.Data.List
                .Where(c => c.Ship?.Crew > 0)
                .OrderByDescending(c => c.Ship.Crew)
                .ToList();

            Assert.That(sorted, Is.Not.Null);
            Assert.That(sorted, Is.Not.Empty, $"No cruises found for '{destinationName}'");

            Logger.LogInformation($"===== {destinationName} Cruises Sorted by Crew Count =====");
            foreach (var cruise in sorted)
            {
                Logger.LogInformation($"{cruise.Ship?.Name} - Crew: {cruise.Ship?.Crew}");
            } 
        }
    }
}