using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace APITestingWithRestSharp.Examples
{
    [TestFixture]
    public class Examples02
    {
        private RestClient client;

        private const string BASE_URL = "http://jsonplaceholder.typicode.com";

        [OneTimeSetUp]
        public void SetupRestSharpClient()
        {
            client = new RestClient(BASE_URL);
        }

        [Test]
        public void AddPathParameterAsUrlSegmentToRequest()
        {
            RestRequest request = (RestRequest) new RestRequest("/users/{userid}", Method.GET)
                .AddUrlSegment("userid", 1);

            IRestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void AddQueryParameter()
        {
            RestRequest request = (RestRequest) new RestRequest("/users", Method.GET)
                .AddParameter("name", "value");

            IRestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void AddQueryParameterExplicit()
        {
            RestRequest request = (RestRequest) new RestRequest("/users", Method.GET)
                .AddParameter("name", "value", ParameterType.QueryString);

            IRestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void GetDataForUser1_CheckName_ShouldBeLeanneGraham()
        {
            RestRequest request = new RestRequest("/users/1", Method.GET);

            IRestResponse response = client.Execute(request);

            var responseData = JObject.Parse(response.Content);

            Assert.That(responseData.GetValue("name").ToString(), Is.EqualTo("Leanne Graham"));
        }

        [Test]
        public void GetDataForUser2_CheckName_ShouldBeErvinHowell()
        {
            RestRequest request = new RestRequest("/users/2", Method.GET);

            IRestResponse response = client.Execute(request);

            var responseData = JObject.Parse(response.Content);

            Assert.That(responseData.GetValue("name").ToString(), Is.EqualTo("Ervin Howell"));
        }

        [Test]
        public void GetDataForUser3_CheckName_ShouldBeClementineBauch()
        {
            RestRequest request = new RestRequest("/users/3", Method.GET);

            IRestResponse response = client.Execute(request);

            var responseData = JObject.Parse(response.Content);

            Assert.That(responseData.GetValue("name").ToString(), Is.EqualTo("Clementine Bauch"));
        }

        [TestCase(1, "Leanne Graham", TestName = "User 1 is Leanne Graham - using TestCase")]
        [TestCase(2, "Ervin Howell", TestName = "User 2 is Ervin Howell - using TestCase")]
        [TestCase(3, "Clementine Bauch", TestName = "User 3 is Clementine Bauch - using TestCase")]
        public void DataDrivenTestUsingTestCaseAttribute(int userId, string expectedName)
        {
            RestRequest request = (RestRequest) new RestRequest("/users/{userid}", Method.GET)
                .AddUrlSegment("userid", userId);

            IRestResponse response = client.Execute(request);

            var responseData = JObject.Parse(response.Content);

            Assert.That(responseData.GetValue("name").ToString(), Is.EqualTo(expectedName));
        }

        [Test, TestCaseSource(nameof(UserTestData))]
        public void DataDrivenTestUsingTestCaseSource(int userId, string expectedName)
        {
            RestRequest request = (RestRequest)new RestRequest("/users/{userid}", Method.GET)
                .AddUrlSegment("userid", userId);

            IRestResponse response = client.Execute(request);

            var responseData = JObject.Parse(response.Content);

            Assert.That(responseData.GetValue("name").ToString(), Is.EqualTo(expectedName));
        }

        private static IEnumerable<TestCaseData> UserTestData()
        {
            yield return new TestCaseData(1, "Leanne Graham").
                SetName("User 1 is Leanne Graham - using TestCaseSource");
            yield return new TestCaseData(2, "Ervin Howell").
                SetName("User 2 is Ervin Howell - using TestCaseSource");
            yield return new TestCaseData(3, "Clementine Bauch").
                SetName("User 3 is Clementine Bauch - using TestCaseSource");
        }
    }
}
