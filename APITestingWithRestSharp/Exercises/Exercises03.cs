using APITestingWithRestSharp.Answers.POCO;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Linq;

namespace APITestingWithRestSharp.Exercises
{
    [TestFixture]
    public class Exercises03
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
         * Deserialize the response into an instance of the
         * Location POCO and use that instance to assert that
         * the CountryAbbreviation is equal to 'US'.
	     ******************************************************/

        [Test]
        public void GetDataForUsZipCode90210_DeserializeResponseToPoco_CheckCountryAbbreviationIsUS()
        {
        }

        /******************************************************
         * Send a GET request to /us/12345
         * Deserialize the response into an instance of the
         * Location POCO and use that instance to assert that
         * the place name for the first place in the list is
         * equal to 'Schenectady'.
	     ******************************************************/

        [Test]
        public void GetDataForUsZipCode12345_DeserializeResponseToPoco_CheckPlaceNameOfFirstPlaceIsSchenectady()
        {
        }

        /******************************************************
         * Send a GET request to /de/24848
         * Deserialize the response into an instance of the
         * Location POCO and use that instance to assert that
         * the list of place names has a length equal to 4.
	     ******************************************************/

        [Test]
        public void GetDataForDeZipCode24848_DeserializeResponseToPoco_CheckNumberOfPlacesIs4()
        {
        }

        /******************************************************
         * EXTRA
         * Send a GET request to /de/24848
         * Deserialize the response into an instance of the
         * Location POCO and use that instance and a Linq query
         * to assert that the list of place names contains 2
         * places with a name starting with 'Klein'
	     ******************************************************/

        [Test]
        public void GetDataForDeZipCode24848_DeserializeResponseToPoco_CheckNumberOfPlaceNamesStartingWithKleinIs2()
        {
        }
    }
}
