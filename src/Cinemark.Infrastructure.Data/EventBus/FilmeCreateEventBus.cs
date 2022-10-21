using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Models;
using Cinemark.Infrastructure.Data.Context;
using Cinemark.Infrastructure.Data.EventBus.Common;
using MongoDB.Driver;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Cinemark.Infrastructure.Data.EventBus
{
    public class FilmeCreateEventBus : IFilmeCreateEventBus, IDisposable
    {
        private const string _queueName = "Filme_Created";
        private readonly ConnectionEventBus _connectionEventBus;
        private readonly IConnection _connection;
        private readonly IModel _model;

        private readonly MongoContext _mongoContext;
        private IMongoCollection<Filme> _mongoCollection;

        public FilmeCreateEventBus(ConnectionEventBus connectionEventBus,
            MongoContext mongoContext)
        {
            _connectionEventBus = connectionEventBus;
            _connection = _connectionEventBus.CreateChannel();
            _model = _connection.CreateModel();

            _model.ExchangeDeclare(_queueName + "_DeadLetter_Exchange", ExchangeType.Fanout);
            _model.QueueDeclare(_queueName + "_DeadLetter_Queue", true, false, false, null);
            _model.QueueBind(_queueName + "_DeadLetter_Queue", _queueName + "_DeadLetter_Exchange", "");

            var arguments = new Dictionary<string, object>()
            {
                { "x-dead-letter-exchange", _queueName + "_DeadLetter_Exchange" }
            };

            _model.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: arguments);

            _mongoContext = mongoContext;
            _mongoCollection = _mongoContext.GetCollection<Filme>(typeof(Filme).Name);
        }

        public async Task SendMessageAsync(Filme filme)
        {
            var json = JsonConvert.SerializeObject(filme);
            var body = Encoding.UTF8.GetBytes(json);

            _model.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);

            await Task.Yield();
        }

        public async Task ReadMessgaesAsync()
        {
            var consumer = new AsyncEventingBasicConsumer(_model);
            consumer.Received += async (ch, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var filme = JsonConvert.DeserializeObject<Filme>(message);

                    if (filme != null)
                        await HandleMessage(filme);

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

        private async Task HandleMessage(Filme filme)
        {
            await _mongoCollection.InsertOneAsync(filme);
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
