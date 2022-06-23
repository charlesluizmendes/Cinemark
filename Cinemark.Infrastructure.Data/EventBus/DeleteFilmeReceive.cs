using Cinemark.Domain.Models;
using Cinemark.Infrastructure.Data.Context;
using Cinemark.Infrastructure.Data.EventBus.Common;
using Cinemark.Infrastructure.Data.EventBus.Option;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Cinemark.Infrastructure.Data.EventBus
{
    public class DeleteFilmeReceive : BackgroundService
    {
        private readonly MongoContext _mongoContext;
        private IMongoCollection<Filme> _mongoCollection;

        private readonly string? _hostname;
        private readonly string? _queueName;
        private readonly string? _username;
        private readonly string? _password;

        private IModel? _channel;
        private IConnection? _connection;

        public DeleteFilmeReceive(MongoContext mongoContext,
            IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _mongoContext = mongoContext;
            _mongoCollection = _mongoContext.GetCollection<Filme>(typeof(Filme).Name);

            _hostname = rabbitMqOptions.Value.Hostname;
            _queueName = rabbitMqOptions.Value.QueueName;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;

            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname,
                    UserName = _username,
                    Password = _password
                };

                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.QueueDeclare(queue: _queueName + EventBusConstants.QUEUE_DELETED, durable: false, exclusive: false, autoDelete: false, arguments: null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var filme = JsonConvert.DeserializeObject<Filme>(message);

                if (filme != null)
                    HandleMessage(filme);

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(_queueName + EventBusConstants.QUEUE_DELETED, false, consumer);

            return Task.CompletedTask;
        }

        private void HandleMessage(Filme filme)
        {
            _mongoCollection.DeleteOneAsync(Builders<Filme>.Filter.Eq("Id", filme.Id));
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();

            base.Dispose();
        }
    }
}
