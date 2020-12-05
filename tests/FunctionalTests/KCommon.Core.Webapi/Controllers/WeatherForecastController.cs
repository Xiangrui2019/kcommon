using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KCommon.Core.Abstract.Logging;
using Microsoft.AspNetCore.Mvc;

namespace KCommon.Core.Webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger _logger;

        public WeatherForecastController(ILoggerFactory factory)
        {
            _logger = factory.Create(GetType().FullName);
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.Error("Testing");


            return Ok();
        }
    }
}
