using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace APITestingWithRestSharp.Answers
{
    [TestFixture]
    public class Answers02
    {
        private RestClient client;

        private const string BASE_URL = "http://api.zippopotam.us";

        [OneTimeSetUp]
        public void SetupRestSharpClient()
        {
            client = new RestClient(BASE_URL);
        }

        /***********************************************
         * EXERCISE 1:
         * Refactor the tests below into a data-driven
         * test using the TestCase attribute.
         * 
         * Think about what the variable input and output
         * values are, create path parameters and replace
         * hard-coded values with parameterized ones.
	     **********************************************/

        [TestCase("us", "90210", "California", TestName = "US zip code 90210 is in California")]
        [TestCase("ca", "Y1A", "Yukon", TestName = "CA zip code Y1A is in Yukon")]
        [TestCase("it", "50123", "Toscana", TestName = "IT zip code 50123 is in Toscana")]
        public void GetDataForZipCode_CheckStateForFirstPlace_UsingTestCase(string countryCode, string zipCode, string expectedState)
        {
            RestRequest request = (RestRequest) new RestRequest("/{countryCode}/{zipCode}", Method.GET)
                .AddUrlSegment("countryCode", countryCode)
                .AddUrlSegment("zipCode", zipCode);

            IRestResponse response = client.Execute(request);

            var responseData = JObject.Parse(response.Content);

            Assert.That(responseData.SelectToken("places[0].state").ToString(), Is.EqualTo(expectedState));
        }

        /***********************************************
         * EXERCISE 2:
         * The same as in Exercise 1, but now use TestCaseSource
         * and a private method that supplies the test data.
	     **********************************************/

        [Test, TestCaseSource(nameof(ZipCodeTestData))]
        public void GetDataForZipCode_CheckStateForFirstPlace_UsingTestCaseSource(string countryCode, string zipCode, string expectedState)
        {
            RestRequest request = (RestRequest)new RestRequest("/{countryCode}/{zipCode}", Method.GET)
                .AddUrlSegment("countryCode", countryCode)
                .AddUrlSegment("zipCode", zipCode);

            IRestResponse response = client.Execute(request);

            var responseData = JObject.Parse(response.Content);

            Assert.That(responseData.SelectToken("places[0].state").ToString(), Is.EqualTo(expectedState));
        }

        private static IEnumerable<TestCaseData> ZipCodeTestData()
        {
            yield return new TestCaseData("us", "90210", "California").
                SetName("US zip code 90210 is in California - using TestCaseSource");
            yield return new TestCaseData("ca", "Y1A", "Yukon").
                SetName("CA zip code Y1A is in Yukon - using TestCaseSource");
            yield return new TestCaseData("it", "50123", "Toscana").
                SetName("IT zip code 50123 is in Toscana - using TestCaseSource");
        }
    }
}
