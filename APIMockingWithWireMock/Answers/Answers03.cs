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
    public class Answers03
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
         * Create a stub that exerts the following behavior:
         * - The scenario is called 'Stateful mock exercise'
         * - 1. A first GET to /nl/3825 returns HTTP 404 and sets the initial state
         * - 2. A POST to /nl/3825 returns HTTP 201
         * 		and causes a transition to state 'Data present for /nl/3825'
         * - 3. A second GET (when in state 'Data present for /nl/3825') to /nl/3825
         *      returns HTTP 200 and body "DATA FOR /nl/3825"
         */
        public void CreateStubExercise301()
        {
            server.Given(
                Request.Create().WithPath("/nl/3825").UsingGet()
            )
           .InScenario("Stateful mock exercise")
           .WillSetStateTo("No data found for /nl/3825")
           .RespondWith(
                Response.Create().WithStatusCode(404)
           );

            server.Given(
                Request.Create().WithPath("/nl/3825").UsingPost()
            )
            .InScenario("Stateful mock exercise")
            .WhenStateIs("No data found for /nl/3825")
            .WillSetStateTo("Data present for /nl/3825")
            .RespondWith(
                Response.Create().WithStatusCode(201)
            );

            server.Given(
                Request.Create().WithPath("/nl/3825").UsingGet()
            )
           .InScenario("Stateful mock exercise")
           .WhenStateIs("Data present for /nl/3825")
           .RespondWith(
                Response.Create().WithStatusCode(200).WithBody("DATA FOR /nl/3825")
           );
        }

        [Test]
        public void TestExercise301()
        {
            CreateStubExercise301();

            RestRequest request = new RestRequest("/nl/3825", Method.GET);

            IRestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

            request = new RestRequest("/nl/3825", Method.POST);

            response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            request = new RestRequest("/nl/3825", Method.GET);

            response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [TearDown]
        public void StopServer()
        {
            server.Stop();
        }
    }
}
