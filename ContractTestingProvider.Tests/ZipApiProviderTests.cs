using Microsoft.AspNetCore.Mvc.Testing;
using PactNet;
using PactNet.Infrastructure.Outputters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Xunit;
using Xunit.Abstractions;

namespace ContractTestingProvider.Tests
{
    public class ZipApiProviderTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        public HttpClient Client { get; }

        private readonly ITestOutputHelper _output;

        public ZipApiProviderTests(WebApplicationFactory<Startup> fixture, ITestOutputHelper output)
        {
            Client = fixture.CreateClient();
            _output = output;
        }

        [Fact]
        public void EnsureEventApiHonoursPactWithConsumer()
        {
            //Arrange
            const string serviceUri = "http://localhost:9876";

            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                {
                    new XUnitOutput(_output)
                }
            };

            //Act / Assert
            IPactVerifier pactVerifier = new PactVerifier(config);
            pactVerifier
                .ProviderState($"{serviceUri}/provider-states")
                .ServiceProvider("Zip API Provider", serviceUri)
                .HonoursPactWith("Zip API Consumer")
                .PactUri($"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}pacts{Path.DirectorySeparatorChar}zip_api_consumer-zip_api_provider.json")
                .Verify();
        }

        public virtual void Dispose()
        {
        }
    }
}
