using ContractTestingProvider.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractTestingProvider.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ZipController : ControllerBase
    {
        private readonly ILogger<ZipController> _logger;

        public ZipController(ILogger<ZipController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{countryCode}/{zipCode}")]
        public ActionResult<Location> GetLocationForCountryCodeAndZipCode(string countryCode, string zipCode)
        {
            if (countryCode.ToLower().Equals("us") && zipCode.ToLower().Equals("99999"))
            {
                return NotFound();
            }

            return new Location
            {
                PlaceName = "Beverly Hills",
                State = "California",
                Country = "United States",
                CountryAbbreviation = "US",
                Active = true
            };
        }
    }
}
