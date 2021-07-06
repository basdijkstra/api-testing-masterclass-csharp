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
    public class Answers04
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
         * Create a stub that listens at path /echo-port
         * and responds to all GET requests with HTTP
         * status code 200 and a response body containing
         * the text "Listening on port <portnumber>"
         * where <portnumber> is replaced with the actual port
         * number (9876, in this case)
         * Don't forget to enable response templating!
         */
        public void CreateStubExercise401()
        {
            server.Given(
                Request.Create().WithPath("/echo-port").UsingGet()
            )
            .RespondWith(
                Response.Create()
                .WithStatusCode(200)
                .WithBody("Listening on port {{request.port}}")
                .WithTransformer()
            );
        }

        /**
         * Create a stub that listens at path /echo-car-model
         * and responds to all POST requests with HTTP
         * status code 200 and a response body containing
         * the value of the JSON element $.car.model extracted
         * from the request body.
         * Don't forget to enable response templating!
         */
        public void CreateStubExercise402()
        {
            server.Given(
                Request.Create().WithPath("/echo-car-model").UsingPost()
            )
            .RespondWith(
                Response.Create()
                .WithStatusCode(200)
                .WithBody("{{JsonPath.SelectToken request.body \"$.car.model\"}}")
                .WithTransformer()
            );
        }

        [Test]
        public void TestExercise401()
        {
            CreateStubExercise401();

            RestRequest request = new RestRequest("/echo-port", Method.GET);

            IRestResponse response = client.Execute(request);

            Assert.That(response.Content, Is.EqualTo("Listening on port 9876"));
        }

        [Test]
        public void TestExercise402()
        {
            CreateStubExercise402();

            var carData = new { make = "Alfa Romeo", model = "Giulia 2.9 V6 Quadrifoglio", top_speed = 307 };
            var car = new { car = carData };

            RestRequest request = (RestRequest)new RestRequest("/echo-car-model", Method.POST)
                .AddJsonBody(car);

            request.RequestFormat = DataFormat.Json;

            IRestResponse response = client.Execute(request);

            Assert.That(response.Content, Is.EqualTo("Giulia 2.9 V6 Quadrifoglio"));
        }

        [TearDown]
        public void StopServer()
        {
            server.Stop();
        }
    }
}
