using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.Configuration;

namespace lab.webapi.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class HealthCheckController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public HealthCheckController(
            IConfiguration configuration,
            ILogger<UserController> logger,
            IMapper mapper)
        {
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("auth")]
        public IActionResult GetAuthenticated()
        {
            var healthCheck = Environment.GetEnvironmentVariable("HEALTHCHECK") ?? "";

            var id = Guid.NewGuid().ToString();
            var result = new
            {
                Id = id,
                HealthCheck = healthCheck,
                Authenticated = false,
                Now = DateTime.Now
            };

            _logger.LogInformation($"Auth Get HealthCheck -> {healthCheck}");

            return new OkObjectResult(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            var healthCheck = Environment.GetEnvironmentVariable("HEALTHCHECK") ?? "";

            var id = Guid.NewGuid().ToString();
            var result = new
            {
                Id = id,
                HealthCheck = healthCheck,
                Authenticated = false,
                Now = DateTime.Now
            };

            _logger.LogInformation($"Get HealthCheck -> {healthCheck}");

            return new OkObjectResult(result);
        }

        [HttpPost]
        [Route("auth")]
        public IActionResult PostAuthenticated()
        {
            var healthCheck = Environment.GetEnvironmentVariable("HEALTHCHECK") ?? "";

            var id = Guid.NewGuid().ToString();
            var result = new
            {
                Id = id,
                HealthCheck = healthCheck,
                Authenticated = false,
                Now = DateTime.Now
            };

            _logger.LogInformation($"Auth Post HealthCheck -> {healthCheck}");

            return new CreatedResult(id, result);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post()
        {
            var healthCheck = Environment.GetEnvironmentVariable("HEALTHCHECK") ?? "";

            var id = Guid.NewGuid().ToString();
            var result = new
            {
                Id = id,
                HealthCheck = healthCheck,
                Authenticated = false,
                Now = DateTime.Now
            };

            _logger.LogInformation($"Post HealthCheck -> {healthCheck}");

            return new CreatedResult(id, result);
        }
    }
}
