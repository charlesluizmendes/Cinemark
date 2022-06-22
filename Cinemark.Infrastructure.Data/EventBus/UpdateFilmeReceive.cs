using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Models;
using Cinemark.Infrastructure.Data.EventBus.Common;
using Cinemark.Infrastructure.Data.EventBus.Option;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Cinemark.Infrastructure.Data.EventBus
{
    public class UpdateFilmeReceive : BackgroundService
    {
        private readonly IFilmeRepository _filmeRepository;

        private readonly string? _hostname;
        private readonly string? _queueName;
        private readonly string? _username;
        private readonly string? _password;

        private IModel? _channel;
        private IConnection? _connection;

        public UpdateFilmeReceive(IFilmeRepository filmeRepository,
            IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _filmeRepository = filmeRepository;

            _hostname = rabbitMqOptions.Value.Hostname;
            _queueName = rabbitMqOptions.Value.QueueName;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;

            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password,
                DispatchConsumersAsync = true
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.QueueDeclare(queue: _queueName + EventBusConstants.QUEUE_UPDATED, durable: false, exclusive: false, autoDelete: false, arguments: null);
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Trying to Start EventBus", ex);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (ch, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var filme = JsonConvert.DeserializeObject<Filme>(message);

                    if (filme != null)
                        await HandleMessage(filme);

                    _channel.BasicAck(ea.DeliveryTag, false);

                    await Task.Yield();
                }
                catch (Exception ex)
                {
                    _channel.BasicNack(ea.DeliveryTag, false, true);

                    throw new Exception("Error When Trying to Consume Queue", ex);
                }
            };

            object value = _channel.BasicConsume(_queueName, false, consumer);

            await Task.Yield();
        }

        private async Task HandleMessage(Filme filme)
        {
            await _filmeRepository.UpdateAsync(filme);
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();

            base.Dispose();
        }
    }
}
