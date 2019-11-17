using System;
using System.IO;
using lab.domain.Models;
using lab.domain.Models.Api;
using lab.infrastructure.data.Database;
using lab.infrastructure.data.Database.Interfaces;
using lab.infrastructure.data.Models;
using lab.infrastructure.data.Repositories;
using lab.infrastructure.data.Repositories.Interfaces;
using lab.infrastructure.data.Settings;
using lab.infrastructure.data.Settings.Interfaces;
using lab.service.Interfaces;
using lab.service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace lab.infrastructure.ioc
{
    public class IoC
    {
        private static IServiceProvider _serviceProvider;

        public static void ConfigureInjections(
            IConfiguration configuration,
            IServiceCollection serviceCollection)
        {
            if (File.Exists(Path.Combine(AppContext.BaseDirectory, "log4net.config")))
            {
                serviceCollection.AddSingleton(new LoggerFactory().AddLog4Net("log4net.config"));
            }
            else
            {
                serviceCollection.AddSingleton(new NullLoggerFactory());
            }
            
            serviceCollection.AddSingleton<IDatabaseSettings>(sp => 
                new DatabaseSettings(
                    configuration.GetSection("DatabaseSettings:ConnectionString").Value,
                    configuration.GetSection("DatabaseSettings:DatabaseName").Value)
            );

            var mapperConfig = new AutoMapper.MapperConfiguration(c =>
            {
                #region Data Models
                c.CreateMap<ProductDataModel, ProductModel>().ReverseMap();
                #endregion Data Models

                #region Api Models
                c.CreateMap<ProductApiModel, ProductModel>().ReverseMap();
                #endregion Api Models
            });
                        
            serviceCollection.AddSingleton(mapperConfig.CreateMapper());

            serviceCollection.AddSingleton<IMongoContext, MongoContext>();

            serviceCollection.AddSingleton<IProductRepository, ProductRepository>();

            serviceCollection.AddSingleton<IProductService, ProductService>();

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public static Y GetService<Y>()
        {
            return _serviceProvider.GetService<Y>();
        }
    }
}
