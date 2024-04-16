using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Publisher
{
    public class DirectExchange : BaseExchange
    {
        private const string exchangeName = "logs-direct";

        public static void Publisher()
        {
            using (var connection = factory.CreateConnection())
            {
                var channel = connection.CreateModel();

                channel.ExchangeDeclare(exchangeName, durable: true, type: ExchangeType.Direct);

                Enum.GetNames(typeof(LogNames)).ToList().ForEach(x =>
                {
                    var routeKey = $"route-{x}";
                    var queueName = $"direct-queue-{x}";
                    channel.QueueDeclare(queueName, true, false, false);
                    channel.QueueBind(queueName, exchangeName, routeKey);
                });

                Enumerable.Range(1, 50).ToList().ForEach(x =>
                {
                    LogNames log = (LogNames)new Random().Next(1, 5);

                    string message = $"Log Type: {log}";
                    var messageBody = Encoding.UTF8.GetBytes(message);

                    var routeKey = $"route-{log}";

                    channel.BasicPublish(exchangeName, routeKey, null, messageBody);

                    Console.WriteLine($"Message sended : {message}");
                });
            }
        }
    }
}
