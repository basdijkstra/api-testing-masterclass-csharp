using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Linq;
using System.Net;

namespace APITestingWithRestSharp.Examples
{
    [TestFixture]
    public class Examples01
    {
        private RestClient client;

        private const string BASE_URL = "http://jsonplaceholder.typicode.com";

        [OneTimeSetUp]
        public void SetupRestSharpClient()
        {
            client = new RestClient(BASE_URL);
        }

        [Test]
        public void GetDataForUser1_CheckStatusCode_ShouldBeHttpOK()
        {
            RestRequest request = new RestRequest("/users/1", Method.GET);

            IRestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void GetDataForUser1_CheckStatusCode_ShouldBeHttp200()
        {
            RestRequest request = new RestRequest("/users/1", Method.GET);

            IRestResponse response = client.Execute(request);

            Assert.That((int)response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void GetDataForUser2_CheckContentType_ShouldBeApplicationJson()
        {
            RestRequest request = new RestRequest("/users/2", Method.GET);

            IRestResponse response = client.Execute(request);

            Assert.That(response.ContentType, Does.Contain("application/json"));
        }

        [Test]
        public void GetDataForUser3_CheckServerHeader_ShouldBeCloudflare()
        {
            RestRequest request = new RestRequest("/users/3", Method.GET);

            IRestResponse response = client.Execute(request);

            string serverHeaderValue = response.Headers
                .Where(x => x.Name.Equals("Server"))
                .Select(x => x.Value.ToString())
                .FirstOrDefault();

            Assert.That(serverHeaderValue, Is.EqualTo("cloudflare"));
        }

        [Test]
        public void GetDataForUser4_CheckName_ShouldBePatriciaLebsack()
        {
            RestRequest request = new RestRequest("/users/4", Method.GET);

            IRestResponse response = client.Execute(request);

            var responseData = JObject.Parse(response.Content);

            Assert.That(responseData.GetValue("name").ToString(), Is.EqualTo("Patricia Lebsack"));
        }

        [Test]
        public void GetDataForUser5_CheckCompanyCatchPhrase_ShouldBeUserCentricBlahBlah()
        {
            RestRequest request = new RestRequest("/users/5", Method.GET);

            IRestResponse response = client.Execute(request);

            var responseData = JObject.Parse(response.Content);

            Assert.That(responseData.SelectToken("company.catchPhrase").ToString(), Is.EqualTo("User-centric fault-tolerant solution"));
        }
    }
}
