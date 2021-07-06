using NUnit.Framework;
using RestSharp;
using System.Net;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace APIMockingWithWireMock.Examples
{
    [TestFixture]
    public class Examples01
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

        public void CreateHelloWorldStub()
        {
            server.Given(
                Request.Create().WithPath("/hello-world").UsingGet()
            )
            .RespondWith(
                Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "text/plain")
                .WithBody("Hello, world!")
            );
        }

        [Test]
        public void TestHelloWorldStub()
        {
            CreateHelloWorldStub();

            RestRequest request = new RestRequest("/hello-world", Method.GET);

            IRestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.ContentType, Is.EqualTo("text/plain"));
            Assert.That(response.Content, Is.EqualTo("Hello, world!"));
        }

        [TearDown]
        public void StopServer()
        {
            server.Stop();
        }
    }
}
