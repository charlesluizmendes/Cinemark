using Cinemark.Infrastructure.Data.EventBus.Option;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Cinemark.Infrastructure.Data.EventBus.Common
{   
    public class ConnectionEventBus
    {
        private readonly RabbitMqConfiguration _rabbitMqConfiguration;

        public ConnectionEventBus(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _rabbitMqConfiguration = rabbitMqOptions.Value;
        }

        public IConnection CreateChannel()
        {
            var connection = new ConnectionFactory()
            {
                HostName = _rabbitMqConfiguration.HostName,
                UserName = _rabbitMqConfiguration.UserName,
                Password = _rabbitMqConfiguration.Password                
            };

            connection.DispatchConsumersAsync = true;
            var channel = connection.CreateConnection();

            return channel;
        }
    }
}
