using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DockerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly ILogger<HealthCheckController> _logger;
        private readonly ConnectionManager _connectionManager;

        public HealthCheckController(ILogger<HealthCheckController> logger,ConnectionManager connectionManager)
        {
            _logger = logger;
            _connectionManager = connectionManager;
        }

        [HttpGet("~/Check")]       
        public IActionResult HealthCheck()
        {
            try
            {
                using (var connection = _connectionManager.GetConnection())
                {
                    connection.Open();
                    var result = connection.ExecuteScalar<string>("select DB_NAME() as name");
                }
                return Ok("API can connect to sql server running in docker.");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Cann't connect to server");
                return StatusCode(500);
            }
        }

    }
}
