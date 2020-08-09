using Bard.Configuration;
using Bard.Sample.Api;
using Bard.Tests.Scenario;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Xunit.Abstractions;

namespace Bard.Tests
{
    public abstract class BankingTestBase
    {
        protected BankingTestBase(ITestOutputHelper output)
        {
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(builder =>
                    builder
                        .UseStartup<Startup>()
                        .UseTestServer()
                        .UseEnvironment("development"));

            var host = hostBuilder.Start();

            var httpClient = host.GetTestClient();

            var scenario = ScenarioConfiguration
                .WithStoryBook<BankingStory, BankingStoryData>()
                .Configure(options =>
                {
                    options.Client = httpClient;
                    options.LogMessage = output.WriteLine;
                    options.Services = host.Services;
                });

            Given = scenario.Given;
            When = scenario.When;
            Then = scenario.Then;
        }

        protected BankingStory Given { get; set; }

        protected IThen Then { get; }

        protected IWhen When { get; }
    }
}