using PactNet;
using PactNet.Mocks.MockHttpService;
using PactNet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ContractTestingConsumer.Tests
{
    public class ConsumerZipApiPact : IDisposable
    {
        public IPactBuilder PactBuilder { get; }
        public IMockProviderService MockProviderService { get; }

        public int MockServerPort => 9876;
        public string MockProviderServiceBaseUri => $"http://localhost:{MockServerPort}";

        public ConsumerZipApiPact()
        {
            PactBuilder = new PactBuilder(new PactConfig
            {
                SpecificationVersion = "2.0.0",
                LogDir = $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}logs{Path.DirectorySeparatorChar}",
                PactDir = $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}pacts{Path.DirectorySeparatorChar}"
            })
                .ServiceConsumer("Zip API Consumer")
                .HasPactWith("Zip API Provider");

            MockProviderService = PactBuilder.MockService(MockServerPort, false, IPAddress.Any);
        }

        public void Dispose()
        {
            PactBuilder.Build();
        }
    }
}
