using NUnit.Framework;
using RestSharp;
using System.Net;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace APIMockingWithWireMock.Exercises
{
    [TestFixture]
    public class Exercises01
    {
        private WireMockServer server;

        private RestClient client;

        private const string BASE_URL = "http://localhost:9876";

        [OneTimeSetUp]
        public void SetupRestSharpClient()
        {
            client = new RestClient(BASE_URL);
        }

        [SetUp]
        public void StartServer()
        {
            server = WireMockServer.Start(9876);
        }

        /**
         * Create a stub that will respond to a POST
         * to /pl/80-862 with an HTTP status code 200 
         */
        public void CreateStubExercise101()
        {
        }

        /**
         * Create a stub that will respond to a POST
         * to /pl/80-862 with a header 'Content-Type' with
         * value 'text/plain' and a response body 'Posted!'
         */
        public void CreateStubExercise102()
        {
        }

        [Test]
        public void TestExercise101()
        {
            CreateStubExercise101();

            RestRequest request = new RestRequest("/pl/80-862", Method.POST);

            IRestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void TestExercise102()
        {
            CreateStubExercise102();

            RestRequest request = new RestRequest("/pl/80-862", Method.POST);

            IRestResponse response = client.Execute(request);

            Assert.That(response.ContentType, Is.EqualTo("text/plain"));
            Assert.That(response.Content, Is.EqualTo("Posted!"));
        }

        [TearDown]
        public void StopServer()
        {
            server.Stop();
        }
    }
}
