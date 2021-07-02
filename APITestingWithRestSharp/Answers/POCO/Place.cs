using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APITestingWithRestSharp.Answers.POCO
{
    public class Place
    {
        [JsonProperty("place name")]
        public string PlaceName { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("state abbreviation")]
        public string StateAbbreviation { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }
    }
}
