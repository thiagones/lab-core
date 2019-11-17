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
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;


        public UserController(
            IConfiguration configuration,
            ILogger<UserController> logger,
            IMapper mapper,
            IUserService userService)
        {
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody]AuthenticateApiModel authenticate)
        {
            var user = await _userService.AuthenticateAsync(authenticate.Username, authenticate.Password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return new OkObjectResult(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PostAsync([FromBody]UserApiModel userApi)
        {
            var user = _mapper.Map<UserModel>(userApi);
            
            user = await _userService.AddAsync(user);

            userApi = _mapper.Map<UserApiModel>(user);

            return new CreatedResult(user.Id, userApi);
        }
    }
}
