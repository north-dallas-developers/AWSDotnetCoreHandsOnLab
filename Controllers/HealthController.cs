using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AWSDotnetCoreHandsOnLab.Controllers
{
    public class HealthController : Controller
    {
        private readonly ILogger<HealthController> _logger;

        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return Content("This is the health check that will be used by the load balancer.");
        }
    }
}