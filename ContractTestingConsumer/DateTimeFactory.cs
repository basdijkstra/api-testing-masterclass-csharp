using System;
using System.Collections.Generic;
using System.Text;

namespace ContractTestingConsumer
{
    public class DateTimeFactory
    {
        public static Func<DateTime> Now = () => DateTime.UtcNow;
    }
}
