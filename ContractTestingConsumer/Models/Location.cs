using System;
using System.Collections.Generic;
using System.Text;

namespace ContractTestingConsumer.Models
{
    public class Location
    {
        public string PlaceName{ get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string CountryAbbreviation { get; set; }
        public bool Active { get; set; }
    }
}
