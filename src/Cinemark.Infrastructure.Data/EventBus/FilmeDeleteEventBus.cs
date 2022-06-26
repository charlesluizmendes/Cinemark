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
    public class FilmeDeleteEventBus : IFilmeDeleteEventBus, IDisposable
    {
        private const string _queueName = "Filme_Delete";
        private readonly ConnectionEventBus _connectionEventBus;
        private readonly IConnection _connection;
        private readonly IModel _model;

        private readonly MongoContext _mongoContext;
        private IMongoCollection<Filme> _mongoCollection;

        public FilmeDeleteEventBus(ConnectionEventBus connectionEventBus,
            MongoContext mongoContext)
        {
            _connectionEventBus = connectionEventBus;
            _connection = _connectionEventBus.CreateChannel();
            _model = _connection.CreateModel();
            _model.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            _mongoContext = mongoContext;
            _mongoCollection = _mongoContext.GetCollection<Filme>(typeof(Filme).Name);
        }

        public async Task SendMessageAsync(Filme filme)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var json = JsonConvert.SerializeObject(filme);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);

                await Task.Yield();
            }
        }

        public async Task ReadMessgaesAsync()
        {
            var consumer = new AsyncEventingBasicConsumer(_model);
            consumer.Received += async (ch, ea) =>
            {

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var filme = JsonConvert.DeserializeObject<Filme>(message);

                if (filme != null)
                    await HandleMessage(filme);

                _model.BasicAck(ea.DeliveryTag, false);

                await Task.Yield();
            };

            _model.BasicConsume(_queueName, false, consumer);

            await Task.Yield();
        }

        private async Task HandleMessage(Filme filme)
        {
            await _mongoCollection.DeleteOneAsync(Builders<Filme>.Filter.Eq("_id", filme.Id));
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
