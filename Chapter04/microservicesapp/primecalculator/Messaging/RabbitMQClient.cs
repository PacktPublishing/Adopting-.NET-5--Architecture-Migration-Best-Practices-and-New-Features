using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;

namespace primecalculator.Messaging
{
    /// <summary>
    /// A straight forward implementation of RabbitMQ client functionality in .NET
    /// 
    /// Remarks: In real world code you will do much more management and error handling
    /// Read more here: https://www.rabbitmq.com/dotnet-api-guide.html
    /// </summary>
    public class RabbitMQClient : IMQClient
    {
        public string hostname { get; }
        public string port { get; }
        public string userid { get; }
        public string password { get; }

        private readonly ILogger _logger;
        private readonly IConnection _connection;
        private IModel _channel;

        public RabbitMQClient(ILogger<RabbitMQClient> logger, IConfiguration configuration)
        {
            _logger = logger;

            hostname = Constants.GetRabbitMQHostName();
            port = Constants.GetRabbitMQPort();
            userid = Constants.GetRabbitMQUser();
            password = Constants.GetRabbitMQPassword();

            try
            {
                logger.LogInformation($"Creating RabbitMQClient connection @ {hostname}:{port}:{userid}:{password}");
                var factory = new ConnectionFactory()
                {
                    HostName = hostname,
                    Port = int.Parse(port),
                    UserName = userid,
                    Password = password,
                };

                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                logger.LogError(-1, ex, "Failure in creating RabbitMQClient connection");
                throw;
            }
        }

        public IModel CreateChannel()
        {
            if (_connection == null)
            {
                _logger.LogError("RabbitMQClient connection is not created when CreatingTheChannel");
                throw new Exception("RabbitMQClient connection is not initialized");
            }
            _channel = _connection.CreateModel();
            return _channel;
        }
    }
}
