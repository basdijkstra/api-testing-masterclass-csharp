using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Linq;
using System.Net;

namespace APITestingWithRestSharp.Answers
{
    [TestFixture]
    public class Answers01
    {
        private RestClient client;

        private const string BASE_URL = "http://api.zippopotam.us";

        [OneTimeSetUp]
        public void SetupRestSharpClient()
        {
            client = new RestClient(BASE_URL);
        }

        /******************************************************
         * Send a GET request to /us/90210
         * and check that the response has HTTP status code OK
         * (use the HttpStatusCode enum)
	     ******************************************************/
        [Test]
        public void GetDataForUsZipCode90210_CheckStatusCode_ShouldBeHttpOK()
        {
            RestRequest request = new RestRequest("/us/90210", Method.GET);

            IRestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        /******************************************************
         * Send a GET request to /us/99999
         * and check that the response has HTTP status code 404
         * (cast the status code to an int)
	     ******************************************************/
        [Test]
        public void GetDataForUsZipCode99999_CheckStatusCode_ShouldBeHttp404()
        {
            RestRequest request = new RestRequest("/us/99999", Method.GET);

            IRestResponse response = client.Execute(request);

            Assert.That((int)response.StatusCode, Is.EqualTo(404));
        }

        /*******************************************************
         * Send a GET request to /us/90210
         * and check that the response is in JSON format 
         ******************************************************/
        [Test]
        public void GetDataForUsZipCode90210_CheckContentType_ShouldBeApplicationJson()
        {
            RestRequest request = new RestRequest("/us/90210", Method.GET);

            IRestResponse response = client.Execute(request);

            Assert.That(response.ContentType, Does.Contain("application/json"));
        }

        /*******************************************************
         * Send a GET request to /us/90210
         * and check that the response has a header 'CF-Cache-Status'
         * (case sensitive!) with value 'DYNAMIC'
         ******************************************************/
        [Test]
        public void GetDataForUsZipCode90210_CheckCfCacheStatus_ShouldBeDynamic()
        {
            RestRequest request = new RestRequest("/us/90210", Method.GET);

            IRestResponse response = client.Execute(request);

            string cacheStatusHeaderValue = response.Headers
                .Where(x => x.Name.Equals("CF-Cache-Status"))
                .Select(x => x.Value.ToString())
                .FirstOrDefault();

            Assert.That(cacheStatusHeaderValue, Is.EqualTo("DYNAMIC"));
        }

        /***********************************************
         * Send a GET request to /us/90210 and check
         * that the value of the element 'country' is
         * equal to 'United States'
         * 
         * Parse the response content to a JObject and
         * use the GetValue() method to retrieve the
         * element value
	     **********************************************/
        [Test]
        public void GetDataForUsZipCode90210_CheckCountry_ShouldBeUnitedStates()
        {
            RestRequest request = new RestRequest("/us/90210", Method.GET);

            IRestResponse response = client.Execute(request);

            var responseData = JObject.Parse(response.Content);

            Assert.That(responseData.GetValue("country").ToString(), Is.EqualTo("United States"));
        }

        /***********************************************
         * Send a GET request to /us/90210 and check
         * that the value of the element 'state' for the
         * first place returned is equal to 'California'
         * 
         * Parse the response content to a JObject and
         * use the SelectToken() method to retrieve the
         * element value
	     **********************************************/
        [Test]
        public void GetDataForUsZipCode90210_CheckStateForFirstPlace_ShouldBeCalifornia()
        {
            RestRequest request = new RestRequest("/us/90210", Method.GET);

            IRestResponse response = client.Execute(request);

            var responseData = JObject.Parse(response.Content);

            Assert.That(responseData.SelectToken("places[0].state").ToString(), Is.EqualTo("California"));
        }
    }
}
