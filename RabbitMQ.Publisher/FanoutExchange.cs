using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Publisher
{
    public class FanoutExchange : BaseExchange
    {
        private const string exchangeName = "logs-fanout";

        public static void Publisher()
        {
            using (var connection = factory.CreateConnection())
            {
                var channel = connection.CreateModel();

                channel.ExchangeDeclare(exchangeName, durable: true, type: ExchangeType.Fanout);//durable:true -> uygulama restart atılırsa exhangeler kaybolamasın, false-> restart atılırsa silinir

                Enumerable.Range(1, 50).ToList().ForEach(x =>
                {
                    string message = $"Log {x}";
                    var messageBody = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchangeName, string.Empty, null, messageBody);

                    Console.WriteLine($"Message sended : {message}");
                });
            }
        }
    }
}
