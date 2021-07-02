using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APITestingWithRestSharp.Answers.POCO
{
    public class Location
    {
        [JsonProperty("post code")]
        public string PostCode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("country abbreviation")]
        public string CountryAbbreviation { get; set; }

        [JsonProperty("places")]
        public List<Place> Places { get; set; }
}
}
