using APITestingWithRestSharp.Examples.POCO;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace APITestingWithRestSharp.Examples
{
    [TestFixture]
    public class Examples03
    {
        private RestClient client;

        private const string BASE_URL = "http://jsonplaceholder.typicode.com";

        [OneTimeSetUp]
        public void SetupRestSharpClient()
        {
            client = new RestClient(BASE_URL);
        }

        [Test]
        public void DeserializeResponseToPoco_AssertOnTitle()
        {
            RestRequest request = (RestRequest) new RestRequest("/todos/{id}", Method.GET)
                .AddUrlSegment("id", 1);

            IRestResponse response = client.Execute(request);

            Todo todo = new JsonDeserializer().Deserialize<Todo>(response);

            Assert.That(todo.Title, Is.EqualTo("delectus aut autem"));
        }

        [Test]
        public void SerializePocoToRequest_AssertOnStatusCode()
        {
            Todo todo = new Todo
            {
                UserId = 1,
                Title = "Take out the trash",
                Completed = false
            };

            RestRequest request = (RestRequest)new RestRequest("/todos", Method.POST)
                .AddJsonBody(todo);

            IRestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
    }
}
