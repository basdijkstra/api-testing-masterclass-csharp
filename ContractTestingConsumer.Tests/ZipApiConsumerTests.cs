using PactNet.Matchers;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ContractTestingConsumer.Tests
{
    public class ZipApiConsumerTests : IClassFixture<ConsumerZipApiPact>
    {
        private readonly IMockProviderService _mockProviderService;
        private readonly string _mockProviderServiceBaseUri;

        private readonly string placeName = "Beverly Hills";
        private readonly string state = "California";
        private readonly string country = "United States";
        private readonly string countryAbbreviation = "US";
        private readonly bool active = true;

        public ZipApiConsumerTests(ConsumerZipApiPact data)
        {
            _mockProviderService = data.MockProviderService;
            _mockProviderServiceBaseUri = data.MockProviderServiceBaseUri;
            _mockProviderService.ClearInteractions();
        }

        [Fact]
        public async Task GetLocationByCountryAndZipCode_WhenTheLocationExists_ReturnsLocation()
        {
            //Arrange
            var countryCodeRegex = "[a-zA-Z]{2}";
            var zipCodeRegex = "[0-9]{5}";
            var countryCode = "us";
            var zipCode = "90210";
            _mockProviderService.Given(string.Format("there is location data for {0} zip code {1}", countryCode, zipCode))
                .UponReceiving(string.Format("a request to retrieve location data for {0} zip code {1}", countryCode, zipCode))
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = Match.Regex($"/zip/{countryCode}/{zipCode}", $"^\\/zip\\/{countryCodeRegex}\\/{zipCodeRegex}$"),
                    Headers = new Dictionary<string, object>
                    {
                        { "Accept", "application/json" }
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = new
                    {
                        placeName = Match.Type(placeName),
                        state = Match.Type(state),
                        country = Match.Type(country),
                        countryAbbreviation = Match.Type(countryAbbreviation),
                        active = Match.Type(active)
                    }
                });

            var consumer = new ZipApiClient(_mockProviderServiceBaseUri);

            //Act
            var result = await consumer.GetLocationByCountryAndZipCode(countryCode, zipCode);

            //Assert
            Assert.Equal(placeName, result.PlaceName);
            Assert.Equal(state, result.State);
            Assert.Equal(country, result.Country);
            Assert.Equal(countryAbbreviation, result.CountryAbbreviation);
            Assert.Equal(active, result.Active);

            _mockProviderService.VerifyInteractions();
        }

        [Fact]
        public async Task GetLocationByCountryAndZipCode_WhenTheLocationDoesNotExist_ReturnsHttp404()
        {
            //Arrange
            var countryCode = "us";
            var zipCode = "99999";
            _mockProviderService.Given(string.Format("there is no location data for {0} zip code {1}", countryCode, zipCode))
                .UponReceiving(string.Format("a request to retrieve location data for {0} zip code {1}", countryCode, zipCode))
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = Match.Type($"/zip/{countryCode}/{zipCode}"),
                    Headers = new Dictionary<string, object>
                    {
                        { "Accept", "application/json" }
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 404
                });

            var consumer = new ZipApiClient(_mockProviderServiceBaseUri);

            //Act //Assert
            await Assert.ThrowsAnyAsync<Exception>(() => consumer.GetLocationByCountryAndZipCode(countryCode, zipCode));

            _mockProviderService.VerifyInteractions();
        }
    }
}
