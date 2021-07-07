using System;
using System.Collections.Generic;
using System.Text;

namespace ContractTestingConsumer.Models
{
    public class StatusResponseBody : RestResponseBody
    {
        public bool Alive { get; set; }
    }
}
