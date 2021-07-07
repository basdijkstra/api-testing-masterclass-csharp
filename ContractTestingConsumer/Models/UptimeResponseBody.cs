using System;
using System.Collections.Generic;
using System.Text;

namespace ContractTestingConsumer.Models
{
    public class UptimeResponseBody : RestResponseBody
    {
        public DateTime UpSince { get; set; }
    }
}
