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

        private IConnection _connection;
        private IModel _model;

        public BaseEventBus(IOptions<RabbitMqConfiguration> rabbitMqConfiguration)
        {
            _rabbitMqConfiguration = rabbitMqConfiguration;

            TryConnect();

            Queue(typeof(T).Name + "_Insert");
            Queue(typeof(T).Name + "_Update");
            Queue(typeof(T).Name + "_Delete");
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

        public void Queue(string queueName)
        {
            _model = _connection.CreateModel();

            var argsQueue = new Dictionary<string, object>()
            {
                { "x-dead-letter-exchange", queueName + "_DeadLetterExchange" },
                { "x-dead-letter-routing-key", queueName + "_DeadLetterQueue" }
            };

            _model.ExchangeDeclare(exchange: queueName, type: ExchangeType.Fanout);
            _model.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: argsQueue);
            _model.QueueBind(queue: queueName, exchange: queueName, routingKey: string.Empty, arguments: null);

            _model.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var argsDeadLetter = new Dictionary<string, object>()
            {
               { "x-dead-letter-exchange", queueName },
               { "x-message-ttl", 60000 }
            };

            _model.ExchangeDeclare(exchange: queueName + "_DeadLetterExchange", type: ExchangeType.Fanout);
            _model.QueueDeclare(queue: queueName + "_DeadLetterQueue", durable: true, exclusive: false, autoDelete: false, arguments: argsDeadLetter);
            _model.QueueBind(queue: queueName + "_DeadLetterQueue", exchange: queueName + "_DeadLetterExchange", routingKey: string.Empty, arguments: null);
        }     

        public async Task PublisherAsync(string queueName, T entity)
        {
            try
            {
                var json = JsonConvert.SerializeObject(entity);
                var body = Encoding.UTF8.GetBytes(json);

                _model.BasicPublish(exchange: queueName, routingKey: queueName, basicProperties: null, body: body);

                await Task.Yield();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SubscriberAsync(string queueName, Func<T, CancellationToken, Task<bool>> entity)
        {
            var consumer = new AsyncEventingBasicConsumer(_model);
            consumer.Received += async (ch, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var text = Encoding.UTF8.GetString(body);
                    var message = JsonConvert.DeserializeObject<T>(text);

                    if (message != null)
                    {
                        var result = entity(message, default).GetAwaiter().GetResult();

                        if (result)
                        {
                            _model.BasicAck(ea.DeliveryTag, false);
                        }
                        else
                        {
                            _model.BasicNack(ea.DeliveryTag, false, false);
                        }
                    }                        
                    else
                    {
                        _model.BasicAck(ea.DeliveryTag, false);
                    }                    

                    await Task.Yield();
                }
                catch (Exception)
                {
                    _model.BasicNack(ea.DeliveryTag, false, false);
                }
            };

            _model.BasicConsume(queueName, false, consumer);

            await Task.Yield();
        }

        public void Dispose()
        {
            if (_model.IsOpen)
                _model.Close();
            if (_connection.IsOpen)
                _connection.Close();
        }        
    }
}
