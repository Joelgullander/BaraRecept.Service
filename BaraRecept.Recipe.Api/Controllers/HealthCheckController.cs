using Microsoft.AspNetCore.Mvc;

namespace BaraRecept.Recipe.Api.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// API-methods for health check
    /// </summary>
    public class HealthCheckController : BaseController
    {
        /// <summary>
        /// Gets health of service
        /// </summary>
        /// <remarks>
        /// Used for polling the service for current status
        /// </remarks>
        /// <returns>StatusCode 200 with BODY "OK"</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("OK");
        }
    }
}
