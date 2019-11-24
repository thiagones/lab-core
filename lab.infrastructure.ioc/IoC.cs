using System;
using System.IO;
using lab.domain.Models;
using lab.domain.Models.Api;
using lab.infrastructure.data.Database;
using lab.infrastructure.data.Database.Interfaces;
using lab.infrastructure.data.Repositories;
using lab.infrastructure.data.Settings;
using lab.infrastructure.data.Settings.Interfaces;
using lab.mq.Messaging;
using lab.domain.Interfaces.Messaging;
using lab.domain.Interfaces.Services;
using lab.domain.Interfaces.Repositories;
using lab.service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using lab.domain.Models.Data;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using lab.domain.Validators;
using FluentValidation;

namespace lab.infrastructure.ioc
{
    public class IoC
    {
        private static IServiceProvider _serviceProvider;

        public static void ConfigureInjections(
            IConfiguration configuration,
            IServiceCollection serviceCollection)
        {

            #region Log
            if (File.Exists(Path.Combine(AppContext.BaseDirectory, "log4net.config")))
            {
                serviceCollection.AddSingleton(new LoggerFactory().AddLog4Net("log4net.config"));
            }
            else
            {
                serviceCollection.AddSingleton(new NullLoggerFactory());
            }
            #endregion Log

            #region Automapper
            var mapperConfig = new AutoMapper.MapperConfiguration(c =>
            {
                #region Data Models
                c.CreateMap<ProductDataModel, ProductModel>().ReverseMap();
                c.CreateMap<UserDataModel, UserModel>().ReverseMap();
                #endregion Data Models

                #region Api Models
                c.CreateMap<ProductApiModel, ProductModel>().ReverseMap();
                c.CreateMap<UserApiModel, UserModel>().ReverseMap();
                #endregion Api Models
            });

            serviceCollection.AddSingleton(mapperConfig.CreateMapper());
            #endregion Automapper

            #region Database
            serviceCollection.AddSingleton<IDatabaseSettings>(sp =>
                new DatabaseSettings(
                    configuration.GetSection("DatabaseSettings:ConnectionString").Value,
                    configuration.GetSection("DatabaseSettings:DatabaseName").Value)
            );

            serviceCollection.AddSingleton<IMongoContext, MongoContext>();

            BsonClassMap.RegisterClassMap<UserDataModel>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(c => c.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });

            BsonClassMap.RegisterClassMap<ProductDataModel>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(c => c.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });
            #endregion DB Context

            #region Repository
            serviceCollection.AddSingleton<IProductRepository, ProductRepository>();
            serviceCollection.AddSingleton<IUserRepository, UserRepository>();
            #endregion Repository

            #region Service
            serviceCollection.AddSingleton<IProductService, ProductService>();
            serviceCollection.AddSingleton<IUserService, UserService>();
            #endregion Service

            #region Configuration
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            #endregion Configuration

            #region Message Queue
            serviceCollection.AddSingleton<IMessageQueueConnection, RabbitMQConnection>();
            #endregion Message Queue

            #region Validators
            serviceCollection.AddTransient<IValidator<UserApiModel>, UserApiModelValidator>();
            #endregion

            #region Build ServiceProvider
            _serviceProvider = serviceCollection.BuildServiceProvider();
            #endregion Build ServiceProvider
        }

        public static Y GetService<Y>()
        {
            return _serviceProvider.GetService<Y>();
        }
    }
}
