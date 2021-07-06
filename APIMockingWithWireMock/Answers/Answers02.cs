using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Diagnostics;
using System.Net;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace APIMockingWithWireMock.Answers
{
    [TestFixture]
    public class Answers02
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
         * Create a stub that will respond to all GET
         * requests to /servicedown
         * with HTTP status code 503
         */
        public void CreateStubExercise201()
        {
            server.Given(
                Request.Create().WithPath("/servicedown").UsingGet()
            )
            .RespondWith(
                Response.Create()
                .WithStatusCode(503)
            );
        }

        /**
         * Create a stub that will respond to a GET request
         * to /slow with request header 'speed' with value 'slow'.
         * Respond with status code 200, but only after a
         * fixed delay of 3000 milliseconds.
         */
        public void CreateStubExercise202()
        {
            server.Given(
                Request.Create().WithPath("/slow").UsingGet()
                .WithHeader("speed", "slow")
            )
            .RespondWith(
                Response.Create()
                .WithStatusCode(200)
                .WithDelay(TimeSpan.FromMilliseconds(3000))
            );
        }

        /**
         * Create a stub that will respond to a GET request
         * to /fault.
         * Respond with a malformed response chunk fault.
         */
        public void CreateStubExercise203()
        {
            server.Given(
                Request.Create().WithPath("/fault").UsingGet()
            )
            .RespondWith(
                Response.Create()
                .WithFault(FaultType.MALFORMED_RESPONSE_CHUNK)
            );
        }

        [Test]
        public void TestExercise201()
        {
            CreateStubExercise201();

            RestRequest request = new RestRequest("/servicedown", Method.GET);

            IRestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.ServiceUnavailable));
        }

        [Test]
        public void TestExercise202()
        {
            CreateStubExercise202();

            RestRequest request = (RestRequest) new RestRequest("/slow", Method.GET)
                .AddHeader("speed", "slow");

            Stopwatch stopWatch = Stopwatch.StartNew();

            IRestResponse response = client.Execute(request);

            stopWatch.Stop();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(stopWatch.ElapsedMilliseconds, Is.GreaterThanOrEqualTo(3000));
        }

        [Test]
        public void TestExercise203()
        {
            CreateStubExercise203();

            RestRequest request = new RestRequest("/fault", Method.GET);

            IRestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            Assert.Throws<Newtonsoft.Json.JsonReaderException>(
                () => { JObject.Parse(response.Content); }
                );
        }

        [TearDown]
        public void StopServer()
        {
            server.Stop();
        }
    }
}
