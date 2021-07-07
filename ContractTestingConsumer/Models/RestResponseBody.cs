using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContractTestingConsumer.Models
{
    public class RestResponseBody
    {
        [JsonProperty(PropertyName = "_links")]
        public Dictionary<string, HypermediaLink> Links { get; set; }

        public RestResponseBody()
        {
            Links = new Dictionary<string, HypermediaLink>();
        }
    }
}
