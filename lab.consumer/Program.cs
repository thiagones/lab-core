using System;
using lab.domain.Enums;
using lab.infrastructure.ioc;
using lab.domain.Events.Args;
using lab.domain.Interfaces.Messaging;
using lab.domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace lab.consumer
{
    public class Program
    {

        public static IMessageQueueConnection _messageQueueConnection;
        private static readonly IConfiguration _configuration;
        private static readonly ILoggerFactory _loggerFactory;
        private static readonly ILogger _logger;
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

            StartConsumer("queueTest");

            while (true)
            {
                System.Threading.Thread.Sleep(100);
            }

            //_logger.LogInformation($"Finalizando -> Consumer");
        }

        protected static void StartConsumer(string queueName)
        {
           using (_messageQueueConnection = IoC.GetService<IMessageQueueConnection>())
            {
                _messageQueueConnection.ReceivedMessage += ReceivedMessage;

                _messageQueueConnection.ListenToQueue(queueName, null);

                while (true)
                {
                    System.Threading.Thread.Sleep(300);
                }
            }
        }

        public static void ReceivedMessage(IMessageQueueConnection connection, ReceivedMessageEventArgs e)
        {
            _logger.LogInformation($"Mensagem recebida -> {e.Message}");
        }
    }
}
