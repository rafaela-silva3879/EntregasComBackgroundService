using Entregas.Application.Events;
using Entregas.Application.Interfaces;
using Entregas.Infra.EventBus.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Entregas.Infra.EventBus.Produccers
{
    public class PedidoProducer : IPedidoProducer 
    {
        private readonly MessageBrokerSettings? _messageBrokerSettings;
        public PedidoProducer
        (IOptions<MessageBrokerSettings>? messageBrokerSettings)
        {
            _messageBrokerSettings = messageBrokerSettings.Value;
        }
        public async Task AddAsync(PedidoPendenteEvent @event)
        {
            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(_messageBrokerSettings.Url)
            };
            using (var connection = connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                    queue: _messageBrokerSettings.Queue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );
                    await Task.Run(() => channel.BasicPublish(
                        exchange: string.Empty,
                        routingKey: _messageBrokerSettings.Queue,
                        basicProperties: null,
                        body: Encoding.UTF8.GetBytes
                        (JsonConvert.SerializeObject(@event))
                    ));
                }
            }
        }

    }
}