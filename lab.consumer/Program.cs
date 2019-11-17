using System;
using System.Linq;
using System.Threading.Tasks;
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

        private static async Task Main(string[] args)
        {
            int cont = 1;
            while (true)
            {

                try
                {
                    if (cont == 6)
                    {
                        throw new Exception("Erro inesperado! Bah!");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.ToString());
                    throw;
                }

                var products = await _productService.GetByNameAsync("Ração");

                _logger.LogInformation($"Product {string.Join(" | ", products.Select(x => x.Name).ToArray())} - {cont}");

                cont++;

                System.Threading.Thread.Sleep(1000);
            }
        }


    }
}
