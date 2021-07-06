using NUnit.Framework;
using RestSharp;
using System.Net;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace APIMockingWithWireMock.Examples
{
    [TestFixture]
    public class Examples02
    {
        private WireMockServer server;

        [SetUp]
        public void StartServer()
        {
            server = WireMockServer.Start(9876);
        }

        public void CreateStubUrlMatching()
        {
            server.Given(
                Request.Create().WithPath("/url-matching").UsingGet()
            )
            .RespondWith(
                Response.Create()
                .WithBody("URL matching")
            );
        }

        public void CreateStubHeaderMatching()
        {
            server.Given(
                Request.Create().WithPath("/header-matching").UsingGet()
                .WithHeader("Content-Type", "application/json")
                .WithHeader("ShouldNotBeThere", ".*", matchBehaviour: WireMock.Matchers.MatchBehaviour.RejectOnMatch)
            )
            .RespondWith(
                Response.Create()
                .WithBody("Header matching")
            );
        }

        public void CreateStubReturningErrorCode()
        {
            server.Given(
                Request.Create().WithPath("/error-code").UsingGet()
            )
            .RespondWith(
                Response.Create()
                .WithStatusCode(500)
            );
        }

        public void CreateStubReturningDelayedResponse()
        {
            server.Given(
                Request.Create().WithPath("/delay").UsingGet()
            )
            .RespondWith(
                Response.Create()
                .WithStatusCode(200)
                .WithDelay(3000)
            );
        }

        public void CreateStubReturningFault()
        {
            server.Given(
                Request.Create().WithPath("/fault").UsingGet()
            )
            .RespondWith(
                Response.Create()
                .WithFault(FaultType.MALFORMED_RESPONSE_CHUNK)
            );
        }

        public void CreateStubSometimesReturningFault()
        {
            server.Given(
                Request.Create().WithPath("/fault").UsingGet()
            )
            .RespondWith(
                Response.Create()
                .WithFault(FaultType.MALFORMED_RESPONSE_CHUNK, percentage: 0.5)
            );
        }

        [TearDown]
        public void StopServer()
        {
            server.Stop();
        }
    }
}
