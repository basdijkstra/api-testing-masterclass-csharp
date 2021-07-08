using ContractTestingConsumer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ContractTestingConsumer
{
    public class ZipApiClient : IDisposable
    {
        private readonly HttpClient _httpClient;

        public ZipApiClient(string baseUri = null)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseUri ?? "http://my.api/v2/capture") };
        }

        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        public async Task<Location> GetLocationByCountryAndZipCode(string countryCode, string zipCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, string.Format("/zip/{0}/{1}", countryCode, zipCode));
            request.Headers.Add("Accept", "application/json");

            var response = await _httpClient.SendAsync(request);

            try
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<Location>(await response.Content.ReadAsStringAsync(), _jsonSettings);
                }

                await RaiseResponseError(request, response);
            }
            finally
            {
                Dispose(request, response);
            }

            return null;
        }

        private static async Task RaiseResponseError(HttpRequestMessage failedRequest, HttpResponseMessage failedResponse)
        {
            throw new HttpRequestException(
                string.Format("The Location API request for {0} {1} failed. Response Status: {2}, Response Body: {3}",
                failedRequest.Method.ToString().ToUpperInvariant(),
                failedRequest.RequestUri,
                (int)failedResponse.StatusCode,
                await failedResponse.Content.ReadAsStringAsync()));
        }

        public void Dispose()
        {
            Dispose(_httpClient);
        }

        public void Dispose(params IDisposable[] disposables)
        {
            foreach (var disposable in disposables.Where(d => d != null))
            {
                disposable.Dispose();
            }
        }
    }
}
