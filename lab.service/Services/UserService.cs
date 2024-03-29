using System.Threading.Tasks;
using lab.domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using lab.domain.Models;
using AutoMapper;
using lab.domain.Models.Data;
using lab.domain.Interfaces.Repositories;

namespace lab.service.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(
            ILoggerFactory loggerFactory,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _logger = loggerFactory.CreateLogger(this.GetType());
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserModel> AddAsync(UserModel user)
        {
            _logger.LogInformation("Iniciando -> AddAsync");

            var userData = _mapper.Map<UserDataModel>(user);

            userData = await _userRepository.AddAsync(userData);

            user = _mapper.Map<UserModel>(userData);

            _logger.LogInformation("Finalizando -> AddAsync");
            
            return user;
        }

        public async Task<UserModel> AuthenticateAsync(string username, string password)
        {
            _logger.LogInformation("Iniciando -> Authenticate");

            var userData = await _userRepository.AuthenticateAsync(username, password);

            var user = _mapper.Map<UserModel>(userData);

            _logger.LogInformation("Finalizando -> Authenticate");
            
            return user;
        }
    }
}