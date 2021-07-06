using NUnit.Framework;
using System;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace APIMockingWithWireMock.Examples
{
    [TestFixture]
    public class Examples04
    {
        private WireMockServer server;

        [SetUp]
        public void StartServer()
        {
            server = WireMockServer.Start(9876);
        }

        public void CreateStubEchoHttpMethod()
        {
            server.Given(
                Request.Create().WithPath("/echo-http-method").UsingAnyMethod()
            )
            .RespondWith(
                Response.Create()
                .WithBody("HTTP method used was {{request.method}}")
                .WithTransformer()
            );
        }

        public void CreateStubEchoJsonRequestElement()
        {
            server.Given(
                Request.Create().WithPath("/echo-json-request-element").UsingAnyMethod()
            )
            .RespondWith(
                Response.Create()
                .WithBody("{{JsonPath.SelectToken request.body \"$.book.title\"}}")
                .WithTransformer()
            );
        }

        [TearDown]
        public void StopServer()
        {
            server.Stop();
        }
    }
}
