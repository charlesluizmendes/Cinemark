using Cinemark.Domain.Entities;
using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Infrastructure.Data.EventBus.Option;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Cinemark.Infrastructure.Data.EventBus
{
    public class CreateFilmeSender : ICreateFilmeSender
    {
        private readonly string? _hostname;
        private readonly string? _queueName;
        private readonly string? _username;
        private readonly string? _password;

        public CreateFilmeSender(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _queueName = rabbitMqOptions.Value.QueueName;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
        }

        public async Task SendMessageAsync(Filme filme)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _hostname,
                    UserName = _username,
                    Password = _password
                };

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var json = JsonConvert.SerializeObject(filme);
                    var body = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);

                    await Task.Yield();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
