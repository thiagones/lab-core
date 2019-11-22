using System.Threading.Tasks;
using AutoMapper;
using lab.domain.Models.Api;
using lab.service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using lab.domain.Models;
using Microsoft.Extensions.Configuration;

namespace lab.webapi.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
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
            _logger.LogInformation($"Iniciando -> GetAuthenticated");

            var healthCheck = Environment.GetEnvironmentVariable("HEALTHCHECK") ?? "";

            _logger.LogInformation($"HealthCheck -> {healthCheck}");
            
            var id = Guid.NewGuid().ToString();
            var result = new
            {
                Id = id,
                HealthCheck = healthCheck,
                Authenticated = false,
                Now = DateTime.Now
            };

            _logger.LogTrace($"Resultado -> {result}");

            _logger.LogInformation($"Finalizando -> GetAuthenticated");

            return new OkObjectResult(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            _logger.LogInformation($"Iniciando -> Get");

            var healthCheck = Environment.GetEnvironmentVariable("HEALTHCHECK") ?? "";

            _logger.LogInformation($"HealthCheck -> {healthCheck}");

            var id = Guid.NewGuid().ToString();
            var result = new
            {
                Id = id,
                HealthCheck = healthCheck,
                Authenticated = false,
                Now = DateTime.Now
            };

            _logger.LogTrace($"Resultado -> {result}");

            _logger.LogInformation($"Finalizando -> Get");

            return new OkObjectResult(result);
        }

        [HttpPost]
        [Route("auth")]
        public IActionResult PostAuthenticated()
        {
            _logger.LogInformation($"Iniciando -> PostAuthenticated");

            var healthCheck = Environment.GetEnvironmentVariable("HEALTHCHECK") ?? "";

            _logger.LogInformation($"HealthCheck -> {healthCheck}");

            var id = Guid.NewGuid().ToString();
            var result = new
            {
                Id = id,
                HealthCheck = healthCheck,
                Authenticated = false,
                Now = DateTime.Now
            };

            _logger.LogTrace($"Resultado -> {result}");

            _logger.LogInformation($"Finalizando -> PostAuthenticated");

            return new CreatedResult(id, result);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post()
        {
            _logger.LogInformation($"Iniciando -> Post");

            var healthCheck = Environment.GetEnvironmentVariable("HEALTHCHECK");

            _logger.LogInformation($"HealthCheck -> {healthCheck}");

            var id = Guid.NewGuid().ToString();
            var result = new
            {
                Id = id,
                HealthCheck = healthCheck,
                Authenticated = false,
                Now = DateTime.Now
            };

            _logger.LogTrace($"Resultado -> {result}");

            _logger.LogInformation($"Finalizando -> Post");

            return new CreatedResult(id, result);
        }
    }
}
