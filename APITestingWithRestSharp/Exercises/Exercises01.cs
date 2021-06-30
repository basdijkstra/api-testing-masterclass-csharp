using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Linq;
using System.Net;

namespace APITestingWithRestSharp.Exercises
{
    [TestFixture]
    public class Exercises01
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
        }

        /******************************************************
         * Send a GET request to /us/99999
         * and check that the response has HTTP status code 404
         * (cast the status code to an int)
	     ******************************************************/
        [Test]
        public void GetDataForUsZipCode99999_CheckStatusCode_ShouldBeHttp404()
        {
        }

        /*******************************************************
         * Send a GET request to /us/90210
         * and check that the response is in JSON format 
         ******************************************************/
        [Test]
        public void GetDataForUsZipCode90210_CheckContentType_ShouldBeApplicationJson()
        {
        }

        /*******************************************************
         * Send a GET request to /us/90210
         * and check that the response has a header 'CF-Cache-Status'
         * (case sensitive!) with value 'DYNAMIC'
         ******************************************************/
        [Test]
        public void GetDataForUsZipCode90210_CheckCfCacheStatus_ShouldBeDynamic()
        {
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
        }
    }
}
