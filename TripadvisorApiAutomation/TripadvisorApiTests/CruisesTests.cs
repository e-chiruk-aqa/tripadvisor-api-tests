using Microsoft.Extensions.Logging;
using System.Net;
using TripadvisorApiFramework.Requests;
using TripadvisorApiFramework.Responses;

namespace TripadvisorApiTests
{
    public class CruisesTests : BaseTest
    {

        [TestCase("Caribbean")]
        public async Task PrintCruisesSortedByCrewCount(string destinationName)
        {
            var cruisesLocations = await TripadvisorApiClient.GetCruisesLocationsAsync();

            Assert.That(cruisesLocations.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(cruisesLocations.Data?.Status, Is.True);

            var destination = cruisesLocations.Data?.Data.FirstOrDefault(x =>
            x.Name.Equals(destinationName, StringComparison.OrdinalIgnoreCase));

            Assert.IsNotNull(destination, $"Destination '{destinationName}' not found");

            Logger.LogInformation($"Found destinationId for '{destinationName}': {destination.DestinationId}");

            var allCruises = new List<CruiseItem>();
            int currentPage = 1;
            int totalPages = 1;

            do
            {
                var request = new SearchCruisesRequest
                {
                    DestinationId = destination.DestinationId.ToString(),
                    Page = currentPage.ToString(),
                    Order = "popularity"
                };

                var response = await TripadvisorApiClient.SearchCruisesAsync(request);

                Assert.That(response.Data?.Status, Is.True);
                Assert.That(response.Data?.Data.List, Is.Not.Null);

                if (currentPage == 1 && response.Data.Data.TotalPages > 1)
                {
                    totalPages = response.Data.Data.TotalPages;
                }

                allCruises.AddRange(response.Data.Data.List);
                currentPage++;

            } while (currentPage <= totalPages);

            var sorted = allCruises
                .OrderByDescending(c => c.Ship.Crew)
                .Select(c => $"{c.Title} — Crew: {c.Ship.Crew}");

            Assert.That(sorted, Is.Not.Null);
            Assert.That(sorted, Is.Not.Empty);

            Logger.LogInformation($"\nTotal Cruises for '{destinationName}': {allCruises.Count}");
            foreach (var cruise in sorted)
            {
                Logger.LogInformation(cruise);
            }
        }
    }
}