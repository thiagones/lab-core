using System;
using System.Linq;
using System.Threading.Tasks;
using lab.domain.Enums;
using lab.infrastructure.ioc;
using lab.mq.Interfaces;
using lab.mq.Messaging;
using lab.service.Interfaces;
using lab.service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace lab.consumer
{
    public class Program
    {

        public static readonly IMessageQueueConnection _messageQueueConnection;
        protected static readonly IConfiguration _configuration;
        protected static readonly ILoggerFactory _loggerFactory;
        protected static readonly ILogger _logger;
        private static readonly IServiceCollection _serviceCollection = new ServiceCollection();
        private static readonly IProductService _productService;

        static Program()
        {

            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            IoC.ConfigureInjections(_configuration, _serviceCollection);

            _loggerFactory = IoC.GetService<ILoggerFactory>();
            _logger = _loggerFactory.CreateLogger<Program>();

            _productService = IoC.GetService<IProductService>();

            Init();
        }
        private static void Init()
        {
            Console.Title = AppDomain.CurrentDomain.FriendlyName;
        }

        private static void Main(string[] args)
        {
            _logger.LogInformation($"Iniciando -> Consumer");

            LabEnvironment LAB_ENVIRONMENT = Environment
                    .GetEnvironmentVariable(nameof(LAB_ENVIRONMENT))
                    .ToEnum<LabEnvironment>();

            switch (LAB_ENVIRONMENT)
            {
                case LabEnvironment.Development:
                    _logger.LogInformation($"Ambiente de desenvolvimento");
                    break;
                case LabEnvironment.Testing:
                    _logger.LogInformation($"Ambiente de testes");
                    break;
                case LabEnvironment.Staging:
                    _logger.LogInformation($"Ambiente de pré produção");
                    break;
                case LabEnvironment.Production:
                    _logger.LogInformation($"Ambiente de produção");
                    break;
                case LabEnvironment.Unset:
                default:
                    _logger.LogInformation($"Ambiente não definido");
                    break;
            }


            _logger.LogInformation($"Finalizando -> Consumer");
        }


    }
}
