using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Linq;
using System.Net;

namespace APITestingWithRestSharp.Exercises
{
    [TestFixture]
    public class Exercises02
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

        /***********************************************
         * EXERCISE 2:
         * The same as in Exercise 1, but now use TestCaseSource
         * and a private method that supplies the test data.
	     **********************************************/

        [Test]
        public void GetDataForUsZipCode90210_CheckStateForFirstPlace_ShouldBeCalifornia()
        {
            RestRequest request = new RestRequest("/us/90210", Method.GET);

            IRestResponse response = client.Execute(request);

            var responseData = JObject.Parse(response.Content);

            Assert.That(responseData.SelectToken("places[0].state").ToString(), Is.EqualTo("California"));
        }

        [Test]
        public void GetDataForCaZipCodeY1A_CheckStateForFirstPlace_ShouldBeYukon()
        {
            RestRequest request = new RestRequest("/ca/Y1A", Method.GET);

            IRestResponse response = client.Execute(request);

            var responseData = JObject.Parse(response.Content);

            Assert.That(responseData.SelectToken("places[0].state").ToString(), Is.EqualTo("Yukon"));
        }

        [Test]
        public void GetDataForItZipCode50123_CheckStateForFirstPlace_ShouldBeToscana()
        {
            RestRequest request = new RestRequest("/it/50123", Method.GET);

            IRestResponse response = client.Execute(request);

            var responseData = JObject.Parse(response.Content);

            Assert.That(responseData.SelectToken("places[0].state").ToString(), Is.EqualTo("Toscana"));
        }
    }
}
