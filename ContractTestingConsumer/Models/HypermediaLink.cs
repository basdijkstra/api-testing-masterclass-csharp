using System;
using System.Collections.Generic;
using System.Text;

namespace ContractTestingConsumer.Models
{
    public class HypermediaLink
    {
        public string Href { get; set; }

        public HypermediaLink(string href)
        {
            Href = href;
        }
    }
}
