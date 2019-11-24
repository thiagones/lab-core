using System;
using System.Reflection;
using System.Text;
using lab.domain.Events.Args;
using lab.domain.Events.Handlers;
using lab.domain.Interfaces.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace lab.mq.Messaging
{
    public class RabbitMQConnection : IMessageQueueConnection
    {
        private IConnection _connection;
        private IModel _channel;
        private EventingBasicConsumer _consumer;
        private IConfiguration _configuration;
        private ILogger _logger;

        public event MessageQueueReceivedMessageEventHandler ReceivedMessage;

        public RabbitMQConnection(
            IConfiguration configuration,
            ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _logger = loggerFactory?.CreateLogger(this.GetType());
        }

        public void Connect()
        {
            _logger?.LogInformation("Inicinando -> Connect");

            string hostName = _configuration["MQ:Host"];
            int port = Convert.ToInt32(_configuration["MQ:Port"]);
            string username = _configuration["MQ:Username"];
            string password = _configuration["MQ:Password"];

            IConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://ujdvpjcg:wFo_zKIlyn8ClA9ZhX2uQOYc7qNzBt0G@rhino.rmq.cloudamqp.com/ujdvpjcg");
            
            _connection = factory.CreateConnection();
            //_connection.CallbackException += Callback;
            _connection.ConnectionBlocked += ConnectionBlocked;
            _connection.ConnectionRecoveryError += ConnectionRecoveryError;
            _connection.ConnectionShutdown += ConnectionShutdown;
            _connection.ConnectionUnblocked += ConnectionUnblocked;

            _logger?.LogInformation("Finalizando -> Connect");
        }

        public void PostMessage(string destination, string correlationId, string command, string message, string repplyTo = "")
        {
            _logger?.LogInformation("Inicinando -> PostMessage");

            if (_connection == null)
            {
                Connect();
            }

            if (_channel == null)
            {
                _channel = _connection.CreateModel();
            }

            _channel.QueueDeclare(
                queue: destination,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(
                exchange: "",
                routingKey: correlationId,
                basicProperties: null,
                body: body
            );

            _logger?.LogInformation("Finalizando -> PostMessage");
        }

        public void ListenToQueue(string destination, string selector)
        {
            _logger?.LogInformation("Inicinando -> ListenToQueue");

            if (_connection == null)
            {
                Connect();
            }

            if (_channel == null)
            {
                _channel = _connection.CreateModel();
            }

            _channel.QueueDeclare(
                 queue: destination,
                 durable: true,
                 exclusive: false,
                 autoDelete: false,
                 arguments: null);

            _consumer = new EventingBasicConsumer(_channel);
            
            _consumer.ConsumerTag = Assembly.GetEntryAssembly().ManifestModule.Name.Replace(".dll", "");

            _consumer.Received += (e, args) =>
            {
                _logger?.LogInformation("Iniciando -> Recebimento de Mensagem");
                
                string message = Encoding.UTF8.GetString(args.Body);

                ReceivedMessage(this, new ReceivedMessageEventArgs
                {
                    Message = message,
                    CorrelationId = args.RoutingKey,
                    Command = args.ConsumerTag
                });

                _logger?.LogInformation("Iniciando -> Confirmação de processamento");

                _consumer.Model.BasicAck(args.DeliveryTag, false);
                
                _logger?.LogInformation("Finalizando -> Confirmação de processamento");

                _logger?.LogInformation("Finalizando -> Recebimento de Mensagem");
            };

            _channel.BasicConsume(
                queue: destination,
                autoAck: false,
                consumer: _consumer);

            _logger?.LogInformation("Finalizando -> ListenToQueue");
        }

        public void ListenToTopic(string destination, string selector, Func<string, string, string, bool> receivedMessage)
        {

        }

        public void ConnectionBlocked(object sender, ConnectionBlockedEventArgs args)
        {

        }

        private void ConnectionRecoveryError(object sender, ConnectionRecoveryErrorEventArgs args)
        {

        }

        private void ConnectionShutdown(object sender, ShutdownEventArgs args)
        {

        }

        private void ConnectionUnblocked(object sender, EventArgs args)
        {

        }

        private void SessionShutdown(object sender, ShutdownEventArgs args)
        {

        }

        public void Dispose()
        {
            _consumer = null;

            _channel.Dispose();
            _channel = null;

            _connection.Dispose();
            _connection = null;
            
        }
    }
}