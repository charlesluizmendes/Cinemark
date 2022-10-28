using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Infrastructure.Data.EventBus.Option;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Cinemark.Infrastructure.Data.EventBus
{
    public abstract class BaseEventBus<T> : IBaseEventBus<T> where T : class
    {
        private readonly IOptions<RabbitMqConfiguration> _rabbitMqConfiguration;
        private readonly string _queueName;

        private IConnection? _connection;
        private IModel? _model;

        public BaseEventBus(IOptions<RabbitMqConfiguration> rabbitMqConfiguration,
            string queueName)
        {
            _rabbitMqConfiguration = rabbitMqConfiguration;
            _queueName = queueName;

            TryConnect();
            Queue();
        }

        public void TryConnect()
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqConfiguration.Value.HostName,
                UserName = _rabbitMqConfiguration.Value.UserName,
                Password = _rabbitMqConfiguration.Value.Password,
                DispatchConsumersAsync = true
            };

            _connection = factory.CreateConnection();
        }

        public void Queue()
        {
            _model = _connection.CreateModel();

            _model.ExchangeDeclare(_queueName + "_DeadLetter", ExchangeType.Fanout);
            _model.QueueDeclare(_queueName + "_DeadLetter", true, false, false, null);
            _model.QueueBind(_queueName + "_DeadLetter", _queueName + "_DeadLetter", "");

            var arguments = new Dictionary<string, object>()
            {
                { "x-dead-letter-exchange", _queueName + "_DeadLetter" }
            };

            _model.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: arguments);
        }     

        public async Task PublisherAsync(T entity)
        {
            try
            {
                var json = JsonConvert.SerializeObject(entity);
                var body = Encoding.UTF8.GetBytes(json);

                _model.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);

                await Task.Yield();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SubscriberAsync()
        {
            var consumer = new AsyncEventingBasicConsumer(_model);
            consumer.Received += async (ch, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var entity = JsonConvert.DeserializeObject<T>(message);

                    if (entity != null)
                        await HandlerMessageAsync(entity);

                    _model.BasicAck(ea.DeliveryTag, false);

                    await Task.Yield();
                }
                catch (Exception)
                {
                    _model.BasicNack(ea.DeliveryTag, false, false);
                }
            };

            _model.BasicConsume(_queueName, false, consumer);

            await Task.Yield();
        }

        public abstract Task HandlerMessageAsync(T entity);

        public void Dispose()
        {
            if (_model.IsOpen)
                _model.Close();
            if (_connection.IsOpen)
                _connection.Close();
        }
    }
}
