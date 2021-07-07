using System;
using System.Collections.Generic;
using System.Text;

namespace ContractTestingConsumer.Models
{
    public class Event
    {
        public Guid EventId { get; set; }
        public DateTime Timestamp { get; set; }
        public string EventType { get; set; }
    }
}
