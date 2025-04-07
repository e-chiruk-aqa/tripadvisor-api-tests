using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TripadvisorApiFramework;
using TripadvisorApiFramework.DependencyInjection;
using TripadvisorApiTests.Extensions;

namespace TripadvisorApiTests
{
    [Parallelizable(scope: ParallelScope.All)]
    public class BaseTest
    {
        protected ILogger Logger { get; set; }
        protected TripadvisorApiClient TripadvisorApiClient { get; set; }
        private string _logFilePath;

        [SetUp]
        public void SetUp() 
        {
            var testName = TestContext.CurrentContext.Test.MethodName;
            var testId = TestContext.CurrentContext.Test.ID;
            _logFilePath = Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                $"{testName}#{testId}.log"
            );
            var serviceProvider = ServiceProviderFactory
                .ServiceCollection()
                .AddTripadvisorServices(_logFilePath)
                .BuildServiceProvider();

            Logger = serviceProvider.GetRequiredService<ILogger<BaseTest>>();
            TripadvisorApiClient = serviceProvider.GetRequiredService<TripadvisorApiClient>();

            Logger.LogInformation($"Starting test: {testName}");
        }

        [TearDown]
        public void TearDown()
        {
            Logger.LogInformation("Test finished.");

            if (File.Exists(_logFilePath))
            {
                TestContext.AddTestAttachment(_logFilePath);
            }
        }
    }
}
