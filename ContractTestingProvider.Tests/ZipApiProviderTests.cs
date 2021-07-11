using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using PactNet;
using PactNet.Infrastructure.Outputters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ContractTestingProvider.Tests
{
    public class ZipApiProviderTests
    {
        private readonly ITestOutputHelper _output;

        private string _providerUri { get; }
        private string _pactServiceUri { get; }
        private IWebHost _webHost { get; }

        public ZipApiProviderTests(ITestOutputHelper output)
        {
            _output = output;
            _providerUri = "http://localhost:9876";
            _pactServiceUri = "http://localhost:9001";

            _webHost = WebHost.CreateDefaultBuilder()
                .UseUrls(_pactServiceUri)
                .UseStartup<TestStartup>()
                .Build();

            _webHost.Start();
        }

        [Fact]
        public void VerifyZipApiHonoursExpectationsInConsumerContract()
        {
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
                .ProviderState($"{_pactServiceUri}/provider-states")
                .ServiceProvider("Zip API Provider", _providerUri)
                .HonoursPactWith("Zip API Consumer")
                .PactUri($"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}pacts{Path.DirectorySeparatorChar}zip_api_consumer-zip_api_provider.json")
                .Verify();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _webHost.StopAsync().GetAwaiter().GetResult();
                    _webHost.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
