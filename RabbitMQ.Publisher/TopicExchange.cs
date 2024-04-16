using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Publisher
{
    public class TopicExchange : BaseExchange
    {
        private const string exchangeName = "logs-topic";

        public static void Publisher()
        {
            using (var connection = factory.CreateConnection())
            {
                var channel = connection.CreateModel();

                channel.ExchangeDeclare(exchangeName, durable: true, type: ExchangeType.Topic);

                Random random = new Random();
                Enumerable.Range(1, 50).ToList().ForEach(x =>
                {
                    LogNames log1 = (LogNames)random.Next(1, 5);
                    LogNames log2 = (LogNames)random.Next(1, 5);
                    LogNames log3 = (LogNames)random.Next(1, 5);

                    var routeKey = $"{log1}.{log2}.{log3}";

                    string message = $"Log Type: {log1}-{log2}-{log3}";
                    var messageBody = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchangeName, routeKey, null, messageBody);

                    Console.WriteLine($"Message sended : {message}");
                });
            }
        }
    }
}
