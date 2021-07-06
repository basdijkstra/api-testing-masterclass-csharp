using NUnit.Framework;
using System;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace APIMockingWithWireMock.Examples
{
    [TestFixture]
    public class Examples03
    {
        private WireMockServer server;

        [SetUp]
        public void StartServer()
        {
            server = WireMockServer.Start(9876);
        }

        public void CreateStubStateful()
        {
            server.Given(
                Request.Create().WithPath("/todo/items").UsingGet()
            )
           .InScenario("To do list")
           .WillSetStateTo("TodoList State Started")
           .RespondWith(
                Response.Create().WithBody("Buy milk")
           );

           server.Given(
               Request.Create().WithPath("/todo/items").UsingPost()
           )
           .InScenario("To do list")
           .WhenStateIs("TodoList State Started")
           .WillSetStateTo("Cancel newspaper item added")
           .RespondWith(
               Response.Create().WithStatusCode(201)
           );

           server.Given(
               Request.Create().WithPath("/todo/items").UsingGet()
           )
           .InScenario("To do list")
           .WhenStateIs("Cancel newspaper item added")
           .RespondWith(
               Response.Create().WithBody("Buy milk;Cancel newspaper subscription")
           );
        }

        [TearDown]
        public void StopServer()
        {
            server.Stop();
        }
    }
}
