using Cinemark.Domain.Constants;
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

        private const int Retry = 60000;
        private const string Queue = ".Queue";
        private const string Exchange = ".Exchange";        
        private const string DeadLetterQueue = ".DeadLetter.Queue";
        private const string DeadLetterExchange = ".DeadLetter.Exchange";

        public BaseEventBus(IOptions<RabbitMqConfiguration> rabbitMqConfiguration)
        {
            _rabbitMqConfiguration = rabbitMqConfiguration;

            CreateConnect();

            CreateQueue(typeof(T).Name + QueueConstants.Insert);
            CreateQueue(typeof(T).Name + QueueConstants.Update);
            CreateQueue(typeof(T).Name + QueueConstants.Delete);
        }

        public void CreateConnect()
        {
            var factory = new ConnectionFactory
            {
                HostName = _rabbitMqConfiguration.Value.HostName,
                Port = _rabbitMqConfiguration.Value.Port,
                UserName = _rabbitMqConfiguration.Value.UserName,
                Password = _rabbitMqConfiguration.Value.Password,
                DispatchConsumersAsync = true
            };

            _connection = factory.CreateConnection();
        }

        public void CreateQueue(string queueName)
        {
            _model = _connection.CreateModel();

            var argsQueue = new Dictionary<string, object>()
            {
                { "x-dead-letter-exchange", queueName + DeadLetterExchange },
                { "x-dead-letter-routing-key", queueName + DeadLetterQueue }
            };

            _model.ExchangeDeclare(exchange: queueName + Exchange, type: ExchangeType.Fanout);
            _model.QueueDeclare(queue: queueName + Queue, durable: true, exclusive: false, autoDelete: false, arguments: argsQueue);
            _model.QueueBind(queue: queueName + Queue, exchange: queueName + Exchange, routingKey: string.Empty, arguments: null);

            _model.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var argsDeadLetter = new Dictionary<string, object>()
            {
               { "x-dead-letter-exchange", queueName + Exchange },
               { "x-message-ttl", Retry }
            };

            _model.ExchangeDeclare(exchange: queueName + DeadLetterExchange, type: ExchangeType.Fanout);
            _model.QueueDeclare(queue: queueName + DeadLetterQueue, durable: true, exclusive: false, autoDelete: false, arguments: argsDeadLetter);
            _model.QueueBind(queue: queueName + DeadLetterQueue, exchange: queueName + DeadLetterExchange, routingKey: string.Empty, arguments: null);
        }     

        public async Task PublisherAsync(string queueName, T entity)
        {
            try
            {
                var json = JsonConvert.SerializeObject(entity);
                var body = Encoding.UTF8.GetBytes(json);

                _model.BasicPublish(exchange: queueName + Exchange, routingKey: queueName + Queue, basicProperties: null, body: body);

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

            _model.BasicConsume(queueName + Queue, false, consumer);

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
